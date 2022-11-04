namespace Gobblet_Gobblers
{
    internal class Player
    {
        internal string Name { get; private set; }

        private IList<Cock> _cocks;

        internal Player(string name, IEnumerable<Cock> cocks)
        {
            Name = name;
            _cocks = cocks.ToList();
        }

        internal Cock GetCock(int index)
        {
            var cock = _cocks[index];

            return cock;
        }

        internal void RemoveCock(int index)
        {
            _cocks.RemoveAt(index);
        }


        internal void SetCock(Cock cock)
        {
            this._cocks.Add(cock);
            this._cocks = this._cocks.OrderBy(c => c.Size).ToList();
        }

        internal void Print()
        {
            for (int i = 0; i < this._cocks.Count; i++)
            {
                Console.Write($"[{i}]:");
                this._cocks[i].Print();
                Console.Write($" ");
            }

            Console.WriteLine();
        }
    }
}
