using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using PlayingCards.Domain;

namespace Blackjack.Game
{
    public class BlackjackGame
    {
        private readonly Deck _deck;

        public IEnumerable<Player> Players
        {
            get { return _players; }
        }

        public IEnumerable<string> NewPlayers
        {
            get { return _newPlayers; }
        }

        public IEnumerable<string> QuitPlayers
        {
            get { return _quitPlayers; }
        }

        public Player Dealer;
        private readonly List<Player> _players;
        private readonly List<string> _newPlayers;
        private readonly List<string> _quitPlayers;

        [JsonConverter(typeof(StringEnumConverter))]
        public HandStatus GameStatus { get; set; }

        public BlackjackGame(Deck deck)
        {
            _deck = deck;

            Dealer = new Player
            {
                Name = "Dealer",
                Game = this,
                WinningStatus = WinningStatus.Open,
                HandStatus = HandStatus.Open
            };

            _newPlayers = new List<string>();
            _players = new List<Player>();
            _quitPlayers = new List<string>();
            GameStatus = HandStatus.Done;
        }

        private void AddPlayer(string name)
        {
            _players.Add(new Player
            {
                Name = name,
                Position = _players.Count,
                Game = this,
                WinningStatus = WinningStatus.Open,
                HandStatus = HandStatus.Open
            });
        }

        public string AddNewPlayer(string playerName)
        {
            if (playerName == "Dealer")
            {
                return "You cannot be the the dealer.  Whatcha tryin to pull?";
            }

            if (Players.Any(p => p.Name == playerName) ||
                NewPlayers.Any(n => n == playerName))
            {
                return "Cannot add duplicate player name.";
            }

            if (Players.Count() + NewPlayers.Count() >= 5)
            {
                return "The max player count of 5 has been reached.";
            }

            _newPlayers.Add(playerName);
            return string.Empty;
        }

        public void RemovePlayer(string playerName)
        {
            Player gamePlayer = Players.FirstOrDefault(p => p.Name == playerName);

            if (gamePlayer == null)
            {
                _newPlayers.RemoveAll(p => p == playerName);
                return;
            }

            gamePlayer.HandStatus = HandStatus.Done;
            _quitPlayers.Add(gamePlayer.Name);
            MoveActiveSlot();
        }

        public void Deal()
        {
            if (GameStatus == HandStatus.Open)
                return;

            GameStatus = HandStatus.Open;
            _players.RemoveAll(p => QuitPlayers.Contains(p.Name));
            _quitPlayers.Clear();

            _deck.Reset();
            _deck.Shuffle();

            foreach (string name in NewPlayers)
            {
                AddPlayer(name);
            }

            _newPlayers.Clear();

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
            MoveActiveSlot();
            CheckPlayersForBlackjack();
        }

        private void CheckPlayersForBlackjack()
        {
            foreach (Player player in Players)
            {
                CalculatePlayerScore(player);
                if (player.Score != 21) continue;
                player.WinningStatus = WinningStatus.Blackjack;
                player.HandStatus = HandStatus.Done;

                if (player.IsTurnToHit)
                {
                    MoveActiveSlot();
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
            if (!player.IsTurnToHit && player.Name != "Dealer")
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

            if (!player.IsTurnToHit)
            {
                return;
            }

            MoveActiveSlot();
        }

        private void MoveActiveSlot()
        {
            List<Player> availablePlayers = _players.Where(p => p.HandStatus == HandStatus.Open).ToList();

            if (!availablePlayers.Any())
            {
                FinishGame();
            }
            else
            {
                int activeSlot = availablePlayers.Min(p => p.Position);
                Player player = availablePlayers.First(p => p.Position == activeSlot);
                _players.ForEach(p => p.IsTurnToHit = false);
                player.IsTurnToHit = true;
            }
        }

        private void FinishGame()
        {
            while (Dealer.Score < 17)
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

            GameStatus = HandStatus.Done;
        }

        public static void CalculatePlayerScore(Player player)
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
