using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Web;
using Blackjack.Game;
using PlayingCards.Domain;

namespace Blackjack.Web.WebSockets
{
    public class BlackjackSocketService : ISocketService
    {
        private static BlackjackGame _game;

        public void OnOpen()
        {
            _game = new BlackjackGame(new Deck(), new List<string> { "Patrick "});
            _game.Deal();
        }

        public void OnMessage()
        {
            throw new NotImplementedException();
        }

        public void OnClose()
        {
            throw new NotImplementedException();
        }
    }
}