using Gobblet_Gobblers.Enums;
using Gobblet_Gobblers.Sizes;

namespace Gobblet_Gobblers
{
    internal class Checkerboard
    {
        private readonly int boardSize = 3;

        private readonly Stack<Cock>[] _board;

        private readonly Player[] _players;

        private Color _winColor;

        private readonly Dictionary<int, int[]> _horizontal = new Dictionary<int, int[]>();

        private readonly Dictionary<int, int[]> _vertical = new Dictionary<int, int[]>();

        private readonly Dictionary<int, int[]> _incline = new Dictionary<int, int[]>();

        internal Checkerboard()
        {
            _board = new Stack<Cock>[boardSize * boardSize];
            _players = new Player[2];


            _players[0] = new Player("Josh", new List<Cock>
            {
                new Cock(Color.Orange, new Small()),
                new Cock(Color.Orange, new Small()),
                new Cock(Color.Orange, new Medium()),
                new Cock(Color.Orange, new Medium()),
                new Cock(Color.Orange, new Large()),
                new Cock(Color.Orange, new Large()),
            });

            _players[1] = new Player("Tom", new List<Cock>
            {
                new Cock(Color.Blue, new Small()),
                new Cock(Color.Blue, new Small()),
                new Cock(Color.Blue, new Medium()),
                new Cock(Color.Blue, new Medium()),
                new Cock(Color.Blue, new Large()),
                new Cock(Color.Blue, new Large()),
            });

            _winColor = Color.Orange;

            for (var i = 0; i < _board.Length; i++)
            {
                _board[i] = new Stack<Cock>();
            }

            // 橫
            for (var i = 0; i < boardSize; i++)
            {
                _horizontal[i] = Enumerable.Range(i * boardSize, boardSize).ToArray();
            }

            // 縱
            for (var i = 0; i < boardSize; i++)
            {
                _vertical[i] = Enumerable.Range(i, boardSize).Select(x => i + (x - i) * boardSize).ToArray();
            }

            // 斜
            _incline[0] = Enumerable.Range(0, boardSize).Select(x => x + x * boardSize).ToArray();
            _incline[boardSize - 1] = Enumerable.Range(0, boardSize).Select(x => boardSize - x - 1 + x * boardSize).ToArray();
        }

        internal void Start()
        {
            Print();

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

        internal void Print()
        {
            var bound = string.Join("\u3000", Enumerable.Range(0, this.boardSize).Select(x => "—"));
            Console.WriteLine($"\u3000{bound}\u3000");

            for (var i = 0; i < this._board.Length; i++)
            {
                var cocks = this._board[i];

                if ((i + 1) % this.boardSize == 1)
                {
                    Console.Write("｜");
                }

                if (cocks.TryPeek(out var cock))
                    cock.Print();
                else
                    Console.Write("\u3000");

                Console.Write("｜");

                if ((i + 1) % this.boardSize == 0)
                {
                    Console.WriteLine();
                    Console.WriteLine($"\u3000{bound}\u3000");
                }
            }
        }

        internal bool Place(Cock cock, int location)
        {
            if (!_board[location].TryPeek(out var c) || c.CompareTo(cock) < 0)
            {
                _board[location].Push(cock);

                return true;
            }

            return false;
        }

        internal bool Move(int fromIndex, int toIndex)
        {
            if (_board[fromIndex].TryPop(out var c) && Place(c, toIndex))
            {
                return true;
            }

            return false;
        }

        internal bool Gameover(int location)
        {
            var cock = _board[location].Peek();
            _winColor = cock.Color;

            // 橫
            var horizontalIndex = location / boardSize;
            if (CheckCondition(_horizontal, horizontalIndex))
                return true;

            // 縱
            var verticalIndex = location % boardSize;
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
                    indexs.All(i => _board[i].TryPeek(out var c) && c.Equals(cock)))
                {
                    return true;
                }

                return false;
            }

            return false;
        }
    }
}
