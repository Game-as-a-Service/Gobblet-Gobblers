using Wsa.Gaas.GobbletGobblers.Application.Interfaces;
using Wsa.Gaas.GobbletGobblers.Domain.Commands;

namespace Wsa.Gaas.GobbletGobblers.Application.UseCases
{
    public class PutCockUseCase
    {
        public async Task<GameModel> ExecuteAsync(PutCockRequest request, IRepository repository)
        {
            // 查
            var game = repository.Find(request.Id);

            if (game == null)
                throw new Exception();

            // 改
            var command = new PutCockCommand(request.PlayerId, request.HandCockIndex, request.Location);
            game.PutCock(command);

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
