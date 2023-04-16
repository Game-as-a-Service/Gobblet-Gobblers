using Wsa.Gaas.GobbletGobblers.Domain;

namespace Wsa.Gaas.Gobblet_Gobblers.Tests
{
    [TestFixture]
    public class PutCockTests
    {
        [SetUp]
        public void Setup()
        {
        }

        // 玩家A的 中奇雞 放置至 棋盤(1, 1)
        [Test]
        public void PlayerA_PutMediumCockToLocation1_1()
        {
            var game = new Game();

            var playerA = new Player()
                .Nameself("Josh");

            var playerB = new Player()
                .Nameself("Tom");

            // Given:
            // 玩家A, 玩家B
            game.JoinPlayer(playerA)
                .JoinPlayer(playerB);

            game.Start();

            // When:
            // 玩家A 放置 中奇雞 至 棋盤(1, 1) 位置
            var expected = playerA.GetHandCock(2);
            var command = new PutCockCommand(playerA.Id, 2, new Location(1, 1));
            game.PutCock(command);

            // Then:
            // 玩家A的 中奇雞 放置至 棋盤(1, 1)
            var actual = game.GetCock(command.Location);

            Assert.That(actual, Is.EqualTo(expected));
        }

        // 玩家A的手上無奇雞, 並將 中奇雞 放置至 棋盤(1, 1) 位置
        [Test]
        public void PlayerA_NoHaveCock_PutMediumCockToLocation1_1()
        {
            var game = new Game();

            var playerA = new Player()
                .Nameself("Josh");

            var playerB = new Player()
                .Nameself("Tom");

            // Given:
            // 玩家A, 玩家B
            game.JoinPlayer(playerA)
                .JoinPlayer(playerB);

            // TODO: 無法彈性設定玩家手牌,

            // When:
            // 玩家A 放置 中奇雞 至 棋盤(1, 1) 位置
            var command = new PutCockCommand(playerA.Id, 2, new Location(1, 1));

            // Then:
            // 不合法
            Assert.Throws<ArgumentOutOfRangeException>(() => game.PutCock(command), "Can not get cock");
        }


        // 玩家A的手上無奇雞, 並將 中奇雞 放置至 棋盤(1, 1) 位置
        [Test]
        public void PlayerA_BoardHaveSmallCock_PutMediumCockToLocation1_1()
        {
            var game = new Game();

            var playerA = new Player()
                .Nameself("Josh");

            var playerB = new Player()
                .Nameself("Tom");

            // Given:
            // 玩家A, 玩家B
            game.JoinPlayer(playerA)
                .JoinPlayer(playerB);

            game.Start();

            // When:
            // 玩家A 放置 中奇雞 至 棋盤(1, 1) 位置, TODO: 測試的時候要完整模擬, 放置整個流程放置奇雞
            var commandSetp1 = new PutCockCommand(playerA.Id, 0, new Location(0, 0));
            game.PutCock(commandSetp1);

            var commandSetp2 = new PutCockCommand(playerB.Id, 4, new Location(1, 1));
            game.PutCock(commandSetp2);

            var expected = playerA.GetHandCock(2);
            var commandSetp3 = new PutCockCommand(playerA.Id, 2, new Location(1, 1));
            game.PutCock(commandSetp3);

            // Then:
            // 玩家A的 中奇雞 放置至 棋盤(1, 1)
            var actual = game.GetCock(commandSetp3.Location);

            Assert.That(actual, Is.EqualTo(expected));
        }

        // 玩家A的手上無奇雞, 並將 中奇雞 放置至 棋盤(1, 1) 位置
        [Test]
        public void PlayerA_BoardHaveMediumCock_PutMediumCockToLocation1_1()
        {
            var game = new Game();

            var playerA = new Player()
                .Nameself("Josh");

            var playerB = new Player()
                .Nameself("Tom");

            // Given:
            // 玩家A, 玩家B
            game.JoinPlayer(playerA)
                .JoinPlayer(playerB);

            game.Start();

            // When:
            // 玩家A 放置 中奇雞 至 棋盤(1, 1) 位置
            var commandSetp1 = new PutCockCommand(playerA.Id, 0, new Location(0, 0));
            game.PutCock(commandSetp1);

            var commandSetp2 = new PutCockCommand(playerB.Id, 2, new Location(1, 1));
            game.PutCock(commandSetp2);

            var commandSetp3 = new PutCockCommand(playerA.Id, 2, new Location(1, 1));

            // Then:
            // 不合法
            Assert.Throws<ArgumentOutOfRangeException>(() => game.PutCock(commandSetp3), "Illegal put cock");
        }
    }
}
