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
            Assert.Equal(2, game.Players.Count());
            
            Assert.Equal("Dealer", game.Dealer.Name);
        }

        [Fact]
        public void CanAddNewPlayer()
        {
            BlackjackGame game = new BlackjackGame(new Deck());

            Assert.Equal(0, game.Players.Count());

            game.AddNewPlayer("patrick");

            Assert.Equal(0, game.Players.Count());

            game.Deal();

            Assert.Equal(1, game.Players.Count());
            Assert.Equal(0, game.NewPlayers.Count());
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
            Assert.Equal(0, BlackjackGame.CardValue(new Card { Number = CardNumber.Ace }));
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
        public void CannotDealInMiddleOfGame()
        {
            BlackjackGame game = GetGame();

            Player player1 = game.Players.FirstOrDefault(p => p.Position == 0);
            player1.Stay();

            Assert.Equal(1, game.Players.Single(p => p.IsTurnToHit).Position);

            game.Deal();

            Assert.Equal(1, game.Players.Single(p => p.IsTurnToHit).Position);
        }

        [Fact]
        public void CanRemovePlayers()
        {
            BlackjackGame game = GetGame();

            Player player1 = game.Players.FirstOrDefault(p => p.Position == 0);
            player1.Stay();

            game.RemovePlayer(player1.Name);

            Player player2 = game.Players.FirstOrDefault(p => p.Position == 1);
            player2.Stay();

            game.Deal();

            Assert.Equal(1, game.Players.Count());
        }

        [Fact]
        public void CanRemovePlayerNotStarted()
        {
            BlackjackGame game = GetGame();

            Assert.Equal(0, game.NewPlayers.Count());

            game.AddNewPlayer("Joe");

            game.RemovePlayer("Joe");

            Assert.Equal(0, game.NewPlayers.Count());
        }

        [Fact]
        public void CannotAddDuplicatePlayerName()
        {
            BlackjackGame game = GetGame();

            Assert.Equal("Cannot add duplicate player name.", game.AddNewPlayer("Patrick"));
        }

        [Fact]
        public void CannotAddMoreThanFivePlayers()
        {
            BlackjackGame game = GetGame();

            Assert.Equal(string.Empty, game.AddNewPlayer("Joe"));
            Assert.Equal(string.Empty, game.AddNewPlayer("Chuck"));
            Assert.Equal(string.Empty, game.AddNewPlayer("Wes"));

            Assert.Equal("The max player count of 5 has been reached.", game.AddNewPlayer("Rain Man"));
        }

        [Fact]
        public void CannotAddAPlayerWithNameDealer()
        {
            BlackjackGame game = GetGame();

            Assert.Equal("You cannot be the the dealer.  Whatcha tryin to pull?", game.AddNewPlayer("Dealer"));
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
            game.AddNewPlayer("Patrick");
            game.AddNewPlayer("Ashley");
            game.Deal();
            return game;
        }
    }
}
