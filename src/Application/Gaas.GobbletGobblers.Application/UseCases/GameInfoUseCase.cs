using Gaas.GobbletGobblers.Application.Interfaces;

namespace Gaas.GobbletGobblers.Application.UseCases
{
    public class GameInfoUseCase
    {
        public async Task<GameModel> ExecuteAsync(GameInfoRequest request, IRepository repository)
        {
            // 查
            var game = repository.Find(request.Id);

            if (game == null)
                throw new Exception();

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


            // 推

            return await Task.FromResult(gameModel);
        }
    }
}
