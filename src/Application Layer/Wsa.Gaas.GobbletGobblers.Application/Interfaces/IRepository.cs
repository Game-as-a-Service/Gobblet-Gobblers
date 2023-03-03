using Wsa.Gaas.GobbletGobblers.Domain;

namespace Wsa.Gaas.GobbletGobblers.Application.Interfaces
{
    public interface IRepository
    {
        bool Add(Game game);

        Game Find(Guid gameid);
    }
}