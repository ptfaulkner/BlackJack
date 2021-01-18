using System.Collections.Generic;
using System.Linq;
using PlayingCards.Domain;

namespace Blackjack.Game
{
    public class PlayerManager
    {
        public readonly string PlayerName;
        public readonly string PlayerId;

        public PlayerManager(string playerName, string playerId)
        {
            PlayerName = playerName;
            PlayerId = playerId;
        }

        public CurrentPlayerDto GetCurrentPlayerDto(BlackjackGame game)
        {
            CurrentPlayerDto currentPlayerDto = new CurrentPlayerDto
            {
                GameStatus = game.GameStatus,
                Dealer = HideDealerFirstCard(game),
                Player = game.Players.FirstOrDefault(p => p.Name == PlayerName),
                TablePlayers = game.Players.Where(p => p.Name != PlayerName).ToList(),
                NewPlayers = game.NewPlayers
            };

            return currentPlayerDto;
        }

        private Player HideDealerFirstCard(BlackjackGame game)
        {
            if (game.GameStatus == HandStatus.Done)
            {
                return game.Dealer;
            }

            Player dealerWithHiddenCard = new Player
            {
                Hand = new List<Card> { new Card(), new Card { Suit = game.Dealer.Hand[1].Suit, Number = game.Dealer.Hand[1].Number } },
                HandStatus = game.Dealer.HandStatus,
                IsTurnToHit = game.Dealer.IsTurnToHit,
                Name = game.Dealer.Name,
                Position = game.Dealer.Position,
                Score = game.Dealer.Score,
                Game = game.Dealer.Game
            };

            return dealerWithHiddenCard;
        }
    }
}