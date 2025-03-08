namespace Tennis
{
    public class TennisGame2 : ITennisGame
    {
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
            if (p1point == p2point && p1point < 3)
            {
                score = $"{ScoreName(p1point)}-All";
            }
            else if (p1point == p2point && p1point > 2)
            {
                score = "Deuce";
            }
            else if (p1point < 4 && p2point < 4)
            {
                score = ScoreName(p1point) + "-" + ScoreName(p2point);
            }
            else if (p1point >= 4 && p2point >= 0 && (p1point - p2point) >= 2)
            {
                score = "Win for player1";
            }
            else if (p2point >= 4 && p1point >= 0 && (p2point - p1point) >= 2)
            {
                score = "Win for player2";
            }
            else if (p1point > p2point && p2point >= 3)
            {
                score = "Advantage player1";
            }
            else if (p2point > p1point && p1point >= 3)
            {
                score = "Advantage player2";
            }
            else
            {
                score = "";
            }
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

    }
}

