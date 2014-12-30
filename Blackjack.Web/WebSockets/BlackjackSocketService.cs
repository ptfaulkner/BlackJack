using System;
using System.Collections.Specialized;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using Blackjack.Game;
using Newtonsoft.Json;
using PlayingCards.Domain;

namespace Blackjack.Web.WebSockets
{
    public class BlackjackSocketService : ISocketService
    {
        private static readonly GameManager GameManager = new GameManager();

        public void OnOpen(WebSocket webSocket, NameValueCollection queryString)
        {
            string playerName = queryString.Get("playerName");

            PlayerManager playerManager = new PlayerManager(playerName, webSocket);
            GameManager.PlayerManagers.Add(playerManager);
            GameManager.Game.NewPlayers.Add(playerName);

            if (GameManager.Game.Players.Count == 0)
            {
                GameManager.Game.Deal();
            }

            BroadcastGameStatus();
        }

        public void OnMessage(WebSocket webSocket, string message)
        {
            switch (message)
            {
                case "Hit":
                    GetPlayer(webSocket).Hit();
                    break;
                case "Stay":
                    GetPlayer(webSocket).Stay();
                    break;
                case "Deal":
                    GameManager.Game.Deal();
                    break;
            }

            BroadcastGameStatus();
        }

        public void OnClose(WebSocket webSocket)
        {
            PlayerManager playerManager = GameManager.PlayerManagers.First(pm => pm.WebSocket == webSocket);
            Player player = GameManager.Game.Players.FirstOrDefault(p => p.Name == playerManager.PlayerName);

            GameManager.PlayerManagers.Remove(playerManager);

            if (player == null)
            {
                GameManager.Game.NewPlayers.RemoveAll(p => p == playerManager.PlayerName);
            }
            else
            {
                GameManager.Game.RemovePlayer(player);
                if (GameManager.Game.Players.Count == GameManager.Game.QuitPlayers.Count)
                {
                    GameManager.Game.GameStatus = HandStatus.Done;
                }
            }

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

        private static Player GetPlayer(WebSocket webSocket)
        {
            PlayerManager playerManager = GameManager.PlayerManagers.First(pm => pm.WebSocket == webSocket);
            Player player = GameManager.Game.Players.First(p => p.Name == playerManager.PlayerName);
            return player;
        }
    }
}