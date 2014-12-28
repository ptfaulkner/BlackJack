using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blackjack.Game;
using Newtonsoft.Json;
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
            Assert.Equal(2, game.Players.Count);
            
            Assert.Equal("Dealer", game.Dealer.Name);
        }

        [Fact]
        public void CanAddNewPlayer()
        {
            BlackjackGame game = new BlackjackGame(new Deck());

            Assert.Equal(0, game.Players.Count);

            game.NewPlayers.Add("Patrick");

            Assert.Equal(0, game.Players.Count);

            game.Deal();

            Assert.Equal(1, game.Players.Count);
            Assert.Equal(0, game.NewPlayers.Count);
        }

        [Fact]
        public void CanGetTwoCardsFromDeal()
        {
            BlackjackGame game = GetGame();

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

        [Fact]
        public void PlayersCanStay()
        {
            BlackjackGame game = GetGame();

            Player player1 = game.Players.FirstOrDefault(p => p.Position == 0);
            player1.Stay();

            Player player2 = game.Players.FirstOrDefault(p => p.Position == 1);
            player1.Hit();
            Assert.Equal(2, player1.Hand.Count);

            player2.Hit();
            Assert.Equal(3, player2.Hand.Count);
        }

        [Fact]
        public void CanFinishGame()
        {
            BlackjackGame game = GetGame();

            foreach (Player player in game.Players)
            {
                player.Stay();
            }

            foreach (Player player in game.Players)
            {
                Assert.NotEqual(WinningStatus.Open, player.WinningStatus);
                Assert.NotEqual(HandStatus.Open, player.HandStatus);
            }

            Assert.NotEqual(HandStatus.Open, game.Dealer.HandStatus);
        }

        [Fact]
        public void CanSerializeGame()
        {
            string gameJson = JsonConvert.SerializeObject(GetGame());
        }

        internal static BlackjackGame GetGame()
        {
            BlackjackGame game = new BlackjackGame(new Deck());
            game.NewPlayers.Add("Patrick");
            game.NewPlayers.Add("Ashley");
            game.Deal();
            return game;
        }
    }
}
