using Gobblet_Gobblers.Enums;

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
            foreach (var player in _players)
            {
                var control = 1;
                var fromIndex = 0;
                var toIndex = 0;

                if (control == 1)
                {
                    var cock = new Cock(Color.Orange, Size.Large);

                    Place(cock, toIndex);
                }
                else if (control == 2)
                {
                    Move(fromIndex, toIndex);
                    if (Gameover(fromIndex))
                    {
                        return;
                    }
                }

                if (Gameover(toIndex))
                {
                    return;
                }
            }
        }

        internal void Place(Cock cock, int location)
        {
            if (!_board[location].TryPeek(out var c) || c.CompareTo(cock) < 0)
            {
                _board[location].Push(cock);
            }
        }

        internal void Move(int fromIndex, int toIndex)
        {
            if (_board[fromIndex].TryPop(out var c))
            {
                Place(c, toIndex);
            }
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
