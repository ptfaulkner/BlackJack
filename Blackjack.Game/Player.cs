using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PlayingCards.Domain;

namespace Blackjack.Game
{
    public class Player
    {
        public string Name { get; set; }
        public List<Card> Hand { get; set; }

        public int Score()
        {
            int score = 0;

            Hand.Sum(c => c.Number);

            return score;
        }
    }
}
