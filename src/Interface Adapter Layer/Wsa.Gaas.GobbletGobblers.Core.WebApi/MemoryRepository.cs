using Microsoft.Extensions.Caching.Memory;
using Wsa.Gaas.GobbletGobblers.Application.Interfaces;
using Wsa.Gaas.GobbletGobblers.Domain;

namespace Wsa.Gaas.GobbletGobblers.Core.WebApi
{
    public class MemoryRepository : IRepository
    {
        private readonly IMemoryCache _memoryCache;

        public MemoryRepository(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        public Game Add(Guid id, Game game)
        {
            var result = _memoryCache.Set(id, game);

            return result;
        }

        public Game Update(Guid id, Game game)
        {
            var result = _memoryCache.Set(id, game);

            return result;
        }

        public Game Find(Guid gameId)
        {
            var result = default(Game);

            if (_memoryCache.TryGetValue(gameId, out var game))
            {
                result = (Game)game;
            }

            return result;
        }
    }
}
