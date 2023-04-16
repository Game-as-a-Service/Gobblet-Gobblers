using Microsoft.AspNetCore.Mvc;
using Wsa.Gaas.GobbletGobblers.Application;
using Wsa.Gaas.GobbletGobblers.Application.Interfaces;
using Wsa.Gaas.GobbletGobblers.Application.UseCases;

namespace Wsa.Gaas.GobbletGobblers.WebApi.Controllers
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
            return await new PutCockUseCase().ExecuteAsync(request, _repository);
        }

        [HttpPost]
        [Route("MoveCock")]
        public async Task<GameModel> MoveCockAsync(MoveCockRequest request)
        {
            return await new MoveCockUseCase().ExecuteAsync(request, _repository);
        }
    }
}