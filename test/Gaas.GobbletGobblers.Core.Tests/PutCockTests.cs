using Gaas.GobbletGobblers.Domain;
using Gaas.GobbletGobblers.Domain.Commands;

namespace Gaas.Gobblet_Gobblers.Tests
{
    [TestFixture]
    public class PutCockTests
    {
        private Game _game;
        private Player _playerA;
        private Player _playerB;
        [SetUp]
        public void Setup()
        {
            _game = new Game();
            _playerA = new Player().Nameself("Josh");
            _playerB = new Player().Nameself("Tom");
            _game.JoinPlayer(_playerA)
                .JoinPlayer(_playerB);
            _game.Start();
        }

        // 玩家A的 中奇雞 放置至 棋盤(1, 1)
        [Test]
        public void PlayerA_PutMediumCockToLocation1_1()
        {
            // Given: (In Setup)
            
            // When:
            // 玩家A 放置 中奇雞 至 棋盤(1, 1) 位置
            var expected = _playerA.GetHandCock(2);
            var command = new PutCockCommand(_playerA.Id, 2, new Location(1, 1));
            _game.PutCock(command);

            // Then:
            // 玩家A的 中奇雞 放置至 棋盤(1, 1)
            var actual = _game.GetCock(command.Location);

            Assert.That(actual, Is.EqualTo(expected));
        }

        /*
        // 玩家A的手上無奇雞, 並將 中奇雞 放置至 棋盤(1, 1) 位置
        [Test]
        public void PlayerA_NoHaveCock_PutMediumCockToLocation1_1()
        {
            // TODO: 無法彈性設定玩家手牌,

            // When:
            // 玩家A 放置 中奇雞 至 棋盤(1, 1) 位置
            var command = new PutCockCommand(_playerA.Id, 2, new Location(1, 1));

            // Then:
            // 不合法
            Assert.Throws<ArgumentOutOfRangeException>(() => _game.PutCock(command), "Can not get cock");
        }
        */

        // 玩家A的手上無奇雞, 並將 中奇雞 放置至 棋盤(1, 1) 位置
        [Test]
        public void PlayerA_BoardHaveSmallCock_PutMediumCockToLocation1_1()
        {
            //Given:
            var commandStep1 = new PutCockCommand(_playerA.Id, 0, new Location(0, 0));
            _game.PutCock(commandStep1);

            var commandStep2 = new PutCockCommand(_playerB.Id, 4, new Location(1, 1));
            _game.PutCock(commandStep2);

            // When:
            // 玩家A 放置 中奇雞 至 棋盤(1, 1) 位置, TODO: 測試的時候要完整模擬, 放置整個流程放置奇雞
    
            var expected = _playerA.GetHandCock(2);
            var commandStep3 = new PutCockCommand(_playerA.Id, 2, new Location(1, 1));
            _game.PutCock(commandStep3);

            // Then:
            // 玩家A的 中奇雞 放置至 棋盤(1, 1)
            var actual = _game.GetCock(commandStep3.Location);

            Assert.That(actual, Is.EqualTo(expected));
        }

        // 玩家A的手上無奇雞, 並將 中奇雞 放置至 棋盤(1, 1) 位置
        [Test]
        public void PlayerA_BoardHaveMediumCock_PutMediumCockToLocation1_1()
        {
            //Given:
            var commandStep1 = new PutCockCommand(_playerA.Id, 0, new Location(0, 0));
            _game.PutCock(commandStep1);

            var commandStep2 = new PutCockCommand(_playerB.Id, 2, new Location(1, 1));
            _game.PutCock(commandStep2);
            
            // When:
            // 玩家A 放置 中奇雞 至 棋盤(1, 1) 位置
            var commandStep3 = new PutCockCommand(_playerA.Id, 2, new Location(1, 1));
            
            // Then:
            // 不合法
            Assert.Throws<ArgumentOutOfRangeException>(() => _game.PutCock(commandStep3), "Illegal put cock");
        }
    }
}
