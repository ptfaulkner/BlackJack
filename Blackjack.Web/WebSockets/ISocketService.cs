using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack.Web.WebSockets
{
    public interface ISocketService
    {
        void OnOpen(WebSocket webSocket, NameValueCollection queryString);
        void OnMessage(WebSocket webSocket, string message);
        void OnClose(WebSocket webSocket);
    }
}
