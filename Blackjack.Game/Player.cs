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

        public short Score()
        {
            short score = 0;

            foreach (Card nonAceCard in Hand.Where(c => c.Number != CardNumber.Ace))
            {
                score += CardValue(nonAceCard);
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

        private short CardValue(Card card)
        {
            short value;
            switch (card.Number)
            {
                case CardNumber.Two:
                    value = 2;
                    break;
                case CardNumber.Three:
                    value = 3;
                    break;
                case CardNumber.Four:
                    value = 4;
                    break;
                case CardNumber.Five:
                    value = 5;
                    break;
                case CardNumber.Six:
                    value = 6;
                    break;
                case CardNumber.Seven:
                    value = 7;
                    break;
                case CardNumber.Eight:
                    value = 8;
                    break;
                case CardNumber.Nine:
                    value = 9;
                    break;
                case CardNumber.Ten:
                case CardNumber.Jack:
                case CardNumber.Queen:
                case CardNumber.King:
                    value = 10;
                    break;
                default:
                    value = 0;
                    break;
            }

            return value;
        }
    }
}
