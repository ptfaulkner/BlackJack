using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Security.Policy;
using System.Text;
using System.Threading;
using System.Web;
using Blackjack.Game;
using Newtonsoft.Json;
using PlayingCards.Domain;

namespace Blackjack.Web.WebSockets
{
    public class BlackjackSocketService : ISocketService
    {
        private static BlackjackGame _game;

        public void OnOpen(WebSocket webSocket, string playerName)
        {
            _game = new BlackjackGame(new Deck(), new List<string> { playerName });
            _game.Deal();
            OnMessage(webSocket);
        }

        public void OnMessage(WebSocket webSocket)
        {
            string gameJson = JsonConvert.SerializeObject(_game);
            ArraySegment<byte> outputBuffer = new ArraySegment<byte>(Encoding.UTF8.GetBytes(gameJson));

            webSocket.SendAsync(outputBuffer, WebSocketMessageType.Text, true, CancellationToken.None);
        }

        public void OnClose(WebSocket webSocket)
        {
            throw new NotImplementedException();
        }
    }
}