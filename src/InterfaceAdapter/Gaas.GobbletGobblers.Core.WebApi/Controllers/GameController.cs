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