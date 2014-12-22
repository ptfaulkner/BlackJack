using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PlayingCards.Domain;

namespace Blackjack.Game
{
    public class Game
    {
        private Deck _deck;

        public List<Player> Players { get; set; }
        public Player Dealer;

        public Game(Deck deck, IEnumerable<string> playerNames)
        {
            _deck = deck;

            Players = playerNames.Select(p => new Player { Name = p }).ToList();

            Dealer = new Player {Name = "Dealer"};
        }

        public void Deal()
        {
            foreach (Player player in Players)
            {
                player.Hand = new List<Card> {_deck.TakeCard()};
            }

            Dealer.Hand = new List<Card>{_deck.TakeCard()};

            foreach (Player player in Players)
            {
                player.Hand.Add(_deck.TakeCard());
            }

            Dealer.Hand.Add(_deck.TakeCard());
        }
    }
}
