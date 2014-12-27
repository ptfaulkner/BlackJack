using System;
using System.Collections.Generic;
using System.Dynamic;
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

            Players = playerNames.Select((p, i) => new Player
            {
                Name = p, 
                Position = i, 
                Game = this, 
                WinningStatus = WinningStatus.Open, 
                HandStatus = HandStatus.Open
            }).ToList();

            Dealer = new Player { Name = "Dealer" };
        }

        public void Deal()
        {
            _deck.Shuffle();
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

            CheckPlayersForBlackjack();

            _activeSlot = 0;
        }

        private void CheckPlayersForBlackjack()
        {
            foreach (Player player in Players)
            {
                if (player.Score() == 21)
                {
                    player.WinningStatus = WinningStatus.Blackjack;
                    player.HandStatus = HandStatus.Done;

                    if (_activeSlot == player.Position)
                    {
                        _activeSlot++;
                    }
                }
            }

            if (Dealer.Score() == 21)
            {
                FinishGame();
            }
        }

        internal void Hit(Player player)
        {
            if (player.Position != _activeSlot)
            {
                return;
            }

            player.Hand.Add(_deck.TakeCard());

            int playerScore = player.Score();

            if (playerScore > 21)
            {
                player.WinningStatus = WinningStatus.Busted;
                player.HandStatus = HandStatus.Done;
                if (player.Name != "Dealer")
                {
                    MoveActiveSlot();
                }
            }
            else if (playerScore == 21)
            {
                player.HandStatus = HandStatus.Done;
                if (player.Name != "Dealer")
                {
                    MoveActiveSlot();
                }
            }
        }

        internal void Stay(Player player)
        {
            player.HandStatus = HandStatus.Done;

            if (player.Position != _activeSlot)
            {
                return;
            }

            MoveActiveSlot();

            FinishGame();
        }

        private void MoveActiveSlot()
        {
            IEnumerable<Player> availablePlayers = Players.Where(p => p.HandStatus == HandStatus.Open).ToList();

            if (!availablePlayers.Any())
            {
                FinishGame();
            }
            else
            {
                _activeSlot = availablePlayers.Min(p => p.Position);
            }
        }

        private void FinishGame()
        {
            while (Dealer.Score() < 18)
            {
                Dealer.Hit();
            }

            Dealer.HandStatus = HandStatus.Done;
            int dealerScore = Dealer.Score();

            foreach (Player player in Players)
            {
                player.HandStatus = HandStatus.Done;
                if (player.WinningStatus == WinningStatus.Blackjack)
                {
                    continue;
                }
                
                if (Dealer.WinningStatus == WinningStatus.Busted &&
                    player.WinningStatus != WinningStatus.Busted)
                {
                    player.WinningStatus = WinningStatus.Winner;
                }
                else if (player.Score() > dealerScore)
                {
                    player.WinningStatus = WinningStatus.Winner;
                }
                else if (player.Score() == dealerScore)
                {
                    player.WinningStatus = WinningStatus.Push;
                }
                else
                {
                    player.WinningStatus = WinningStatus.Loser;
                }
            }
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
