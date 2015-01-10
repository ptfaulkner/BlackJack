using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using PlayingCards.Domain;

namespace Blackjack.Game
{
    public class Player
    {
        public string Name { get; set; }
        public List<Card> Hand { get; set; }
        public bool IsTurnToHit { get; set; }
        [JsonIgnore]
        public BlackjackGame Game { get; set; }
        public int Position { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public HandStatus HandStatus { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public WinningStatus WinningStatus { get; set; }

        private readonly object _lockObject = new object();

        public short Score { get; set; }

        public void Hit()
        {
            Game.Hit(this);
        }

        public void Stay()
        {
            Game.Stay(this);
        }
    }
}
