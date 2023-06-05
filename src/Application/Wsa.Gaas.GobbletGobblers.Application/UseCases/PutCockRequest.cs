using Wsa.Gaas.GobbletGobblers.Domain;

namespace Wsa.Gaas.GobbletGobblers.Application.UseCases
{
    public class PutCockRequest
    {
        public Guid Id { get; set; }

        public Guid PlayerId { get; set; }

        public int HandCockIndex { get; set; }

        public Location Location { get; set; }
    }
}