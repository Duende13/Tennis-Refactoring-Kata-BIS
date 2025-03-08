using System;
using System.Collections;
using System.Collections.Generic;
using Xunit;

namespace Tennis.Tests
{
    public class TestDataGenerator : IEnumerable<object[]>
    {
        private readonly List<object[]> _data = new List<object[]>
        {
            new object[] {0, 0, "Love-All"},
            new object[] {1, 1, "Fifteen-All"},
            new object[] {2, 2, "Thirty-All"},
            new object[] {3, 3, "Deuce"},
            new object[] {4, 4, "Deuce"},
            new object[] {1, 0, "Fifteen-Love"},
            new object[] {0, 1, "Love-Fifteen"},
            new object[] {2, 0, "Thirty-Love"},
            new object[] {0, 2, "Love-Thirty"},
            new object[] {3, 0, "Forty-Love"},
            new object[] {0, 3, "Love-Forty"},
            new object[] {4, 0, "Win for player1"},
            new object[] {0, 4, "Win for player2"},
            new object[] {2, 1, "Thirty-Fifteen"},
            new object[] {1, 2, "Fifteen-Thirty"},
            new object[] {3, 1, "Forty-Fifteen"},
            new object[] {1, 3, "Fifteen-Forty"},
            new object[] {4, 1, "Win for player1"},
            new object[] {1, 4, "Win for player2"},
            new object[] {3, 2, "Forty-Thirty"},
            new object[] {2, 3, "Thirty-Forty"},
            new object[] {4, 2, "Win for player1"},
            new object[] {2, 4, "Win for player2"},
            new object[] {4, 3, "Advantage player1"},
            new object[] {3, 4, "Advantage player2"},
            new object[] {5, 4, "Advantage player1"},
            new object[] {4, 5, "Advantage player2"},
            new object[] {15, 14, "Advantage player1"},
            new object[] {14, 15, "Advantage player2"},
            new object[] {6, 4, "Win for player1"},
            new object[] {4, 6, "Win for player2"},
            new object[] {16, 14, "Win for player1"},
            new object[] {14, 16, "Win for player2"},
        };

        public IEnumerator<object[]> GetEnumerator() => _data.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

    public class TestDataGeneratorScoreNames : IEnumerable<object[]>
    {
        private readonly List<object[]> _data = new List<object[]>
        {
            new object[] {0, TennisScoreName.Love},
            new object[] {1, TennisScoreName.Fifteen},
            new object[] {2, TennisScoreName.Thirty},
            new object[] {3, TennisScoreName.Forty },
        };

        public IEnumerator<object[]> GetEnumerator() => _data.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

    public class TennisTests
    {
        [Theory]
        [ClassData(typeof(TestDataGenerator))]
        public void Tennis1Test(int p1, int p2, string expected)
        {
            var game = new TennisGame1("player1", "player2");
            CheckAllScores(game, p1, p2, expected);
        }

        [Theory]
        [ClassData(typeof(TestDataGenerator))]
        public void Tennis2Test(int p1, int p2, string expected)
        {
            var game = new TennisGame2("player1", "player2");
            CheckAllScores(game, p1, p2, expected);
        }

        [Theory]
        [ClassData(typeof(TestDataGenerator))]
        public void Tennis3Test(int p1, int p2, string expected)
        {
            var game = new TennisGame3("player1", "player2");
            CheckAllScores(game, p1, p2, expected);
        }
        [Theory]
        [ClassData(typeof(TestDataGenerator))]
        public void Tennis4Test(int p1, int p2, string expected)
        {
            var game = new TennisGame4("player1", "player2");
            CheckAllScores(game, p1, p2, expected);
        }
        [Theory]
        [ClassData(typeof(TestDataGenerator))]
        public void Tennis5Test(int p1, int p2, string expected)
        {
            var game = new TennisGame5("player1", "player2");
            CheckAllScores(game, p1, p2, expected);
        }
        [Theory]
        [ClassData(typeof(TestDataGenerator))]
        public void Tennis6Test(int p1, int p2, string expected)
        {
            var game = new TennisGame6("player1", "player2");
            CheckAllScores(game, p1, p2, expected);
        }
        [Theory]
        [ClassData(typeof(TestDataGenerator))]
        public void Tennis7Test(int p1, int p2, string expected)
        {
            var game = new TennisGame7("player1", "player2");
            CheckAllScores(game, p1, p2, "Current score: " + expected + ", enjoy your game!");
        }

        [Theory]
        [ClassData(typeof(TestDataGeneratorScoreNames))]
        public void Tennis2RegularScoreTest(int points, TennisScoreName expectedScore)
        {
            CheckRegularScoreNames(points, expectedScore);
        }

        [Fact]
        public void Player_Constructor_ShouldSetName()
        {
            var player = new Player("Roger Federer");
            Assert.Equal("Roger Federer", player.Name);
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void Player_ShouldThrowException_WhenNameIsInvalid(string invalidName)
        {
            Assert.Throws<ArgumentException>(() => new Player(invalidName));
        }

        private void CheckAllScores(ITennisGame game, int player1Score, int player2Score, string expectedScore)
        {
            var highestScore = Math.Max(player1Score, player2Score);
            for (var i = 0; i < highestScore; i++)
            {
                if (i < player1Score)
                    game.WonPoint("player1");
                if (i < player2Score)
                    game.WonPoint("player2");
            }

            Assert.Equal(expectedScore, game.GetScore());
        }

        private void CheckRegularScoreNames(int points, TennisScoreName expectedScore)
        {
            var player1 = new Player("player1");
            for (int i = 0; i < points; i++)
                player1.WinPoint();

            if (points < 3)
                Assert.Equal(expectedScore, player1.RegularScoreName());
        }

        [Theory]
        [InlineData(4)]
        [InlineData(15)]
        public void RegularScoreName_ShouldThrowException_WhenScoreIsInvalid(int score)
        {
            var player = new Player("player2");

            for (int i = 0; i < score; i++)
            {
                player.WinPoint();
            }

            Assert.Throws<InvalidOperationException>(() => player.RegularScoreName());
        }
    }
}