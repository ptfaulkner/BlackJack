using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.WebSockets;

namespace Blackjack.Web.Controllers
{
    [RoutePrefix("api/blackjack")]
    public class BlackjackController : ApiController
    {
        [HttpGet, Route]
        public HttpResponseMessage Get()
        {
            HttpContext currentContext = HttpContext.Current;
            if (currentContext.IsWebSocketRequest ||
                currentContext.IsWebSocketRequestUpgrading)
            {
                currentContext.AcceptWebSocketRequest(ProcessWebsocketSession);
                return Request.CreateResponse(HttpStatusCode.SwitchingProtocols);
            }

            return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Need a websocket connection.");
        }

        private async Task ProcessWebsocketSession(AspNetWebSocketContext context)
        {
            const int maxMessageSize = 1024;
            byte[] receiveBuffer = new byte[maxMessageSize];
            WebSocket webSocket = context.WebSocket;

            while (webSocket.State == WebSocketState.Open)
            {
                ArraySegment<byte> inputSegment = new ArraySegment<byte>(receiveBuffer);
                WebSocketReceiveResult result = webSocket.ReceiveAsync(inputSegment, CancellationToken.None).Result;

                if (result.MessageType == WebSocketMessageType.Close)
                {
                    await webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, string.Empty, CancellationToken.None);
                }
                else if (result.MessageType == WebSocketMessageType.Binary)
                {
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
                    var echoString = "You said " + receivedString;
                    ArraySegment<byte> outputBuffer = new ArraySegment<byte>(Encoding.UTF8.GetBytes(echoString));

                    await webSocket.SendAsync(outputBuffer, WebSocketMessageType.Text, true, CancellationToken.None);
                }
            }
        }
    }
}
