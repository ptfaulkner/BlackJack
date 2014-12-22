using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blackjack.Game;
using PlayingCards.Domain;
using Xunit;

namespace PlayingCards.Tests
{
    public class BlackjackPlayerTests
    {
        [Fact]
        public void CanCalculatePlayerScore()
        {
            Player player = new Player();
            player.Hand = new List<Card>
            {
                new Card {Number = CardNumber.Three},
                new Card {Number = CardNumber.Queen}
            };

            Assert.Equal(13, player.Score());
        }
    }
}
