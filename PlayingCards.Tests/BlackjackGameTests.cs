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
            BlackjackGame game = GetGame();

            Assert.Equal(3, game.Players.Count);
            
            Assert.Equal("Dealer", game.Dealer.Name);
        }

        [Fact]
        public void CannotCreateBlackjackWithDuplicateNames()
        {
            InvalidOperationException exception =
                Assert.Throws<InvalidOperationException>(() => new BlackjackGame(new Deck(), new List<string> { "Patrick", "Patrick" }));

            Assert.Equal("The player names must be unique.", exception.Message);
        }

        [Fact]
        public void CanGetTwoCardsFromDeal()
        {
            BlackjackGame game = GetGame();
            game.Deal();

            foreach (Player p in game.Players)
            {
                Assert.Equal(2, p.Hand.Count);
            }

            Assert.Equal(2, game.Dealer.Hand.Count);
        }

        [Fact]
        public void CanGetCardValue()
        {
            Assert.Equal(2, BlackjackGame.CardValue(new Card { Number = CardNumber.Two }));
            Assert.Equal(3, BlackjackGame.CardValue(new Card { Number = CardNumber.Three }));
            Assert.Equal(4, BlackjackGame.CardValue(new Card { Number = CardNumber.Four }));
            Assert.Equal(5, BlackjackGame.CardValue(new Card { Number = CardNumber.Five }));
            Assert.Equal(6, BlackjackGame.CardValue(new Card { Number = CardNumber.Six }));
            Assert.Equal(7, BlackjackGame.CardValue(new Card { Number = CardNumber.Seven }));
            Assert.Equal(8, BlackjackGame.CardValue(new Card { Number = CardNumber.Eight })); 
            Assert.Equal(9, BlackjackGame.CardValue(new Card { Number = CardNumber.Nine }));
            Assert.Equal(10, BlackjackGame.CardValue(new Card { Number = CardNumber.Ten }));
            Assert.Equal(10, BlackjackGame.CardValue(new Card { Number = CardNumber.Jack }));
            Assert.Equal(10, BlackjackGame.CardValue(new Card { Number = CardNumber.Queen }));
            Assert.Equal(10, BlackjackGame.CardValue(new Card { Number = CardNumber.King }));
        }

        private static BlackjackGame GetGame()
        {
            return new BlackjackGame(new Deck(), new List<string> { "Patrick", "Ashley", "Harvey" });
        }
    }
}
