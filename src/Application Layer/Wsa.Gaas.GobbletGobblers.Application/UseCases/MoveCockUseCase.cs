using Wsa.Gaas.GobbletGobblers.Application.Interfaces;
using Wsa.Gaas.GobbletGobblers.Domain.Commands;

namespace Wsa.Gaas.GobbletGobblers.Application.UseCases
{
    public class MoveCockUseCase
    {
        public async Task<GameModel> ExecuteAsync(MoveCockRequest request, IRepository repository)
        {
            // 查
            var game = repository.Find(request.Id);

            if (game == null)
                throw new Exception();

            // 改
            var command = new MoveCockCommand(request.PlayerId, request.From, request.To);
            game.MoveCock(command);

            // 存
            var players = game.Players.Select(x => new PlayerModel
            {
                Id = x.Id,
                Name = x.Name,
                Cocks = x.GetHandAllCock(),
            }).ToList();

            var gameModel = new GameModel
            {
                Id = request.Id,
                Board = game.Board,
                Players = players,
                Lines = game.Lines,
            };

            repository.Update(request.Id, game);

            // 推

            return await Task.FromResult(gameModel);
        }
    }
}
