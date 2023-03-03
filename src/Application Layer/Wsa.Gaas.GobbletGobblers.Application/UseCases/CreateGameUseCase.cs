using Wsa.Gaas.GobbletGobblers.Application.Interfaces;
using Wsa.Gaas.GobbletGobblers.Domain;

namespace Wsa.Gaas.GobbletGobblers.Application.UseCases
{
    public class CreateGameUseCase
    {
        public async Task ExecuteAsync(CreateGameRequest request, IRepository repository)
        {
            var gameId = Guid.NewGuid();

            // 查
            var game = repository.Find(gameId);

            if (game != null)
                throw new Exception();
            // 改
            var player = new Player();
            player.Nameself(request.PlayerName);

            game = new Game();
            game.JoinPlayer(player);

            // 存
            repository.Add(game);

            // 推
            await Task.CompletedTask;
        }
    }
}
