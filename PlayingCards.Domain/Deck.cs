﻿using System;
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
 
        public List<Card> Cards { get; set; }
 
        public void Reset()
        {
            Cards = Enumerable.Range(1, 4)
                .SelectMany(s => Enumerable.Range(2, 13)
                    .Select(c => new Card
                    {
                        Suit = (Suit)s,
                        Number = (CardNumber)c
                    }))
                .ToList();
        }
 
        public void Shuffle()
        {
            Cards = Cards.OrderBy(c => Guid.NewGuid()).ToList();
        }
 
        public Card TakeCard()
        {
            Card card = Cards.FirstOrDefault();
            Cards.Remove(card);
 
            return card;
        }
    }
}
