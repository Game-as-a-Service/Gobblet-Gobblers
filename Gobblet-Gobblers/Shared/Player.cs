namespace Gobblet_Gobblers.Shared
{
    public class Player
    {
        public Guid Id { get; private set; }

        public string? Name { get; private set; }

        private ICollection<Cock> _cocks = new List<Cock>();

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

        // TODO : 拔除
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
