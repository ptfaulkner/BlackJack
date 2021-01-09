using System.Text.Json.Serialization;

namespace PlayingCards.Domain
{
    public class Card
    {
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public CardNumber Number { get; set; }
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public Suit Suit { get; set; }
    }
}