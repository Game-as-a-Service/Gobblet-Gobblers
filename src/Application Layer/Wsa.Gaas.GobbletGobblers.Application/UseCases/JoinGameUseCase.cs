using Wsa.Gaas.GobbletGobblers.Application.Interfaces;
using Wsa.Gaas.GobbletGobblers.Domain;

namespace Wsa.Gaas.GobbletGobblers.Application.UseCases
{
    public class JoinGameUseCase
    {
        public async Task<GameModel> ExecuteAsync(JoinGameRequest request, IRepository repository)
        {
            // 查
            var game = repository.Find(request.Id);

            if (game == null)
                throw new Exception();

            // 改
            var player = new Player();
            player.Nameself(request.PlayerName);
            game.JoinPlayer(player);

            game.Start();

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
            };

            repository.Update(request.Id, game);

            // 推

            return await Task.FromResult(gameModel);
        }
    }
}
