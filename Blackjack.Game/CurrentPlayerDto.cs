using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Blackjack.Game
{
    public class CurrentPlayerDto
    {
        public Player Player { get; set; }
        public Player Dealer { get; set; }
        public IList<Player> TablePlayers { get; set; }
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public HandStatus GameStatus { get; set; }
        public IEnumerable<string> NewPlayers { get; set; }
    }
}