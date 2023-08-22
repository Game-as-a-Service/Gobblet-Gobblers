namespace Gaas.GobbletGobblers.Application.UseCases
{
    public class GameRequest
    {
        public List<GamePlayer> Players { get; set; }
    }

    public class GamePlayer
    {
        public string Id { get; set; }

        public string Nickname { get; set; }
    }
}