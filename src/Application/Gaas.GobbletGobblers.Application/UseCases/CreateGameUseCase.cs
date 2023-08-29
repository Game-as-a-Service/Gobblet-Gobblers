using Gaas.GobbletGobblers.Application.Interfaces;
using Gaas.GobbletGobblers.Domain;

namespace Gaas.GobbletGobblers.Application.UseCases
{
    public class CreateGameUseCase
    {
        public async Task<GameModel> ExecuteAsync(CreateGameRequest request, IRepository repository)
        {
            var gameId = Guid.NewGuid();

            // 查
            var game = repository.Find(gameId);

            if (game != null)
                throw new Exception();

            // 改
            var player = new Player(request.PlayerId);
            player.Nameself(request.PlayerName);

            game = new Game();
            game.JoinPlayer(player);

            // 存
            var players = game.Players.Select(x => new PlayerModel
            {
                Id = x.Id,
                Name = x.Name,
                Cocks = x.GetHandAllCock(),
            }).ToList();

            var gameModel = new GameModel
            {
                Id = gameId,
                BoardSize = game.CheckerboardSize,
                Board = game.Board,
                Players = players,
                Lines = game.Lines,
            };

            repository.Add(gameId, game);

            // 推

            return await Task.FromResult(gameModel);
        }
    }
}
