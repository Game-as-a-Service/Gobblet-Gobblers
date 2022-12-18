using Gobblet_Gobblers.Shared;
using Gobblet_Gobblers.Shared.Enums;
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
        public Guid Create(Player player)
        {
            var guid = Guid.NewGuid();
            player = new Player()
                .Nameself(player.Name)
                .AddCocks(Cock.StandardEditionCocks(Color.Orange));

            var game = new Checkerboard(3)
                .JoinPlayer(player);

            if (Program._games.TryAdd(guid, game))
            {
                return guid;
            }
            else
            {
                //error
            }

            return guid;
        }

        [HttpPost]
        [Route("Join/{gameId}")]
        public Guid Join(Guid gameId, [FromBody] Player player)
        {
            if (Program._games.TryGetValue(gameId, out Checkerboard game) && !game.IsFull())
            {
                player = new Player()
                    .Nameself(player.Name)
                    .AddCocks(Cock.StandardEditionCocks(Color.Blue));

                game.JoinPlayer(player);
                game.Print();
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
            if (Program._games.TryGetValue(gameId, out Checkerboard game))
            {
                var player = game.GetPlayer(placeEvent.Player.Name);
                var cock = player.GetCock(placeEvent.Index);
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
            if (Program._games.TryGetValue(gameId, out Checkerboard game))
            {
                var player = game.GetPlayer(placeEvent.Player.Name);
                var cock = player.GetCock(placeEvent.Index);
                game.Move(placeEvent.Index, placeEvent.Location);

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
            public Player Player { get; set; }

            public int Index { get; set; }

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