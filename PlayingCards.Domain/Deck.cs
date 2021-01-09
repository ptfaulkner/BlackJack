using System;
using System.Collections.Generic;
using System.Linq;

namespace PlayingCards.Domain
{
    public class Deck
    {
        public Deck()
        {
            Reset();
        }

        public Stack<Card> Cards { get; set; }

        public void Reset()
        {
            Cards = new Stack<Card>(
                Enumerable.Range(1, 4)
                    .SelectMany(s => Enumerable.Range(2, 13)
                        .Select(c => new Card
                        {
                            Suit = (Suit)s,
                            Number = (CardNumber)c
                        })));
        }

        public void Shuffle()
        {
            Cards = new Stack<Card>(Cards.OrderBy(c => Guid.NewGuid()));
        }

        public Card TakeCard()
        {
            Card card = Cards.Pop();

            return card;
        }
    }
}