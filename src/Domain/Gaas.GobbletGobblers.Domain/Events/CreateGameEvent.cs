using System;
using System.Collections.Generic;

namespace Gaas.GobbletGobblers.Domain.Events
{
    public class CreateGameEvent
    {
        public Guid GameId { get; set; }

        public List<Player> Players { get; set; }
    }
}
