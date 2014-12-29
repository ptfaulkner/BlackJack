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

            BroadcastGame();
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
                    _gameManager.Game.Deal();
                    break;
            }

            BroadcastGame();
        }

        public Player GetPlayer(WebSocket webSocket)
        {
            PlayerManager playerManager = _gameManager.PlayerManagers.First(pm => pm.WebSocket == webSocket);
            Player player = _gameManager.Game.Players.First(p => p.Name == playerManager.PlayerName);
            return player;
        }

        public void OnClose(WebSocket webSocket)
        {
            PlayerManager playerManager = _gameManager.PlayerManagers.First(pm => pm.WebSocket == webSocket);
            Player player = _gameManager.Game.Players.FirstOrDefault(p => p.Name == playerManager.PlayerName);

            _gameManager.PlayerManagers.Remove(playerManager);

            if (player == null)
            {
                _gameManager.Game.NewPlayers.RemoveAll(p => p == playerManager.PlayerName);
            }
            else
            {
                _gameManager.Game.RemovePlayer(player);
                if (_gameManager.Game.Players.Count == _gameManager.Game.QuitPlayers.Count)
                {
                    _gameManager.Game.GameStatus = HandStatus.Done;
                }
            }

            BroadcastGame();
        }

        public void BroadcastGame()
        {
            string gameJson = JsonConvert.SerializeObject(_gameManager.Game);
            ArraySegment<byte> outputBuffer = new ArraySegment<byte>(Encoding.UTF8.GetBytes(gameJson));

            foreach (PlayerManager pm in _gameManager.PlayerManagers)
            {
                pm.WebSocket.SendAsync(outputBuffer, WebSocketMessageType.Text, true, CancellationToken.None);
            }
        }
    }
}