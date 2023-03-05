using System;
using System.Collections.Generic;
using System.Linq;

namespace Wsa.Gaas.GobbletGobblers.Domain
{
    public class Player
    {
        public Guid Id { get; private set; }

        public string Name { get; private set; } = string.Empty;

        private ICollection<Cock> _cocks = new List<Cock>();

        private Line _lines;

        public Player()
        {
            this.Id = Guid.NewGuid();
        }

        public Player Nameself(string name)
        {
            this.Name = name;

            return this;
        }

        public Player AddCocks(IEnumerable<Cock> cocks)
        {
            foreach (var cock in cocks)
            {
                cock.Owner = this;

                this._cocks.Add(cock);
            }

            _cocks = _cocks.OrderByDescending(c => c.Size).ToList();

            return this;
        }

        public ICollection<Cock> GetCocks()
        {
            return _cocks;
        }

        public Cock GetCock(int index)
        {
            var cock = _cocks.ElementAtOrDefault(index);

            if (cock == default)
            {
                throw new ArgumentOutOfRangeException(nameof(index), "Can not get Cock");
            }

            return cock;
        }

        public void RemoveCock(int index)
        {
            _cocks.Remove(_cocks.ElementAt(index));
        }

        public void InitLines(int checkerboardSize)
        {
            _lines = new Line(checkerboardSize);
        }

        public Line GetLines()
        {
            return _lines;
        }
    }
}
