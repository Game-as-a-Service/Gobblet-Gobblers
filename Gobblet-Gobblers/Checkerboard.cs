namespace Gobblet_Gobblers
{
    public class Checkerboard
    {
        private readonly int checkerboardSize = 3;

        private readonly Stack<Cock>[] _board;

        private readonly Player[] _players;

        private Player _winner;

        private readonly Dictionary<int, int[]> _horizontal = new();

        private readonly Dictionary<int, int[]> _vertical = new();

        private readonly Dictionary<int, int[]> _incline = new();

        public Checkerboard(int checkerboardSize, Player[] players)
        {
            this._board = InitIalCheckerboard(checkerboardSize);
            this._players = players;

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
            for (var i = 0; i < checkerboardSize; i++)
            {
                _horizontal[i] = Enumerable.Range(i * checkerboardSize, checkerboardSize).ToArray();
            }

            // 縱
            for (var i = 0; i < checkerboardSize; i++)
            {
                _vertical[i] = Enumerable.Range(i, checkerboardSize).Select(x => i + (x - i) * checkerboardSize).ToArray();
            }

            // 斜
            _incline[0] = Enumerable.Range(0, checkerboardSize).Select(x => x + x * checkerboardSize).ToArray();
            _incline[checkerboardSize - 1] = Enumerable.Range(0, checkerboardSize).Select(x => checkerboardSize - x - 1 + x * checkerboardSize).ToArray();
        }

        public void Start()
        {
            Print();

            Process();

            ShowWinner();
        }

        private void Process()
        {
            while (true)
            {
                foreach (var player in _players)
                {
                    var isNext = false;
                    var fromIndex = 0;
                    var toIndex = 0;

                    while (!isNext)
                    {
                        Console.WriteLine($"{player.Name} [1]:Place, [2]:Move");
                        var control = Console.ReadLine();

                        if (control == "1")
                        {
                            player.Print();
                            var cockIndex = int.Parse(Console.ReadLine());
                            var cock = player.GetCock(cockIndex);

                            Console.WriteLine($"{player.Name} Pacle 0~9 Location");
                            toIndex = int.Parse(Console.ReadLine());

                            isNext = Place(cock, toIndex);

                            if (isNext)
                            {
                                player.RemoveCock(cockIndex);
                            }
                        }
                        else if (control == "2")
                        {
                            isNext = Move(fromIndex, toIndex);

                            if (Gameover(fromIndex))
                            {
                                return;
                            }
                        }
                    }

                    Print();

                    if (Gameover(toIndex))
                    {
                        return;
                    }
                }
            }
        }

        public void Print()
        {
            var bound = string.Join("\u3000", Enumerable.Range(0, this.checkerboardSize).Select(x => "—"));
            Console.WriteLine($"\u3000{bound}\u3000");

            for (var i = 0; i < this._board.Length; i++)
            {
                var cocks = this._board[i];

                if ((i + 1) % this.checkerboardSize == 1)
                {
                    Console.Write("｜");
                }

                if (cocks.TryPeek(out var cock))
                    cock.Print();
                else
                    Console.Write("\u3000");

                Console.Write("｜");

                if ((i + 1) % this.checkerboardSize == 0)
                {
                    Console.WriteLine();
                    Console.WriteLine($"\u3000{bound}\u3000");
                }
            }
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
            _winner = this._players.First(p => p.Color == cock.Color);

            // 橫
            var horizontalIndex = location / checkerboardSize;
            if (CheckCondition(_horizontal, horizontalIndex))
                return true;

            // 縱
            var verticalIndex = location % checkerboardSize;
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
