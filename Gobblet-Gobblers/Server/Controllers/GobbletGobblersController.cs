using Gobblet_Gobblers.Shared;
using Microsoft.AspNetCore.Mvc;

namespace Gobblet_Gobblers.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GobbletGobblersController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<GobbletGobblersController> _logger;

        public GobbletGobblersController(ILogger<GobbletGobblersController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        [Route("Create")]
        public Guid Create(string playerName)
        {
            // 查

            // 改
            var guid = Guid.NewGuid();
            var player = new Player().Nameself(playerName);
            var game = new Checkerboard(3).JoinPlayer(player);

            if (!Program._games.TryAdd(guid, game))
            {
                // error
            }

            // 存

            // 推

            return guid;
        }

        [HttpPost]
        [Route("Join/{gameId}")]
        public Guid Join(Guid gameId, [FromBody] string playerName)
        {
            if (Program._games.TryGetValue(gameId, out Checkerboard? game) && !game.IsFull())
            {
                var player = new Player().Nameself(playerName);
                game.JoinPlayer(player);

                if (game.IsFull())
                {
                    game.Start();

                    // TODO:修改成不相依
                    game.Print();
                }
            }
            else
            {
                //error
            }

            return gameId;
        }

        [HttpPost]
        [Route("Place/{gameId}")]
        public Guid Join(Guid gameId, [FromBody] PlaceEvent placeEvent)
        {
            if (Program._games.TryGetValue(gameId, out Checkerboard? game))
            {
                var player = game.GetPlayer(placeEvent.PlayerId);
                var cock = player.GetCock(placeEvent.CockIndex);
                game.Place(cock, placeEvent.Location);

                game.Print();
            }
            else
            {
                //error
            }

            return gameId;
        }

        [HttpPost]
        [Route("Move/{gameId}")]
        public Guid Move(Guid gameId, [FromBody] PlaceEvent placeEvent)
        {
            if (Program._games.TryGetValue(gameId, out Checkerboard? game))
            {
                var player = game.GetPlayer(placeEvent.Player.Name);
                var cock = player.GetCock(placeEvent.CockIndex);
                game.Move(placeEvent.CockIndex, placeEvent.Location);

                game.Print();
            }
            else
            {
                //error
            }

            return gameId;
        }

        public class PlaceEvent
        {
            public Guid PlayerId { get; set; }

            public int CockIndex { get; set; }

            public int Location { get; set; }
        }

        public class MoveEvent
        {
            public Guid PlayerId { get; set; }

            public int CockIndex { get; set; }

            public int Location { get; set; }
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}