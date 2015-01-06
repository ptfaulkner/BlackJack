using System;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.WebSockets;

namespace Blackjack.Web.WebSockets
{
    public class Handler
    {
        private readonly ISocketService _socketService;

        public Handler(ISocketService socketService)
        {
            _socketService = socketService;
        }

        public async Task ProcessWebsocketSession(AspNetWebSocketContext context)
        {
            const int maxMessageSize = 1024;
            byte[] receiveBuffer = new byte[maxMessageSize];
            WebSocket webSocket = context.WebSocket;

            _socketService.OnOpen(webSocket, context.QueryString);

            while (webSocket.State == WebSocketState.Open)
            {
                ArraySegment<byte> inputSegment = new ArraySegment<byte>(receiveBuffer);
                WebSocketReceiveResult result = webSocket.ReceiveAsync(inputSegment, CancellationToken.None).Result;

                if (result.MessageType == WebSocketMessageType.Close)
                {
                    _socketService.OnClose(webSocket);
                    await webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, string.Empty, CancellationToken.None);
                }
                else if (result.MessageType == WebSocketMessageType.Binary)
                {
                    _socketService.OnClose(webSocket);
                    await webSocket.CloseAsync(WebSocketCloseStatus.InvalidMessageType, "Cannot accept binary frame", CancellationToken.None);
                }
                else
                {
                    int count = result.Count;

                    while (result.EndOfMessage == false)
                    {
                        if (count >= maxMessageSize)
                        {
                            string closeMessage = string.Format("Maximum message size: {0} bytes.", maxMessageSize);
                            await webSocket.CloseAsync(WebSocketCloseStatus.MessageTooBig, closeMessage, CancellationToken.None);
                            return;
                        }

                        result = await webSocket.ReceiveAsync(new ArraySegment<byte>(receiveBuffer, count, maxMessageSize - count), CancellationToken.None);
                        count += result.Count;
                    }

                    var receivedString = Encoding.UTF8.GetString(receiveBuffer, 0, count);

                    _socketService.OnMessage(webSocket, receivedString);
                }
            }
        }
    }
}