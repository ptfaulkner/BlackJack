using System.Collections.Generic;
using Blackjack.Game;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Blackjack.Web.Models
{
    public class CurrentPlayerDto
    {
        public Player Player { get; set; }
        public Player Dealer { get; set; }
        public IList<Player> TablePlayers { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public HandStatus GameStatus { get; set; }
        public IEnumerable<string> NewPlayers { get; set; } 
    }
}