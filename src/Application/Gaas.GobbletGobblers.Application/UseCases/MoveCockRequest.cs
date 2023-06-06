using Gaas.GobbletGobblers.Domain;

namespace Gaas.GobbletGobblers.Application.UseCases
{
    public class MoveCockRequest
    {
        public Guid Id { get; set; }

        public Guid PlayerId { get; set; }

        public Location From { get; set; }

        public Location To { get; set; }
    }
}