using System;

namespace Wsa.Gaas.GobbletGobblers.Domain.Commands
{
    public class MoveCockCommand
    {
        public Guid PlayerId { get; private set; }

        public Location FromLocation { get; private set; }

        public Location ToLocation { get; private set; }

        public MoveCockCommand(Guid playerId, Location from, Location to)
        {
            PlayerId = playerId;
            FromLocation = from;
            ToLocation = to;
        }
    }
}