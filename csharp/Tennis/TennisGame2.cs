using System;

namespace Tennis
{
    using static Tennis.TennisScoreName;
    public class TennisGame2 : ITennisGame
    {
        private const int MaxStandardScore = 3;
        private const int WinningPointDifference = 2;

        private readonly Player _player1;
        private readonly Player _player2;

        public TennisGame2(string player1Name, string player2Name)
        {
            _player1 = new Player(player1Name);
            _player2 = new Player(player2Name);
        }

        public string GetScore()
        {
            var score = "";

            if (IsDeuce())
                score = "Deuce";
            else if (IsTie())
                score = $"{_player1.RegularScoreName()}-All";
            else if (IsRegularPoint())
                score = _player1.RegularScoreName() + "-" + _player2.RegularScoreName();
            else if (HasWinner())
                score = $"Win for {GetLeadingPlayerName()}";
            else
                score = $"Advantage {GetLeadingPlayerName()}";

            return score;
        }

        public void WonPoint(string player)
        {
            if (player == "player1")
                _player1.WinPoint();
            else
                _player2.WinPoint();
        }

        private bool IsDeuce() => IsTie() && _player1.Score >= MaxStandardScore;
        private bool IsTie() => _player1.Score == _player2.Score;
        private bool IsRegularPoint() => _player1.Score <= MaxStandardScore && _player2.Score <= MaxStandardScore;
        private bool HasWinner()
        {
            return (_player1.Score > MaxStandardScore || _player2.Score > MaxStandardScore) &&
                Math.Abs(_player1.Score - _player2.Score) >= WinningPointDifference;
        }
        private string GetLeadingPlayerName() => _player1.Score > _player2.Score ? _player1.Name : _player2.Name;

    }

    public sealed class Player
    {
        public string Name { get; }
        private int _score;

        public Player(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Player name cannot be empty.", nameof(name));

            Name = name;
            _score = 0;
        }
        public int Score => _score;

        public void WinPoint()
        {
            _score++;
        }
        public TennisScoreName RegularScoreName()
        {
            return _score switch
            {
                0 => Love,
                1 => Fifteen,
                2 => Thirty,
                3 => Forty,
                _ => throw new InvalidOperationException($"Invalid score: {_score}")
            };
        }
        public override string ToString() => $"{Name} (Score: {Score})";

    }
    public enum TennisScoreName
    {
        Love,
        Fifteen,
        Thirty,
        Forty
    }

}
