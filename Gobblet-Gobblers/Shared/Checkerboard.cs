namespace Gobblet_Gobblers.Shared
{
    public class Checkerboard
    {
        private readonly int _checkerboardSize = 3;

        private readonly int _playerNumberLimit = 2;

        private readonly Stack<Cock>[] _board;

        protected readonly List<Player> _players = new List<Player>();

        private Player _winner;

        private readonly Dictionary<int, int[]> _horizontal = new();

        private readonly Dictionary<int, int[]> _vertical = new();

        private readonly Dictionary<int, int[]> _incline = new();

        public Checkerboard(int checkerboardSize)
        {
            this._board = InitIalCheckerboard(checkerboardSize);

            InitIalWinCondition();
        }

        private Stack<Cock>[] InitIalCheckerboard(int checkerboardSize)
        {
            var board = new Stack<Cock>[checkerboardSize * checkerboardSize];

            for (var i = 0; i < board.Length; i++)
            {
                board[i] = new Stack<Cock>();
            }

            return board;
        }

        private void InitIalWinCondition()
        {
            // 橫
            for (var i = 0; i < _checkerboardSize; i++)
            {
                _horizontal[i] = Enumerable.Range(i * _checkerboardSize, _checkerboardSize).ToArray();
            }

            // 縱
            for (var i = 0; i < _checkerboardSize; i++)
            {
                _vertical[i] = Enumerable.Range(i, _checkerboardSize).Select(x => i + (x - i) * _checkerboardSize).ToArray();
            }

            // 斜
            _incline[0] = Enumerable.Range(0, _checkerboardSize).Select(x => x + x * _checkerboardSize).ToArray();
            _incline[_checkerboardSize - 1] = Enumerable.Range(0, _checkerboardSize).Select(x => _checkerboardSize - x - 1 + x * _checkerboardSize).ToArray();
        }

        public Checkerboard JoinPlayer(Player player)
        {
            if (_players.Count == this._playerNumberLimit)
            {
                throw new Exception("遊戲已滿");
            }

            _players.Add(player);

            return this;
        }

        public bool IsFull()
        {
            return _players.Count == _playerNumberLimit;
        }

        public void ExitPlayer(Player player)
        {
            _players.Remove(player);
        }

        public void Print()
        {
            var bound = string.Join("\u3000", Enumerable.Range(0, this._checkerboardSize).Select(x => "—"));
            Console.WriteLine($"\u3000{bound}\u3000");

            for (var i = 0; i < this._board.Length; i++)
            {
                var cocks = this._board[i];

                if ((i + 1) % this._checkerboardSize == 1)
                {
                    Console.Write("｜");
                }

                if (cocks.TryPeek(out var cock))
                    cock.Print();
                else
                    Console.Write("\u3000");

                Console.Write("｜");

                if ((i + 1) % this._checkerboardSize == 0)
                {
                    Console.WriteLine();
                    Console.WriteLine($"\u3000{bound}\u3000");
                }
            }
        }

        public Player GetPlayer(string name)
        {
            return _players.FirstOrDefault(p => p.Name == name);
        }

        public bool Place(Cock cock, int location)
        {
            if (!_board[location].TryPeek(out var c) || c.CompareTo(cock) < 0)
            {
                _board[location].Push(cock);

                return true;
            }

            return false;
        }

        public bool Move(int fromIndex, int toIndex)
        {
            if (_board[fromIndex].TryPop(out var c) && Place(c, toIndex))
            {
                return true;
            }

            return false;
        }

        public Cock? GetCock(int index)
        {
            return _board[index].TryPeek(out var c) ? c : default(Cock);
        }

        public bool Gameover(int location)
        {
            var cock = _board[location].Peek();
            _winner = cock.Owner;

            // 橫
            var horizontalIndex = location / _checkerboardSize;
            if (CheckCondition(_horizontal, horizontalIndex))
                return true;

            // 縱
            var verticalIndex = location % _checkerboardSize;
            if (CheckCondition(_vertical, verticalIndex))
                return true;

            // 斜
            var incline1 = horizontalIndex - verticalIndex;
            var incline2 = horizontalIndex + verticalIndex;

            if (CheckCondition(_incline, incline1))
                return true;

            if (CheckCondition(_incline, incline2))
                return true;

            bool CheckCondition(Dictionary<int, int[]> conditionDic, int index)
            {
                if (conditionDic.TryGetValue(index, out var indexs) &&
                    indexs.All(i => _board[i].TryPeek(out var c) && c.EqualsColor(cock)))
                {
                    return true;
                }

                return false;
            }

            return false;
        }

        public void ShowWinner()
        {
            Console.WriteLine($"Winner:{this._winner.Name}");
        }
    }
}
