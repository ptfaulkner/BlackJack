using System.Linq;
using PlayingCards.Domain;
using Xunit;

namespace Blackjack.Tests
{
    public class DeckTests
    {
        [Fact]
        public void CanGet52Cards()
        {
            Deck deck = new Deck();

            Assert.Equal(52, deck.Cards.Count);
        }

        [Fact]
        public void CanTakeCard()
        {
            Deck deck = new Deck();

            Card cardTaken = deck.TakeCard();

            Assert.Equal(51, deck.Cards.Count);

            Card missingCard = deck.Cards.FirstOrDefault(c => c.Suit == cardTaken.Suit && c.Number == cardTaken.Number);

            Assert.Null(missingCard);
        }

        [Fact]
        public void CanResetDeck()
        {
            Deck deck = new Deck();

            deck.TakeCard();

            Assert.Equal(51, deck.Cards.Count);

            deck.Reset();

            Assert.Equal(52, deck.Cards.Count);
        }
    }
}