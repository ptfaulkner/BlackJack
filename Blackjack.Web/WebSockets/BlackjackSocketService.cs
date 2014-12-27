using System;
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
        private static GameManager _gameManager;

        public void OnOpen(WebSocket webSocket, string playerName)
        {
            if (_gameManager == null)
            {
                BlackjackGame game = new BlackjackGame(new Deck());
                _gameManager = new GameManager(game);
            }

            PlayerManager playerManager = new PlayerManager(playerName, webSocket);
            _gameManager.PlayerManagers.Add(playerManager);
            _gameManager.Game.NewPlayers.Add(playerName);

            if (_gameManager.Game.Players.Count == 0)
            {
                _gameManager.Game.Deal();
            }

            OnMessage(webSocket, null);
        }

        public void OnMessage(WebSocket webSocket, string message)
        {
            if (message != null)
            {
                PlayerManager playerManager = _gameManager.PlayerManagers.First(pm => pm.WebSocket == webSocket);
                Player player = _gameManager.Game.Players.First(p => p.Name == playerManager.PlayerName);

                switch (message)
                {
                    case "Hit":
                        player.Hit();
                        break;
                    case "Stay":
                        player.Stay();
                        break;
                    case "Deal":
                        _gameManager.Game.Deal();
                        break;
                }
            }

            string gameJson = JsonConvert.SerializeObject(_gameManager.Game);
            ArraySegment<byte> outputBuffer = new ArraySegment<byte>(Encoding.UTF8.GetBytes(gameJson));

            foreach (PlayerManager pm in _gameManager.PlayerManagers)
            {
                pm.WebSocket.SendAsync(outputBuffer, WebSocketMessageType.Text, true, CancellationToken.None);
            }
        }

        public void OnClose(WebSocket webSocket)
        {
            PlayerManager playerManager = _gameManager.PlayerManagers.First(pm => pm.WebSocket == webSocket);
            Player player = _gameManager.Game.Players.First(p => p.Name == playerManager.PlayerName);

            _gameManager.PlayerManagers.Remove(playerManager);
            _gameManager.Game.Players.Remove(player);
        }
    }
}