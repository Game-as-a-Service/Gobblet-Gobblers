using Gaas.GobbletGobblers.Application;
using Gaas.GobbletGobblers.Application.Interfaces;
using Gaas.GobbletGobblers.Application.UseCases;
using Gaas.GobbletGobblers.Domain;
using Gaas.GobbletGobblers.Domain.Enums;
using Microsoft.AspNetCore.Mvc;

namespace Gaas.GobbletGobblers.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GameController : ControllerBase
    {
        private readonly ILogger<GameController> _logger;

        private readonly IRepository _repository;

        public GameController(ILogger<GameController> logger, IRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        [HttpPost]
        [Route("~/games")]
        public async Task<dynamic> GamesAsync(GameRequest request)
        {
            if (request.Players.Count != 2)
            {
                throw new Exception("Requires at least two players to play");
            }

            var createGameRequest = new CreateGameRequest
            {
                PlayerId = new Guid(Convert.FromBase64String(request.Players[0].Id).Take(16).ToArray()),
                PlayerName = request.Players[0].Nickname
            };

            var createGameResponse = await new CreateGameUseCase().ExecuteAsync(createGameRequest, _repository);

            var joinGameRequest = new JoinGameRequest
            {
                Id = createGameResponse.Id,
                PlayerId = new Guid(Convert.FromBase64String(request.Players[1].Id).Take(16).ToArray()),
                PlayerName = request.Players[1].Nickname
            };
            _ = await new JoinGameUseCase().ExecuteAsync(joinGameRequest, _repository);

            return new { url = $"https://oneheed.github.io/Gobblet-Gobblers/games/{createGameResponse.Id}" };
        }

        [HttpPost]
        [Route("Create")]
        public async Task<GameModel> CreateAsync(CreateGameRequest request)
        {
            return await new CreateGameUseCase().ExecuteAsync(request, _repository);
        }

        [HttpPost]
        [Route("Join")]
        public async Task<GameModel> JoinAsync(JoinGameRequest request)
        {
            return await new JoinGameUseCase().ExecuteAsync(request, _repository);
        }

        [HttpPost]
        [Route("PutCock")]
        public async Task<GameModel> PutCockAsync(PutCockRequest request)
        {
            var game = await new PutCockUseCase().ExecuteAsync(request, _repository);

            //_logger.LogInformation($"{game.Players.FirstOrDefault(x => x.Id == request.PlayerId)?.Name} Put Cock Success");

            ShowCheckBoard(game.BoardSize, game.Board);

            return game;
        }

        [HttpPost]
        [Route("MoveCock")]
        public async Task<GameModel> MoveCockAsync(MoveCockRequest request)
        {
            var game = await new MoveCockUseCase().ExecuteAsync(request, _repository);

            //_logger.LogInformation($"{game.Players.FirstOrDefault(x => x.Id == request.PlayerId)?.Name} Move Cock Success");

            ShowCheckBoard(game.BoardSize, game.Board);

            return game;
        }

        [HttpPost]
        [Route("GameInfo")]
        public async Task<GameModel> GameInfoAsync(GameInfoRequest request)
        {
            var game = await new GameInfoUseCase().ExecuteAsync(request, _repository);

            return game;
        }

        private void ShowCheckBoard(int checkerboardSize, Stack<Cock>[] board)
        {
            var bound = string.Join("\u3000", Enumerable.Range(0, checkerboardSize).Select(x => "¡X"));
            Console.WriteLine($"\u3000{bound}\u3000");

            for (var i = 0; i < board.Length; i++)
            {
                var cocks = board[i];

                if ((i + 1) % checkerboardSize == 1)
                {
                    Console.Write("¡U");
                }

                if (cocks.TryPeek(out var cock))
                    ShowCocks(cock);
                else
                    Console.Write("\u3000");

                Console.Write("¡U");

                if ((i + 1) % checkerboardSize == 0)
                {
                    Console.WriteLine();
                    Console.WriteLine($"\u3000{bound}\u3000");
                }
            }
        }

        private void ShowCocks(Cock cock)
        {
            var consoleColor = ConsoleColor.Black;

            var color = cock.Color;
            var symbol = cock.Size.Symbol;

            switch (color)
            {
                case Color.Orange:
                    consoleColor = ConsoleColor.Red;
                    break;
                case Color.Blue:
                    consoleColor = ConsoleColor.Blue;
                    break;
            }

            Console.ForegroundColor = consoleColor;
            Console.Write(symbol);
            Console.ResetColor();
        }
    }
}