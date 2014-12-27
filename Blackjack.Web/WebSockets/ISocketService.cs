using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack.Web.WebSockets
{
    public interface ISocketService
    {
        void OnOpen(WebSocket webSocket, string playerName);
        void OnMessage(WebSocket webSocket);
        void OnClose(WebSocket webSocket);
    }
}
