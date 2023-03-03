using Wsa.Gaas.GobbletGobblers.Domain.Enums;

namespace Wsa.Gaas.GobbletGobblers.Domain
{
    public class Game
    {
        private int _round = 0;

        private static readonly int _playerNumberLimit = 2;

        private readonly int _checkerboardSize = 3;

        private readonly Stack<Cock>[] _board;

        protected readonly List<Player> _players = new List<Player>();

        private Guid _winnerId;

        public Game(int checkerboardSize = 3)
        {
            // 一定要單數
            if (checkerboardSize % 2 == 0)
            {
                throw new ArgumentException("Checker Board Siz Argument Error");
            }

            this._checkerboardSize = checkerboardSize;
            this._board = InitlalCheckerboard(checkerboardSize);
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

        public bool IsFull()
        {
            return _players.Count == _playerNumberLimit;
        }

        public Game JoinPlayer(Player player)
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
            if (_players.Count == _playerNumberLimit)
            {
                _players[0].AddCocks(Cock.StandardEditionCocks(Color.Orange));
                _players[0].InitLines(this._checkerboardSize);

                _players[1].AddCocks(Cock.StandardEditionCocks(Color.Blue));
                _players[1].InitLines(this._checkerboardSize);
            }
            else
            {
                throw new AggregateException("Game is not full");
            }
        }

        public int CheckerboardSize => this._checkerboardSize;

        public Guid CurrentPlayerId => _players.ElementAt(_round % _playerNumberLimit).Id;


        public Stack<Cock>[] Board => this._board;

        public Player GetPlayer(Guid Id)
        {
            var player = _players.FirstOrDefault(p => p.Id == Id);

            if (player == default)
            {
                throw new ArgumentOutOfRangeException(nameof(Id), "Can not get player");
            }

            return player;
        }

        public List<Player> GetPlayers()
        {
            return this._players;
        }

        public PutCockEvent PutCock(PutCockCommand command)
        {
            var domainEvent = new PutCockEvent();
            var player = GetPlayer(command.PlayerId);
            var cock = player.GetCock(command.HandCockIndex);
            var boardIndex = command.Location.X + this.CheckerboardSize * command.Location.Y;

            if (!_board[boardIndex].TryPeek(out var currenctCock) || currenctCock.CompareTo(cock) < 0)
            {
                _board[boardIndex].Push(cock);

                if (currenctCock != null)
                {
                    if (currenctCock.Owner == null)
                        throw new Exception();

                    currenctCock.Owner
                        .GetLines().SetLine(command.Location, -1);
                }

                player
                    .GetLines().SetLine(command.Location, 1);

                player.RemoveCock(command.HandCockIndex);

                _round++;
            }

            return domainEvent;
        }

        //public bool Move(int fromIndex, int toIndex)
        //{
        //    if (_board[fromIndex].TryPeek(out Cock? temp) && temp?.Owner?.Id == this._currentPlayerId)
        //    {
        //        if (_board[fromIndex].TryPop(out Cock? c) && Place(c, toIndex))
        //        {
        //            var index = (int)temp.Color;
        //            SetPlayerLine(index, fromIndex, -1);
        //            SetPlayerLine(index, toIndex, 1);

        //            if (_board[fromIndex].TryPeek(out var c1))
        //            {
        //                var index1 = (int)c1.Color;
        //                SetPlayerLine(index1, fromIndex, 1);
        //            }

        //            _round++;

        //            return true;
        //        }
        //    }

        //    return false;
        //}


        public Cock? GetCock(int index)
        {
            return _board[index].TryPeek(out var c) ? c : default;
        }

        public bool Gameover()
        {
            foreach (var player in _players)
            {
                if (player.GetLines().IsLine())
                {
                    this._winnerId = player.Id;

                    return true;
                }
            }

            return false;
        }

        public Player GetWinner()
        {
            return GetPlayer(_winnerId);
        }
    }
}