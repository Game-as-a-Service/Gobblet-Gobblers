using Wsa.Gaas.GobbletGobblers.Domain;

namespace Wsa.Gaas.Gobblet_Gobblers.Tests
{
    [TestFixture]
    public class PutCockTests
    {
        private Game _game;

        [SetUp]
        public void Setup()
        {
            _game = new Game();
        }

        // 玩家A的 中奇雞 放置至 棋盤(1, 1)
        [Test]
        public async Task PlayerAPutMediumCockToLocation1_1()
        {
            var playerA = new Player()
                .Nameself("Josh");

            var playerB = new Player()
                .Nameself("Tom");

            // Given:
            // 玩家A, 玩家B
            _game.JoinPlayer(playerA)
                .JoinPlayer(playerB);

            _game.Start();

            // When:
            // 玩家A 放置 中奇雞 至 棋盤(1, 1) 位置
            var expected = playerA.GetHandCock(2);
            var command = new PutCockCommand(playerA.Id, 2, new Location(1, 1));
            _game.PutCock(command);

            // Then:
            // 玩家A的 中奇雞 放置至 棋盤(1, 1)
            var actual = _game.GetCock(command.Location);

            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public async Task TestGet()
        {
        }
    }
}
