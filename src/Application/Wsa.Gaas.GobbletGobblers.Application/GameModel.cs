using Wsa.Gaas.GobbletGobblers.Domain;

namespace Wsa.Gaas.GobbletGobblers.Application
{
    public class GameModel
    {
        public Guid Id { get; set; }

        public int BoardSize { get; set; }

        public Stack<Cock>[] Board { get; set; }

        public List<PlayerModel> Players { get; set; }

        public Dictionary<Guid, Line> Lines { get; set; }
    }
}
