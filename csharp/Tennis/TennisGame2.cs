using System;

namespace Tennis
{
    public class TennisGame2 : ITennisGame
    {
        private const int MaxStandardScore = 3;
        private const int WinningPointDifference = 2;

        private int _player1Points = 0;
        private int _player2Point = 0;

        private readonly string _player1Name;
        private readonly string _player2Name;

        public TennisGame2(string player1Name, string player2Name)
        {
            this._player1Name = player1Name;
            this._player2Name = player2Name;
        }

        public string GetScore()
        {
            var score = "";

            if (IsDeuce())
                score = "Deuce";
            else if (IsTie())
                score = $"{ScoreName(_player1Points)}-All";
            else if (IsRegularPoint())
                score = ScoreName(_player1Points) + "-" + ScoreName(_player2Point);
            else if (HasWinner())
                score = $"Win for {GetLeadingPlayerName()}";
            else
                score = $"Advantage {GetLeadingPlayerName()}";

            return score;
        }

        public void WonPoint(string player)
        {
            if (player == "player1")
                _player1Points++;
            else
                _player2Point++;
        }
        private string ScoreName(int points)
        {
            return points switch
            {
                0 => "Love",
                1 => "Fifteen",
                2 => "Thirty",
                3 => "Forty",
                _ => "Invalid Score"
            };
        }
        private bool IsDeuce() => IsTie() && _player1Points >= MaxStandardScore;
        private bool IsTie() => _player1Points == _player2Point;
        private bool IsRegularPoint() => _player1Points <= MaxStandardScore && _player2Point <= MaxStandardScore;
        private bool HasWinner()
        {
            return (_player1Points > MaxStandardScore || _player2Point > MaxStandardScore) &&
                Math.Abs(_player1Points - _player2Point) >= WinningPointDifference;
        }
        private string GetLeadingPlayerName() => _player1Points > _player2Point ? _player1Name : _player2Name;

    }
}

