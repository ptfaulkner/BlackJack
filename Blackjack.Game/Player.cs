using System.Collections.Generic;
using System.Linq;
using PlayingCards.Domain;

namespace Blackjack.Game
{
    public class Player
    {
        public string Name { get; set; }
        public List<Card> Hand { get; set; }

        public short Score()
        {
            short score = 0;

            foreach (Card nonAceCard in Hand.Where(c => c.Number != CardNumber.Ace))
            {
                score += Game.CardValue(nonAceCard);
            }

            foreach (Card ace in Hand.Where(c => c.Number == CardNumber.Ace))
            {
                if ((score + 11) < 21)
                {
                    score += 11;
                    continue;
                }
                score += 1;
            }

            return score;
        }
    }
}
