using Gaas.GobbletGobblers.Domain;
using Gaas.GobbletGobblers.Domain.Enums;

namespace Gaas.GobbletGobblers.Application
{
    public class GameModel
    {
        public Guid Id { get; set; }

        public int BoardSize { get; set; }

        public Stack<Cock>[] Board { get; set; }

        public List<PlayerModel> Players { get; set; }

        public Dictionary<Guid, Line> Lines { get; set; }

        public Guid? WinnerId { get; set; }
    }

    public class GameViewModel
    {
        public Guid Id { get; set; }

        public int BoardSize { get; set; }

        public Stack<CockViewModel>[] Board { get; set; }

        public List<PlayerViewModel> Players { get; set; }

        public Guid? WinnerId { get; set; }
    }

    public class CockViewModel
    {
        public PlayerViewModel? Owner { get; set; }

        public Color Color { get; set; }

        public SizeViewModel Size { get; set; }

        public bool IsClick { get; set; }
    }

    public class PlayerViewModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public ICollection<CockViewModel> Cocks { get; set; } = new List<CockViewModel>();
    }

    public class SizeViewModel
    {
        public int Number { get; set; }

        public string Symbol { get; set; }
    }

    public class ErrorResult
    {
        public string Message { get; set; }
    }
}
