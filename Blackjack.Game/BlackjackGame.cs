using System;
using System.Collections.Generic;
using System.Linq;
using PlayingCards.Domain;

namespace Blackjack.Game
{
    public class BlackjackGame
    {
        private readonly Deck _deck;
        public int ActiveSlot;

        public List<Player> Players { get; set; }
        public List<string> NewPlayers { get; set; } 
        public Player Dealer;

        public BlackjackGame(Deck deck)
        {
            _deck = deck;
            ActiveSlot = 0;

            Dealer = new Player
            {
                Name = "Dealer",
                Game = this,
                WinningStatus = WinningStatus.Open,
                HandStatus = HandStatus.Open
            };

            NewPlayers = new List<string>();
            Players = new List<Player>();
        }

        public void AddPlayer(string name)
        {
            Players.Add(new Player
            {
                Name = name,
                Position = Players.Count,
                Game = this,
                WinningStatus = WinningStatus.Open,
                HandStatus = HandStatus.Open
            });
        }

        public void Deal()
        {
            _deck.Reset();
            _deck.Shuffle();

            foreach (string name in NewPlayers)
            {
                AddPlayer(name);
            }

            NewPlayers.Clear();

            foreach (Player player in Players)
            {
                player.Hand = new List<Card> {_deck.TakeCard()};
                player.WinningStatus = WinningStatus.Open;
                player.HandStatus = HandStatus.Open;
            }

            Dealer.Hand = new List<Card>{_deck.TakeCard()};
            Dealer.WinningStatus = WinningStatus.Open;
            Dealer.HandStatus = HandStatus.Open;

            foreach (Player player in Players)
            {
                player.Hand.Add(_deck.TakeCard());
            }

            Dealer.Hand.Add(_deck.TakeCard());

            CheckPlayersForBlackjack();

            ActiveSlot = 0;
        }

        private void CheckPlayersForBlackjack()
        {
            foreach (Player player in Players)
            {
                CalculatePlayerScore(player);
                if (player.Score != 21) continue;
                player.WinningStatus = WinningStatus.Blackjack;
                player.HandStatus = HandStatus.Done;

                if (ActiveSlot == player.Position)
                {
                    ActiveSlot++;
                }
            }

            CalculatePlayerScore(Dealer);
            if (Dealer.Score == 21)
            {
                FinishGame();
            }
        }

        internal void Hit(Player player)
        {
            if (player.Position != ActiveSlot)
            {
                return;
            }

            player.Hand.Add(_deck.TakeCard());

            CalculatePlayerScore(player);

            if (player.Score > 21)
            {
                player.WinningStatus = WinningStatus.Busted;
                player.HandStatus = HandStatus.Done;
                if (player.Name != "Dealer")
                {
                    MoveActiveSlot();
                }
            }
            else if (player.Score == 21)
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

            if (player.Position != ActiveSlot)
            {
                return;
            }

            MoveActiveSlot();
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
                ActiveSlot = availablePlayers.Min(p => p.Position);
            }
        }

        private void FinishGame()
        {
            while (Dealer.Score < 18)
            {
                Dealer.Hit();
            }

            Dealer.HandStatus = HandStatus.Done;
            int dealerScore = Dealer.Score;

            foreach (Player player in Players.Where(p => p.WinningStatus == WinningStatus.Open))
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
                else if (player.Score > dealerScore)
                {
                    player.WinningStatus = WinningStatus.Winner;
                }
                else if (player.Score == dealerScore)
                {
                    player.WinningStatus = WinningStatus.Push;
                }
                else
                {
                    player.WinningStatus = WinningStatus.Loser;
                }
            }
        }

        private void CalculatePlayerScore(Player player)
        {
            short score = 0;

            if (player.Hand == null)
                return;

            foreach (Card nonAceCard in player.Hand.Where(c => c.Number != CardNumber.Ace))
            {
                score += CardValue(nonAceCard);
            }

            foreach (Card ace in player.Hand.Where(c => c.Number == CardNumber.Ace))
            {
                if ((score + 11) <= 21)
                {
                    score += 11;
                    continue;
                }
                score += 1;
            }

            player.Score = score;
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
