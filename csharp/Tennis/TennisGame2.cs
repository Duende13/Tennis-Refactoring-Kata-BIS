using System;

namespace Tennis
{
    public class TennisGame2 : ITennisGame
    {
        private const int MaxStandardScore = 3;
        private const int WinningPointDifference = 2;

        private int p1point = 0;
        private int p2point = 0;

        private string player1Name;
        private string player2Name;

        public TennisGame2(string player1Name, string player2Name)
        {
            this.player1Name = player1Name;
            this.player2Name = player2Name;
        }

        public string GetScore()
        {
            var score = "";

            if (IsDeuce())
                score = "Deuce";
            else if (IsTie())
                score = $"{ScoreName(p1point)}-All";
            else if (IsRegularPoint())
                score = ScoreName(p1point) + "-" + ScoreName(p2point);
            else if (HasWinner())
                score = $"Win for {GetLeadingPlayerName()}";
            else
                score = $"Advantage {GetLeadingPlayerName()}";

            return score;
        }

        public void SetP1Score(int number)
        {
            for (int i = 0; i < number; i++)
            {
                P1Score();
            }
        }

        public void SetP2Score(int number)
        {
            for (var i = 0; i < number; i++)
            {
                P2Score();
            }
        }

        private void P1Score()
        {
            p1point++;
        }

        private void P2Score()
        {
            p2point++;
        }

        public void WonPoint(string player)
        {
            if (player == "player1")
                P1Score();
            else
                P2Score();
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
        private bool IsDeuce() => IsTie() && p1point >= MaxStandardScore;
        private bool IsTie() => p1point == p2point;
        private bool IsRegularPoint() => p1point <= MaxStandardScore && p2point <= MaxStandardScore;
        private bool HasWinner()
        {
            return (p1point > MaxStandardScore || p2point > MaxStandardScore) &&
                Math.Abs(p1point - p2point) >= WinningPointDifference;
        }
        private string GetLeadingPlayerName() => p1point > p2point ? player1Name : player2Name;

    }
}

