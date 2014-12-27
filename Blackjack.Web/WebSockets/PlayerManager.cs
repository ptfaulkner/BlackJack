using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Web;
using Blackjack.Game;

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