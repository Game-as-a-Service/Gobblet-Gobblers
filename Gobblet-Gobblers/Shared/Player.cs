namespace Gobblet_Gobblers.Shared
{
    public class Player
    {
        public string? Name { get; set; }

        private ICollection<Cock> _cocks = new List<Cock>();

        public Player()
        {
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

        public Cock GetCock(int index)
        {
            var cock = _cocks.ElementAt(index);

            return cock;
        }

        public void RemoveCock(int index)
        {
            _cocks.Remove(_cocks.ElementAt(index));
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
