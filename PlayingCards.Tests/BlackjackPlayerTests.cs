using System.Collections.Generic;
using Blackjack.Game;
using PlayingCards.Domain;
using Xunit;

namespace PlayingCards.Tests
{
    public class BlackjackPlayerTests
    {
        [Fact]
        public void CanCalculatePlayerScoreWithoutAce()
        {
            Player player = new Player
            {
                Hand = new List<Card>
                {
                    new Card {Number = CardNumber.Three},
                    new Card {Number = CardNumber.Queen}
                }
            };

            Assert.Equal(13, player.Score());
        }

        [Fact]
        public void CanCalculatePlayerScoreWithOneAceOver21()
        {
            Player player = new Player
            {
                Hand = new List<Card>
                {
                    new Card {Number = CardNumber.Three},
                    new Card {Number = CardNumber.Queen},
                    new Card {Number = CardNumber.Ace}
                }
            };

            Assert.Equal(14, player.Score());
        }

        [Fact]
        public void CanCalculatePlayerScoreWithOneAceUnder21()
        {
            Player player = new Player
            {
                Hand = new List<Card>
                {
                    new Card {Number = CardNumber.Three},
                    new Card {Number = CardNumber.Two},
                    new Card {Number = CardNumber.Ace}
                }
            };

            Assert.Equal(16, player.Score());
        }

        [Fact]
        public void CanCalculatePlayerScoreWithTwoAcesUnder21()
        {
            Player player = new Player
            {
                Hand = new List<Card>
                {
                    new Card {Number = CardNumber.Three},
                    new Card {Number = CardNumber.Two},
                    new Card {Number = CardNumber.Ace},
                    new Card {Number = CardNumber.Ace}
                }
            };

            Assert.Equal(17, player.Score());
        }
    }
}
