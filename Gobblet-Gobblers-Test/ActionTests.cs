using Gobblet_Gobblers.Enums;
using Gobblet_Gobblers.Sizes;

namespace Gobblet_Gobblers
{
    public class ActionTests
    {
        private Checkerboard _checkerboard;

        [SetUp]
        public void Setup()
        {
            var players = new Player[2];
            players[0] = new Player(Color.Orange).Nameself("Josh");
            players[1] = new Player(Color.Blue).Nameself("Tom");

            _checkerboard = new Checkerboard(3, players);
        }

        /// <summary>
        /// 
        /// </summary>
        [Test]
        [TestCase(0)]
        [TestCase(4)]
        public void PlacedSucess(int placeIndex)
        {
            var actual = new Cock(Enums.Color.Orange, new Medium());

            _checkerboard.Place(actual, placeIndex);

            var expected = _checkerboard.GetCock(placeIndex);

            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void PlacedFail()
        {
            var cock = new Cock(Enums.Color.Blue, new Medium());
            var actual = new Cock(Enums.Color.Orange, new Medium());

            _checkerboard.Place(cock, 4);
            _checkerboard.Place(actual, 4);

            var expected = _checkerboard.GetCock(4);

            Assert.That(actual, Is.Not.EqualTo(expected));
        }
    }
}