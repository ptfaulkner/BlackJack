using System.Linq;
using System.Net.WebSockets;
using Blackjack.Game;
using Blackjack.Web.Models;

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
                TablePlayers = game.Players.Where(p => p.Name != PlayerName).ToList(),
                NewPlayers = game.NewPlayers
            };

            return currentPlayerDto;
        }
    }
}