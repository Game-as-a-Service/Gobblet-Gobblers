using Gaas.GobbletGobblers.Domain;
using Gaas.GobbletGobblers.Domain.Commands;

namespace Gaas.Gobblet_Gobblers.Tests
{
    [TestFixture]
    public class LineTests
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
        public void PlayerA_HorizontalLine()
        {
            // Given:
            var commandStep1 = new PutCockCommand(_playerA.Id, 0, new Location(1, 0));
            _game.PutCock(commandStep1);

            var commandStep2 = new PutCockCommand(_playerB.Id, 0, new Location(0, 0));
            _game.PutCock(commandStep2);

            var commandStep3 = new PutCockCommand(_playerA.Id, 0, new Location(1, 1));
            _game.PutCock(commandStep3);

            var commandStep4 = new PutCockCommand(_playerB.Id, 0, new Location(0, 1));
            _game.PutCock(commandStep4);

            // When:
            // 玩家A 放置 中奇雞 至 棋盤(2, 0) 位置
           

            var commandStep5 = new PutCockCommand(_playerA.Id, 0, new Location(1, 2));
            _game.PutCock(commandStep5);

            // Then:
            // 玩家A的 中奇雞 放置至 棋盤(1, 1)
            Assert.That(_game.Gameover(), Is.True);
            Assert.That(_game.GetWinner(), Is.EqualTo(_playerA));
        }

        [Test]
        public void PlayerA_VerticalLine()
        {
            // Given:
            var commandStep1 = new PutCockCommand(_playerA.Id, 0, new Location(0, 0));
            _game.PutCock(commandStep1);

            var commandStep2 = new PutCockCommand(_playerB.Id, 0, new Location(0, 1));
            _game.PutCock(commandStep2);

            var commandStep3 = new PutCockCommand(_playerA.Id, 0, new Location(1, 0));
            _game.PutCock(commandStep3);

            var commandStep4 = new PutCockCommand(_playerB.Id, 0, new Location(0, 2));
            _game.PutCock(commandStep4);

            // When:
            // 玩家A 放置 中奇雞 至 棋盤(2, 0) 位置
            
            var commandStep5 = new PutCockCommand(_playerA.Id, 0, new Location(2, 0));
            _game.PutCock(commandStep5);

            // Then:
            // 玩家A的 中奇雞 放置至 棋盤(1, 1)
            Assert.That(_game.Gameover(), Is.True);
            Assert.That(_game.GetWinner(), Is.EqualTo(_playerA));
        }

        [Test]
        public void PlayerA_SlashLine()
        {
          
            // Given:
            // 玩家A, 玩家B
            var commandStep1 = new PutCockCommand(_playerA.Id, 0, new Location(0, 0));
            _game.PutCock(commandStep1);

            var commandStep2 = new PutCockCommand(_playerB.Id, 0, new Location(0, 1));
            _game.PutCock(commandStep2);


            var commandStep3 = new PutCockCommand(_playerA.Id, 0, new Location(1, 1));
            _game.PutCock(commandStep3);

            var commandStep4 = new PutCockCommand(_playerB.Id, 0, new Location(0, 2));
            _game.PutCock(commandStep4);
            // When:
            // 玩家A 放置 中奇雞 至 棋盤(2, 0) 位置
            
            var commandStep5 = new PutCockCommand(_playerA.Id, 0, new Location(2, 2));
            _game.PutCock(commandStep5);

            // Then:
            // 玩家A的 中奇雞 放置至 棋盤(1, 1)
            Assert.That(_game.Gameover(), Is.True);
            Assert.That(_game.GetWinner(), Is.EqualTo(_playerA));
        }

        [Test]
        public void PlayerA_NotLine()
        {
           
            // Given:
            var commandStep1 = new PutCockCommand(_playerA.Id, 0, new Location(0, 0));
            _game.PutCock(commandStep1);

            var commandStep2 = new PutCockCommand(_playerB.Id, 0, new Location(1, 1));
            _game.PutCock(commandStep2);
            
            // When:
            // 玩家A 放置 中奇雞 至 棋盤(2, 0) 位置
            var commandStep3 = new PutCockCommand(_playerA.Id, 0, new Location(2, 2));
            _game.PutCock(commandStep3);

            // Then:
            // 玩家A的 中奇雞 放置至 棋盤(1, 1)
            Assert.That(_game.Gameover(), Is.False);
        }
    }
}
