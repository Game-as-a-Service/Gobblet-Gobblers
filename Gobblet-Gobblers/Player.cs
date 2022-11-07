using Gobblet_Gobblers.Enums;

namespace Gobblet_Gobblers
{
    public class Player
    {
        public Color Color { get; private set; }

        public string? Name { get; private set; }

        private ICollection<Cock> _cocks = new List<Cock>();

        public Player(Color color)
        {
            this.Color = color;
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
                this._cocks.Add(cock);
            }

            _cocks = _cocks.OrderByDescending(c => c.Color).ToList();

            return this;
        }

        public Cock GetCock(int index)
        {
            var cock = _cocks.ElementAt(index);

            return cock;
        }

        public void RemoveCock(int index)
        {
            _cocks.Remove(_cocks.ElementAt(index));
        }


        public void AddCock(Cock cock)
        {
            this.AddCocks(new List<Cock> { cock });
        }

        public void Print()
        {
            for (int i = 0; i < this._cocks.Count; i++)
            {
                Console.Write($"[{i}]:");
                this._cocks.ElementAt(i).Print();
                Console.Write($" ");
            }

            Console.WriteLine();
        }
    }
}
