using System.Linq;

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
                Dealer = game.Dealer,
                Player = game.Players.FirstOrDefault(p => p.Name == PlayerName),
                TablePlayers = game.Players.Where(p => p.Name != PlayerName).ToList(),
                NewPlayers = game.NewPlayers
            };

            return currentPlayerDto;
        }
    }
}