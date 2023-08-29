namespace Gaas.GobbletGobblers.Application.UseCases
{
    public class JoinGameRequest
    {
        public Guid Id { get; set; }

        public Guid? PlayerId { get; set; }

        public string PlayerName { get; set; }
    }
}