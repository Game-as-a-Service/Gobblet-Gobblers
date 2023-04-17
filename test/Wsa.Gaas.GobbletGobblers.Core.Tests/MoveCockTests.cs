using Wsa.Gaas.GobbletGobblers.Domain;
using Wsa.Gaas.GobbletGobblers.Domain.Commands;

namespace Wsa.Gaas.Gobblet_Gobblers.Tests
{
    [TestFixture]
    public class MoveCockTests
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

        [Test]
        public void PlayerA_MoveCockLocation2_0ToLocation1_1()
        {
            // Given:
 

            // When:
            // 玩家A 放置 中奇雞 至 棋盤(2, 0) 位置
            var expected = _playerA.GetHandCock(2); // TODO: 測試的時候要知道玩家奇雞是什麼
            var commandStep1 = new PutCockCommand(_playerA.Id, 2, new Location(2, 0));
            _game.PutCock(commandStep1);

            var commandStep2 = new PutCockCommand(_playerB.Id, 4, new Location(0, 0));
            _game.PutCock(commandStep2);


            // var expected = playerA.GetHandCock(2); // TODO: 如果寫這裡會失敗
            var commandStep3 = new MoveCockCommand(_playerA.Id, new Location(2, 0), new Location(1, 1));
            _game.MoveCock(commandStep3);

            // Then:
            // 玩家A的 中奇雞 放置至 棋盤(1, 1)
            var fromActual = _game.GetCock(commandStep3.FromLocation);
            var toActual = _game.GetCock(commandStep3.ToLocation);

            Assert.That(fromActual, Is.Null);
            Assert.That(toActual, Is.EqualTo(expected));
        }

        [Test]
        public void PlayerA_MovePlayerBCockLocation2_0ToLocation1_1()
        {
         

            // When:
            // 玩家A 放置 中奇雞 至 棋盤(2, 0) 位置          
            var commandStep1 = new PutCockCommand(_playerA.Id, 2, new Location(0, 0));
            _game.PutCock(commandStep1);

            var commandStep2 = new PutCockCommand(_playerB.Id, 2, new Location(2, 0));
            _game.PutCock(commandStep2);

            var commandStep3 = new MoveCockCommand(_playerA.Id, new Location(2, 0), new Location(1, 1));

            // Then:
            // 玩家A的 中奇雞 放置至 棋盤(1, 1)
            Assert.Throws<ArgumentOutOfRangeException>(() => _game.MoveCock(commandStep3), "Illegal move cock");
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
            var commandStep1 = new PutCockCommand(playerA.Id, 2, new Location(2, 0));
            game.PutCock(commandStep1);

            var commandStep2 = new PutCockCommand(playerB.Id, 4, new Location(1, 1));
            game.PutCock(commandStep2);


            var commandStep3 = new MoveCockCommand(playerA.Id, new Location(2, 0), new Location(1, 1));
            game.MoveCock(commandStep3);

            // Then:
            // 玩家A的 中奇雞 放置至 棋盤(1, 1)
            var fromActual = game.GetCock(commandStep3.FromLocation);
            var toActual = game.GetCock(commandStep3.ToLocation);

            Assert.That(fromActual, Is.Null);
            Assert.That(toActual, Is.EqualTo(expected));
        }

        [Test]
        public void PlayerA_HavePlayerBMediumCockOnLocation1_1_MoveCockLocation2_0ToLocation1_1()
        {
            // Given:
            var commandStep1 = new PutCockCommand(_playerA.Id, 2, new Location(2, 0));
            _game.PutCock(commandStep1);

            var commandStep2 = new PutCockCommand(_playerB.Id, 2, new Location(1, 1));
            _game.PutCock(commandStep2);
            
            // When:
            // 玩家A 放置 中奇雞 至 棋盤(2, 0) 位置          
            var commandStep3 = new MoveCockCommand(_playerA.Id, new Location(2, 0), new Location(1, 1));

            // Then:
            // 玩家A的 中奇雞 放置至 棋盤(1, 1)
            Assert.Throws<ArgumentOutOfRangeException>(() => _game.MoveCock(commandStep3), "Illegal move cock");
        }
    }
}
