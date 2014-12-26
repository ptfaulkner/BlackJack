﻿using System;
using System.Collections.Generic;
using System.Linq;
using PlayingCards.Domain;

namespace Blackjack.Game
{
    public class BlackjackGame
    {
        private readonly Deck _deck;
        private int _activeSlot;

        public List<Player> Players { get; set; }
        public Player Dealer;

        public BlackjackGame(Deck deck, List<string> playerNames)
        {
            _deck = deck;
            _activeSlot = 0;

            if (playerNames.Distinct().Count() < playerNames.Count())
            {
                throw new InvalidOperationException("The player names must be unique.");
            }

            Players = playerNames.Select((p, i) => new Player { Name = p, Position = i, Game = this }).ToList();

            Dealer = new Player { Name = "Dealer" };
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

            _activeSlot = 0;
        }

        internal void Hit(Player player)
        {
            if (player.Position != _activeSlot)
            {
                return;
            }

            player.Hand.Add(_deck.TakeCard());
        }

        public static short CardValue(Card card)
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
