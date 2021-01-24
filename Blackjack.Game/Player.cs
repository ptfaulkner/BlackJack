using System.Collections.Generic;
using System.Text.Json.Serialization;
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
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public HandStatus HandStatus { get; set; }
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public WinningStatus WinningStatus { get; set; }
        
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