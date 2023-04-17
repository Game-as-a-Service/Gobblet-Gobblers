using Wsa.Gaas.GobbletGobblers.Domain;

namespace Wsa.Gaas.Gobblet_Gobblers.Tests
{
    [TestFixture]
    public class GameTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void GameIsNotFull()
        {
            // Given:
            var game = new Game();

            var playerA = new Player().Nameself("Josh");

            // When:
            game.JoinPlayer(playerA);

            // Then:
            Assert.Throws<AggregateException>(() => game.Start(), "Game is not full");
        }

        [Test]
        public void GameIsFull()
        {
            // Given:
            var game = new Game();

            var playerA = new Player().Nameself("Josh");
            var playerB = new Player().Nameself("Tom");

            game.JoinPlayer(playerA)
                .JoinPlayer(playerB);

            // When:
            var playerC = new Player().Nameself("Mark");

            // Then:
            Assert.Throws<ArgumentException>(() => game.JoinPlayer(playerC), "Game is full");
        }

        [Test]
        public void GameExitPlayer()
        {
            // Given:
            var game = new Game();

            var playerA = new Player().Nameself("Josh");
            var playerB = new Player().Nameself("Tom");

            game.JoinPlayer(playerA)
                .JoinPlayer(playerB);

            // When:
            game.ExitPlayer(playerA);

            // Then:
            Assert.That(game.Players.Count, Is.EqualTo(1));
        }
    }
}
