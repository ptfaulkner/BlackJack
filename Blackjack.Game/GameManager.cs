using System.Collections.Generic;
using System.Linq;
using PlayingCards.Domain;

namespace Blackjack.Game
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

        public string AddPlayer(string playerName, string playerId)
        {
            lock (_addPlayerLocker)
            {
                PlayerManager playerManager = new PlayerManager(playerName, playerId);
                string message = Game.AddNewPlayer(playerName);

                if (!string.IsNullOrEmpty(message))
                {
                    return message;
                }

                PlayerManagers.Add(playerManager);

                if (!Game.Players.Any())
                {
                    Game.Deal();
                }
            }

            return null;
        }

        public void RemovePlayer(string playerId)
        {
            lock (_removePlayerLocker)
            {
                PlayerManager playerManager = PlayerManagers.First(pm => pm.PlayerId == playerId);

                PlayerManagers.Remove(playerManager);
                Game.RemovePlayer(playerManager.PlayerName);
            }
        }

        public void ProcessPlayerAction(string action, string playerId)
        {
            lock (_actionLocker)
            {
                Player player = GetPlayer(playerId);

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

        private Player GetPlayer(string playerId)
        {
            PlayerManager playerManager = PlayerManagers.FirstOrDefault(pm => pm.PlayerId == playerId);
            if (playerManager == null)
            {
                return null;
            }

            Player player = Game.Players.FirstOrDefault(p => p.Name == playerManager.PlayerName);
            return player;
        }
    }
}