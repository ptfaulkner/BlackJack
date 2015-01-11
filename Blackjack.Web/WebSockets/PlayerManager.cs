using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using Blackjack.Game;
using Blackjack.Web.Models;
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
                Dealer = game.Dealer,
                Player = game.Players.FirstOrDefault(p => p.Name == PlayerName),
                TablePlayers = game.Players.Where(p => p.Name != PlayerName)
                    .Select(HideTablePlayerCards)
                    .ToList(),
                NewPlayers = game.NewPlayers
            };

            return currentPlayerDto;
        }

        private Player HideTablePlayerCards(Player player)
        {
            Player playerCopy = new Player
            {
                Name = player.Name,
                Game = player.Game,
                Hand = player.Hand.Select(c => new Card { Number = c.Number, Suit = c.Suit}).ToList(),
                HandStatus = player.HandStatus,
                IsTurnToHit = player.IsTurnToHit,
                Position = player.Position,
                Score = player.Score,
                WinningStatus = player.WinningStatus
            };

            List<Card> cardsToHide = playerCopy.Hand.GetRange(0, 2);
            cardsToHide.ForEach(c => {c.Number = CardNumber.Blank; c.Suit = Suit.Blank;});

            playerCopy.Hand.RemoveRange(0, 2);
            playerCopy.Hand.InsertRange(0, cardsToHide);

            return playerCopy;
        }
    }
}