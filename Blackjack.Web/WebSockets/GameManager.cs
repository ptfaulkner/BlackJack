using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Blackjack.Game;

namespace Blackjack.Web.WebSockets
{
    public class GameManager
    {
        public readonly BlackjackGame Game;
        public List<PlayerManager> PlayerManagers;

        public GameManager(BlackjackGame game)
        {
            Game = game;
            PlayerManagers = new List<PlayerManager>();
        }
    }
}