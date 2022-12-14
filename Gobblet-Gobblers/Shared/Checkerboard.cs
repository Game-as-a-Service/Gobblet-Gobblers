namespace Gobblet_Gobblers.Shared
{
    public class Checkerboard
    {
        private static readonly int _playerNumberLimit = 2;

        private readonly int _checkerboardSize = 3;

        private readonly Stack<Cock>[] _board;

        protected readonly List<Player> _players = new List<Player>();

        private Player _winner;

        private readonly int[][] playerLines; // 垂直:3, 水平:3, 斜線:2

        public Checkerboard(int checkerboardSize)
        {
            // 一定要單數
            if (_checkerboardSize % 2 == 0)
            {
                // TODO: 錯誤訊息
            }

            _checkerboardSize = checkerboardSize;

            playerLines = new int[_playerNumberLimit][];
            for (int i = 0; i < playerLines.Length; i++)
            {
                playerLines[i] = new int[_checkerboardSize * 2 + 2];
            }


            this._board = InitIalCheckerboard(checkerboardSize);
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

        public Checkerboard JoinPlayer(Player player)
        {
            if (IsFull())
            {
                // TODO: 錯誤訊息
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

            playerLines[playerId][x] += diff;
            playerLines[playerId][y + _checkerboardSize] += diff;

            if (x == y)
                playerLines[playerId][6] += diff;

            if (x + y == this._checkerboardSize - 1)
                playerLines[playerId][7] += diff;
        }


        public Cock? GetCock(int index)
        {
            return _board[index].TryPeek(out var c) ? c : default(Cock);
        }

        public bool Gameover(int location)
        {
            var cock = _board[location].Peek();
            _winner = cock.Owner;

            return playerLines[0].Any(x => x == _checkerboardSize) || playerLines[1].Any(x => x == _checkerboardSize);
        }

        public void ShowWinner()
        {
            Console.WriteLine($"Winner:{this._winner.Name}");
        }
    }
}
