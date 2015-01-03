﻿using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using Blackjack.Game;
using PlayingCards.Domain;

namespace Blackjack.Web.WebSockets
{
    public class GameManager
    {
        public readonly BlackjackGame Game;
        public List<PlayerManager> PlayerManagers;

        private readonly object _addPlayerLocker;
        private readonly object _actionLocker;
        private readonly object _removePlayerLocker;

        public GameManager()
        {
            Game = new BlackjackGame(new Deck());
            PlayerManagers = new List<PlayerManager>();
            _addPlayerLocker = new object();
            _actionLocker = new object();
            _removePlayerLocker = new object();
        }

        public void AddPlayer(string playerName, WebSocket webSocket)
        {
            lock (_addPlayerLocker)
            {
                PlayerManager playerManager = new PlayerManager(playerName, webSocket);
                PlayerManagers.Add(playerManager);
                Game.NewPlayers.Add(playerName);

                if (Game.Players.Count == 0)
                {
                    Game.Deal();
                }
            }
        }

        public void RemovePlayer(WebSocket webSocket)
        {
            lock (_removePlayerLocker)
            {
                PlayerManager playerManager =PlayerManagers.First(pm => pm.WebSocket == webSocket);
                Player player = Game.Players.FirstOrDefault(p => p.Name == playerManager.PlayerName);

                PlayerManagers.Remove(playerManager);

                if (player == null)
                {
                    Game.NewPlayers.RemoveAll(p => p == playerManager.PlayerName);
                }
                else
                {
                    Game.RemovePlayer(player);
                    if (Game.Players.Count == Game.QuitPlayers.Count)
                    {
                        Game.GameStatus = HandStatus.Done;
                    }
                }
            }
        }

        public void ProcessPlayerAction(string action, WebSocket webSocket)
        {
            lock (_actionLocker)
            {
                Player player = GetPlayer(webSocket);

                switch (action)
                {
                    case "Hit":
                        player.Hit();
                        break;
                    case "Stay":
                        player.Stay();
                        break;
                    case "Deal":
                        Game.Deal();
                        break;
                }
            }
        }

        private Player GetPlayer(WebSocket webSocket)
        {
            PlayerManager playerManager = PlayerManagers.First(pm => pm.WebSocket == webSocket);
            Player player = Game.Players.First(p => p.Name == playerManager.PlayerName);
            return player;
        }
    }
}