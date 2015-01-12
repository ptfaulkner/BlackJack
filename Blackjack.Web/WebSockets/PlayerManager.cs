using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using Blackjack.Game;
using Blackjack.Web.Models;
using Microsoft.Ajax.Utilities;
using PlayingCards.Domain;

namespace Blackjack.Web.WebSockets
{
    public class PlayerManager
    {
        public readonly string PlayerName;
        public readonly WebSocket WebSocket;

        public PlayerManager(string playerName, WebSocket webSocket)
        {
            PlayerName = playerName;
            WebSocket = webSocket;
        }

        public CurrentPlayerDto GetCurrentPlayerDto(BlackjackGame game)
        {
            CurrentPlayerDto currentPlayerDto = new CurrentPlayerDto
            {
                GameStatus = game.GameStatus,
                Dealer = HideDealerSecondCard(game.Dealer),
                Player = game.Players.FirstOrDefault(p => p.Name == PlayerName),
                TablePlayers = game.Players.Where(p => p.Name != PlayerName)
                    .Select(p => HideTablePlayerCards(p, new []{ 0, 1 }))
                    .ToList(),
                NewPlayers = game.NewPlayers
            };

            return currentPlayerDto;
        }

        private Player HideTablePlayerCards(Player player, int[] cardIndexesToHide)
        {
            if (player.Game.GameStatus == HandStatus.Done)
                return player;

            Player playerCopy = CopyPlayer(player);

            List<Card> cardsToHide = cardIndexesToHide.Select(i => new Card
                {
                    Number = CardNumber.Blank, Suit = Suit.Blank
                })
                .ToList();

            playerCopy.Hand.RemoveRange(0, 2);
            playerCopy.Hand.InsertRange(0, cardsToHide);

            return playerCopy;
        }

        private Player HideDealerSecondCard(Player dealer)
        {
            if (dealer.Game.GameStatus == HandStatus.Done)
                return dealer;

            Player dealerCopy = CopyPlayer(dealer);

            Card cardToHide = new Card
            {
                Number = CardNumber.Blank, 
                Suit = Suit.Blank
            };

            dealerCopy.Hand.RemoveAt(1);
            dealerCopy.Hand.Insert(1, cardToHide);

            return dealerCopy;
        }

        private static Player CopyPlayer(Player player)
        {
            Player playerCopy = new Player
            {
                Name = player.Name,
                Game = player.Game,
                Hand = player.Hand.Select(c => new Card { Number = c.Number, Suit = c.Suit }).ToList(),
                HandStatus = player.HandStatus,
                IsTurnToHit = player.IsTurnToHit,
                Position = player.Position,
                Score = player.Score,
                WinningStatus = player.WinningStatus
            };
            return playerCopy;
        }
    }
}