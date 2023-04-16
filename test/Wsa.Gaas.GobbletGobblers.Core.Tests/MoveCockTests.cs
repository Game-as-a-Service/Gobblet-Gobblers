using Wsa.Gaas.GobbletGobblers.Domain;

namespace Wsa.Gaas.Gobblet_Gobblers.Tests
{
    [TestFixture]
    public class MoveCockTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void PlayerA_MoveCockLocation2_0ToLocation1_1()
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
            // 玩家A 放置 中奇雞 至 棋盤(2, 0) 位置
            var expected = playerA.GetHandCock(2); // TODO: 測試的時候要知道玩家奇雞是什麼
            var commandSetp1 = new PutCockCommand(playerA.Id, 2, new Location(2, 0));
            game.PutCock(commandSetp1);

            var commandSetp2 = new PutCockCommand(playerB.Id, 4, new Location(0, 0));
            game.PutCock(commandSetp2);


            // var expected = playerA.GetHandCock(2); // TODO: 如果寫這裡會失敗
            var commandSetp3 = new MoveCockCommand(playerA.Id, new Location(2, 0), new Location(1, 1));
            game.MoveCock(commandSetp3);

            // Then:
            // 玩家A的 中奇雞 放置至 棋盤(1, 1)
            var fromActual = game.GetCock(commandSetp3.FromLocation);
            var toActual = game.GetCock(commandSetp3.ToLocation);

            Assert.That(fromActual, Is.Null);
            Assert.That(toActual, Is.EqualTo(expected));
        }

        [Test]
        public void PlayerA_MovePlayerBCockLocation2_0ToLocation1_1()
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
            // 玩家A 放置 中奇雞 至 棋盤(2, 0) 位置          
            var commandSetp1 = new PutCockCommand(playerA.Id, 2, new Location(0, 0));
            game.PutCock(commandSetp1);

            var commandSetp2 = new PutCockCommand(playerB.Id, 2, new Location(2, 0));
            game.PutCock(commandSetp2);

            var commandSetp3 = new MoveCockCommand(playerA.Id, new Location(2, 0), new Location(1, 1));

            // Then:
            // 玩家A的 中奇雞 放置至 棋盤(1, 1)
            Assert.Throws<ArgumentOutOfRangeException>(() => game.MoveCock(commandSetp3), "Illegal move cock");
        }

        [Test]
        public void PlayerA_HavePlayerBCockOnLocation1_1_MoveCockLocation2_0ToLocation1_1()
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
            // 玩家A 放置 中奇雞 至 棋盤(2, 0) 位置
            var expected = playerA.GetHandCock(2);
            var commandSetp1 = new PutCockCommand(playerA.Id, 2, new Location(2, 0));
            game.PutCock(commandSetp1);

            var commandSetp2 = new PutCockCommand(playerB.Id, 4, new Location(1, 1));
            game.PutCock(commandSetp2);


            var commandSetp3 = new MoveCockCommand(playerA.Id, new Location(2, 0), new Location(1, 1));
            game.MoveCock(commandSetp3);

            // Then:
            // 玩家A的 中奇雞 放置至 棋盤(1, 1)
            var fromActual = game.GetCock(commandSetp3.FromLocation);
            var toActual = game.GetCock(commandSetp3.ToLocation);

            Assert.That(fromActual, Is.Null);
            Assert.That(toActual, Is.EqualTo(expected));
        }

        [Test]
        public void PlayerA_HavePlayerBMediumCockOnLocation1_1_MoveCockLocation2_0ToLocation1_1()
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
            // 玩家A 放置 中奇雞 至 棋盤(2, 0) 位置          
            var commandSetp1 = new PutCockCommand(playerA.Id, 2, new Location(2, 0));
            game.PutCock(commandSetp1);

            var commandSetp2 = new PutCockCommand(playerB.Id, 2, new Location(1, 1));
            game.PutCock(commandSetp2);

            var commandSetp3 = new MoveCockCommand(playerA.Id, new Location(2, 0), new Location(1, 1));

            // Then:
            // 玩家A的 中奇雞 放置至 棋盤(1, 1)
            Assert.Throws<ArgumentOutOfRangeException>(() => game.MoveCock(commandSetp3), "Illegal move cock");
        }
    }
}
