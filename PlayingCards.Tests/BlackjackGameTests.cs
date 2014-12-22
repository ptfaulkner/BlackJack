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
    public class BlackjackGameTests
    {
        [Fact]
        public void CanCreateBlackjack()
        {
            Game game = GetGame();

            Assert.Equal(3, game.Players.Count);
            
            Assert.Equal("Dealer", game.Dealer.Name);
        }

        [Fact]
        public void CanGetTwoCardsFromDeal()
        {
            Game game = GetGame();
            game.Deal();

            foreach (Player p in game.Players)
            {
                Assert.Equal(2, p.Hand.Count);
            }

            Assert.Equal(2, game.Dealer.Hand.Count);
        }

        private static Game GetGame()
        {
            return new Game(new Deck(), new List<string> { "Patrick", "Ashley", "Harvey" });
        }
    }
}
