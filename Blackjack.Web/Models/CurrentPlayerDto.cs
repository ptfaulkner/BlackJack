using System.Collections.Generic;
using Blackjack.Game;

namespace Blackjack.Web.Models
{
    public class CurrentPlayerDto
    {
        public Player Player { get; set; }
        public Player Dealer { get; set; }
        public IList<Player> TablePlayers { get; set; }
        public HandStatus GameStatus { get; set; }
    }
}