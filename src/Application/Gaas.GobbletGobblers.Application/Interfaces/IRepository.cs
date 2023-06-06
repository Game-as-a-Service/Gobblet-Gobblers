using Gaas.GobbletGobblers.Domain;

namespace Gaas.GobbletGobblers.Application.Interfaces
{
    public interface IRepository
    {
        Game Add(Guid id, Game game);

        Game Update(Guid id, Game game);

        Game Find(Guid gameid);
    }
}