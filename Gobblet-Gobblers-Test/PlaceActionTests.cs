using Gobblet_Gobblers.Shared;
using Gobblet_Gobblers.Shared.Enums;
using Gobblet_Gobblers.Shared.Sizes;

namespace Gobblet_Gobblers
{
    public class PlaceActionTests
    {
        private Checkerboard _checkerboard;

        [SetUp]
        public void Setup()
        {
            _checkerboard = new Checkerboard(3);
        }

        private static readonly object[] _sourceLists =
        {
            new List<Cock> { new Cock(Color.Blue, new Small()) }
        };

        /// <summary>
        /// 
        /// </summary>
        [Test]
        public void PlacedSucess()
        {
            var placeIndex = 4;
            var cocks = new List<Cock> { new Cock(Color.Blue, new Small()) };
            var playersA = new Player()
                .Nameself("Josh")
                .AddCocks(cocks);

            _checkerboard.JoinPlayer(playersA);
            var actual = playersA.GetCock(0);

            _checkerboard.Place(actual, placeIndex);

            var expected = _checkerboard.GetCock(placeIndex);

            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void PlacedFail()
        {
            var placeIndex = 4;
            var playersA = new Player()
                .Nameself("Josh");

            _checkerboard.JoinPlayer(playersA);
            var actual = playersA.GetCock(0);

            Assert.IsNull(actual);
        }

        [Test]
        public void PlacedSucess1()
        {
            var playersA = new Player()
                    .Nameself("Josh");

            Assert.IsNull(playersA.GetCock(0));
        }
    }
}