using System.Collections.Specialized;
using System.Net.WebSockets;

namespace Blackjack.Web.WebSockets
{
    public interface ISocketService
    {
        void OnOpen(WebSocket webSocket, NameValueCollection queryString);
        void OnMessage(WebSocket webSocket, string message);
        void OnClose(WebSocket webSocket);
    }
}
