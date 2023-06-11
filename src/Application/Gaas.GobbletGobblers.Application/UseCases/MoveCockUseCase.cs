using Gaas.GobbletGobblers.Application.Interfaces;
using Gaas.GobbletGobblers.Domain.Commands;

namespace Gaas.GobbletGobblers.Application.UseCases
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
                BoardSize = game.CheckerboardSize,
                Board = game.Board,
                Players = players,
                Lines = game.Lines,
                WinnerId = game.GetWinnerId(),
            };

            repository.Update(request.Id, game);

            // 推

            return await Task.FromResult(gameModel);
        }
    }
}
