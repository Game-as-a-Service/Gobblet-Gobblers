using Wsa.Gaas.GobbletGobblers.Domain;
using Wsa.Gaas.GobbletGobblers.Domain.Commands;

namespace Wsa.Gaas.Gobblet_Gobblers.Tests
{
    [TestFixture]
    public class LineTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void PlayerA_HorizontalLine()
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
            var commandSetp1 = new PutCockCommand(playerA.Id, 0, new Location(1, 0));
            game.PutCock(commandSetp1);

            var commandSetp2 = new PutCockCommand(playerB.Id, 0, new Location(0, 0));
            game.PutCock(commandSetp2);

            var commandSetp3 = new PutCockCommand(playerA.Id, 0, new Location(1, 1));
            game.PutCock(commandSetp3);

            var commandSetp4 = new PutCockCommand(playerB.Id, 0, new Location(0, 1));
            game.PutCock(commandSetp4);

            var commandSetp5 = new PutCockCommand(playerA.Id, 0, new Location(1, 2));
            game.PutCock(commandSetp5);

            // Then:
            // 玩家A的 中奇雞 放置至 棋盤(1, 1)
            Assert.That(game.Gameover(), Is.True);
            Assert.That(game.GetWinner(), Is.EqualTo(playerA));
        }

        [Test]
        public void PlayerA_VerticalLine()
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
            var commandSetp1 = new PutCockCommand(playerA.Id, 0, new Location(0, 0));
            game.PutCock(commandSetp1);

            var commandSetp2 = new PutCockCommand(playerB.Id, 0, new Location(0, 1));
            game.PutCock(commandSetp2);

            var commandSetp3 = new PutCockCommand(playerA.Id, 0, new Location(1, 0));
            game.PutCock(commandSetp3);

            var commandSetp4 = new PutCockCommand(playerB.Id, 0, new Location(0, 2));
            game.PutCock(commandSetp4);

            var commandSetp5 = new PutCockCommand(playerA.Id, 0, new Location(2, 0));
            game.PutCock(commandSetp5);

            // Then:
            // 玩家A的 中奇雞 放置至 棋盤(1, 1)
            Assert.That(game.Gameover(), Is.True);
            Assert.That(game.GetWinner(), Is.EqualTo(playerA));
        }

        [Test]
        public void PlayerA_SlashLine()
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
            var commandSetp1 = new PutCockCommand(playerA.Id, 0, new Location(0, 0));
            game.PutCock(commandSetp1);

            var commandSetp2 = new PutCockCommand(playerB.Id, 0, new Location(0, 1));
            game.PutCock(commandSetp2);


            var commandSetp3 = new PutCockCommand(playerA.Id, 0, new Location(1, 1));
            game.PutCock(commandSetp3);

            var commandSetp4 = new PutCockCommand(playerB.Id, 0, new Location(0, 2));
            game.PutCock(commandSetp4);

            var commandSetp5 = new PutCockCommand(playerA.Id, 0, new Location(2, 2));
            game.PutCock(commandSetp5);

            // Then:
            // 玩家A的 中奇雞 放置至 棋盤(1, 1)
            Assert.That(game.Gameover(), Is.True);
            Assert.That(game.GetWinner(), Is.EqualTo(playerA));
        }

        [Test]
        public void PlayerA_NotLine()
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
            var commandSetp1 = new PutCockCommand(playerA.Id, 0, new Location(0, 0));
            game.PutCock(commandSetp1);

            var commandSetp2 = new PutCockCommand(playerB.Id, 0, new Location(1, 1));
            game.PutCock(commandSetp2);

            var commandSetp3 = new PutCockCommand(playerA.Id, 0, new Location(2, 2));
            game.PutCock(commandSetp3);

            // Then:
            // 玩家A的 中奇雞 放置至 棋盤(1, 1)
            Assert.That(game.Gameover(), Is.False);
        }
    }
}
