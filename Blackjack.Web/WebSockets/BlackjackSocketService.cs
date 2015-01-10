using System;
using System.Collections.Specialized;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Blackjack.Web.WebSockets
{
    public class BlackjackSocketService : ISocketService
    {
        private static readonly GameManager GameManager = new GameManager();

        public void OnOpen(WebSocket webSocket, NameValueCollection queryString)
        {
            string playerName = queryString.Get("playerName");

            string message = GameManager.AddPlayer(playerName, webSocket);

            if (!string.IsNullOrEmpty(message))
            {
                webSocket.CloseAsync(WebSocketCloseStatus.PolicyViolation, message, CancellationToken.None);
            }

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
            JsonSerializerSettings jsonSerializerSettings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };

            foreach (PlayerManager pm in GameManager.PlayerManagers)
            {
                string gameJson = JsonConvert.SerializeObject(pm.GetCurrentPlayerDto(GameManager.Game),
                    jsonSerializerSettings);
                ArraySegment<byte> outputBuffer = new ArraySegment<byte>(Encoding.UTF8.GetBytes(gameJson));
                pm.WebSocket.SendAsync(outputBuffer, WebSocketMessageType.Text, true, CancellationToken.None);
            }
        }
    }
}