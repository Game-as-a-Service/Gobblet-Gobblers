using Gobblet_Gobblers.Shared.Enums;

namespace Gobblet_Gobblers.Shared
{
    public class Checkerboard
    {
        private static readonly int _playerNumberLimit = 2;

        private readonly int _checkerboardSize = 3;

        private readonly Stack<Cock>[] _board;

        protected readonly List<Player> _players = new List<Player>();

        private readonly int[][] _playerLines; // 垂直:3, 水平:3, 斜線:2

        private Player _winner;

        public Checkerboard(int checkerboardSize = 3)
        {
            // 一定要單數
            if (checkerboardSize % 2 == 0)
            {
                throw new ArgumentException("Checker Board Siz Argument Error");
            }

            this._checkerboardSize = checkerboardSize;

            this._board = InitlalCheckerboard(checkerboardSize);
            this._playerLines = InitlalPlayerLines();
        }

        private Stack<Cock>[] InitlalCheckerboard(int checkerboardSize)
        {
            var board = new Stack<Cock>[checkerboardSize * checkerboardSize];

            for (var i = 0; i < board.Length; i++)
            {
                board[i] = new Stack<Cock>();
            }

            return board;
        }

        private int[][] InitlalPlayerLines()
        {
            var lines = new int[_playerNumberLimit][];

            for (int i = 0; i < lines.Length; i++)
            {
                lines[i] = new int[_checkerboardSize * 2 + 2];
            }

            return lines;
        }

        public bool IsFull()
        {
            return _players.Count == _playerNumberLimit;
        }

        public Checkerboard JoinPlayer(Player player)
        {
            if (IsFull())
            {
                throw new ArgumentException("Game is full");
            }

            _players.Add(player);

            return this;
        }

        public void ExitPlayer(Player player)
        {
            _players.Remove(player);
        }

        public void Start()
        {
            if (_players.Count == 2)
            {
                _players[0].AddCocks(Cock.StandardEditionCocks(Color.Orange));
                _players[1].AddCocks(Cock.StandardEditionCocks(Color.Blue));
            }
            else
            {
                throw new AggregateException("Game is not full");
            }
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

        public Player GetPlayer(Guid Id)
        {
            var player = _players.FirstOrDefault(p => p.Id == Id);

            if (player == default)
            {
                throw new ArgumentOutOfRangeException(nameof(Id), "Can not get player");
            }

            return player;
        }

        public bool Place(Cock cock, int location)
        {
            if (!_board[location].TryPeek(out var c) || c.CompareTo(cock) < 0)
            {
                _board[location].Push(cock);


                if (c != null)
                {
                    var index1 = (int)c.Color;
                    SetPlayerLine(index1, location, -1);
                }

                var index = (int)cock.Color;
                SetPlayerLine(index, location, 1);

                return true;
            }

            return false;
        }

        public bool Move(int fromIndex, int toIndex)
        {
            if (_board[fromIndex].TryPop(out var c) && Place(c, toIndex))
            {
                var index = (int)c.Color;
                SetPlayerLine(index, fromIndex, -1);
                SetPlayerLine(index, toIndex, 1);

                if (_board[fromIndex].TryPeek(out var c1))
                {
                    var index1 = (int)c1.Color;
                    SetPlayerLine(index1, fromIndex, 1);
                }

                return true;
            }

            return false;
        }

        public void SetPlayerLine(int playerId, int location, int diff)
        {
            var x = location / this._checkerboardSize;
            var y = location % this._checkerboardSize;

            _playerLines[playerId][x] += diff;
            _playerLines[playerId][y + _checkerboardSize] += diff;

            if (x == y)
                _playerLines[playerId][6] += diff;

            if (x + y == this._checkerboardSize - 1)
                _playerLines[playerId][7] += diff;
        }


        public Cock? GetCock(int index)
        {
            return _board[index].TryPeek(out var c) ? c : default(Cock);
        }

        public bool Gameover(int location)
        {
            var cock = _board[location].Peek();
            _winner = cock.Owner;

            return _playerLines[0].Any(x => x == _checkerboardSize) || _playerLines[1].Any(x => x == _checkerboardSize);
        }

        public void ShowWinner()
        {
            Console.WriteLine($"Winner:{this._winner.Name}");
        }
    }
}
