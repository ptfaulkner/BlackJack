using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace PlayingCards.Domain
{
    public class Card
    {
        [JsonConverter(typeof(StringEnumConverter))]
        public CardNumber Number { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public Suit Suit { get; set; }
    }
}
