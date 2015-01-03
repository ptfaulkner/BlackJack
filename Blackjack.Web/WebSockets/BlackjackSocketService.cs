using System;
using System.Collections.Specialized;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using Newtonsoft.Json;

namespace Blackjack.Web.WebSockets
{
    public class BlackjackSocketService : ISocketService
    {
        private static readonly GameManager GameManager = new GameManager();

        public void OnOpen(WebSocket webSocket, NameValueCollection queryString)
        {
            string playerName = queryString.Get("playerName");

            GameManager.AddPlayer(playerName, webSocket);
            BroadcastGameStatus();
        }

        public void OnMessage(WebSocket webSocket, string message)
        {
            GameManager.ProcessPlayerAction(message, webSocket);
            BroadcastGameStatus();
        }

        public void OnClose(WebSocket webSocket)
        {
            GameManager.RemovePlayer(webSocket);
            BroadcastGameStatus();
        }

        public void BroadcastGameStatus()
        {
            string gameJson = JsonConvert.SerializeObject(GameManager.Game);
            ArraySegment<byte> outputBuffer = new ArraySegment<byte>(Encoding.UTF8.GetBytes(gameJson));

            foreach (PlayerManager pm in GameManager.PlayerManagers)
            {
                pm.WebSocket.SendAsync(outputBuffer, WebSocketMessageType.Text, true, CancellationToken.None);
            }
        }
    }
}