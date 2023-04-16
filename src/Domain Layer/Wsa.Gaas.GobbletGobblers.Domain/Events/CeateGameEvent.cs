using System;
using System.Collections.Generic;

namespace Wsa.Gaas.GobbletGobblers.Domain.Events
{
    public class CeateGameEvent
    {
        public Guid GameId { get; set; }

        public List<Player> Players { get; set; }
    }
}
