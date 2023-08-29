namespace Gaas.GobbletGobblers.Application.UseCases
{
    public class CreateGameRequest
    {
        public Guid PlayerId { get; set; }

        public string PlayerName { get; set; }
    }
}