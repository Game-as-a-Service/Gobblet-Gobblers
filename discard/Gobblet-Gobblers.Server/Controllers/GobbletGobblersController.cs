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
        public Game Create(string playerName)
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

            return new Game
            {
                GameId = guid,
                Players = game.GetPlayers(),
            };
        }

        [HttpPost]
        [Route("Join/{gameId}")]
        public Game Join(Guid gameId, [FromBody] JoinEvent joinEvent)
        {
            if (Program._games.TryGetValue(gameId, out Checkerboard? game) && !game.IsFull())
            {
                var player = new Player().Nameself(joinEvent.PlayerName);
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

            if (game == null)
                return new Game();

            var gameData = new Game
            {
                GameId = gameId,
                Players = game.GetPlayers(),
                Cocks = new List<List<Cock>>(),
            };

            foreach (var player in gameData.Players)
            {
                var cocks = player.GetCocks().ToList();
                cocks = cocks.Select(x => new Cock(x.Color, x.Size)).ToList();
                gameData.Cocks.Add(cocks);
            }

            return gameData;
        }

        [HttpPost]
        [Route("Place/{gameId}")]
        public Game Join(Guid gameId, [FromBody] PlaceEvent placeEvent)
        {
            var isGameOver = false;
            if (Program._games.TryGetValue(gameId, out Checkerboard? game))
            {
                var player = game.GetPlayer(placeEvent.PlayerId);
                var cock = player.GetCock(placeEvent.CockIndex);
                var isNext = game.Place(cock, placeEvent.Location);

                if (isNext)
                {
                    player.RemoveCock(placeEvent.CockIndex);
                }

                game.Print();

                isGameOver = game.Gameover(placeEvent.CockIndex);
            }
            else
            {
                //error
            }

            if (game == null)
                return new Game();

            var gameData = new Game
            {
                GameId = gameId,
                Players = game.GetPlayers(),
                Cocks = new List<List<Cock>>(),
                WinnerName = isGameOver ? game.GetWinner().Name : string.Empty
            };

            foreach (var player in gameData.Players)
            {
                var cocks = player.GetCocks().ToList();
                cocks = cocks.Select(x => new Cock(x.Color, x.Size)).ToList();
                gameData.Cocks.Add(cocks);
            }

            return gameData;
        }

        [HttpPost]
        [Route("Move/{gameId}")]
        public Game Move(Guid gameId, [FromBody] MoveEvent moveEvent)
        {
            var isGameOver = false;
            if (Program._games.TryGetValue(gameId, out Checkerboard? game))
            {
                var player = game.GetPlayer(moveEvent.PlayerId);
                game.Move(moveEvent.FormIndex, moveEvent.ToIndex);

                game.Print();

                isGameOver = game.Gameover(moveEvent.FormIndex);
                if (!isGameOver)
                {
                    game.Gameover(moveEvent.ToIndex);
                }
            }
            else
            {
                //error
            }

            var gameData = new Game
            {
                GameId = gameId,
                Players = game.GetPlayers(),
                Cocks = new List<List<Cock>>(),
                WinnerName = isGameOver ? game.GetWinner().Name : string.Empty
            };

            foreach (var player in gameData.Players)
            {
                var cocks = player.GetCocks().ToList();
                cocks = cocks.Select(x => new Cock(x.Color, x.Size)).ToList();
                gameData.Cocks.Add(cocks);
            }

            return gameData;
        }

        public class JoinEvent
        {
            public string PlayerName { get; set; }
        }

        public class Game
        {
            public Guid GameId { get; set; }

            public List<Player> Players { get; set; }

            public List<List<Cock>> Cocks { get; set; }

            public string WinnerName { get; set; }
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

            public int FormIndex { get; set; }

            public int ToIndex { get; set; }
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