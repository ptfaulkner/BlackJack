using System.Collections.Generic;
using System.Linq;
using Blackjack.Game;
using PlayingCards.Domain;
using Xunit;

namespace Blackjack.Tests
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
            BlackjackGame.CalculatePlayerScore(player);
            Assert.Equal(13, player.Score);
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
            BlackjackGame.CalculatePlayerScore(player);
            Assert.Equal(14, player.Score);
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
            BlackjackGame.CalculatePlayerScore(player);
            Assert.Equal(16, player.Score);
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
            BlackjackGame.CalculatePlayerScore(player);
            Assert.Equal(17, player.Score);
        }

        [Fact]
        public void CanHitPlayer()
        {
            BlackjackGame game = BlackjackGameTests.GetGame();
            Player playah = game.Players.First();

            int cards = playah.Hand.Count;
            playah.Hit();

            Assert.NotEqual(cards, playah.Hand.Count);
        }

        [Fact]
        public void PlayerCanStay()
        {
            BlackjackGame game = BlackjackGameTests.GetGame();
            Player player = game.Players.First();

            HandStatus preStayStatus = player.HandStatus;
            player.Stay();
            Assert.Equal(HandStatus.Open, preStayStatus);
            Assert.Equal(HandStatus.Done, player.HandStatus);
        }
    }
}