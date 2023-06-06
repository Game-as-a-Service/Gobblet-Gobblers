using Gaas.GobbletGobblers.Domain;

namespace Gaas.GobbletGobblers.Application
{
    public class PlayerModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public ICollection<Cock> Cocks { get; set; } = new List<Cock>();
    }
}
