using System.Net.WebSockets;

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
    }
}