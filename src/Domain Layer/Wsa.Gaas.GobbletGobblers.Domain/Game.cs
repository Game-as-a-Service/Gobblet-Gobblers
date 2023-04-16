using System;
using System.Collections.Generic;
using System.Linq;
using Wsa.Gaas.GobbletGobblers.Domain.Commands;
using Wsa.Gaas.GobbletGobblers.Domain.Enums;
using Wsa.Gaas.GobbletGobblers.Domain.Events;

namespace Wsa.Gaas.GobbletGobblers.Domain
{
    public class Game
    {
        private int _round = 0;

        private static readonly int _playerNumberLimit = 2;

        private readonly int _checkerboardSize = 3;

        private readonly Stack<Cock>[] _board;

        protected readonly List<Player> _players = new List<Player>();

        protected readonly Dictionary<Guid, Line> _lines = new Dictionary<Guid, Line>();

        private Guid? _winnerId;

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
                _players[1].AddCocks(Cock.StandardEditionCocks(Color.Blue));


                _lines.Add(_players[0].Id, new Line(this._checkerboardSize));
                _lines.Add(_players[1].Id, new Line(this._checkerboardSize));
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

        public Stack<Cock>[] GetBoard()
        {
            return this._board;
        }

        public List<Player> GetPlayers()
        {
            return this._players;
        }

        public Dictionary<Guid, Line> GetLines()
        {
            return this._lines;
        }

        private void CheckPlayerIsAction(Guid playerId)
        {
            if (this.CurrentPlayerId != playerId)
            {
                throw new ArgumentOutOfRangeException(nameof(playerId), "Is not current action player");
            }
        }

        public PutCockEvent PutCock(PutCockCommand command)
        {
            CheckPlayerIsAction(command.PlayerId);

            var domainEvent = new PutCockEvent();
            var player = GetPlayer(command.PlayerId);
            var cock = player.GetHandCock(command.HandCockIndex);
            var boardIndex = command.Location.X + this.CheckerboardSize * command.Location.Y;

            if (!_board[boardIndex].TryPeek(out var currentCock) || currentCock.CompareTo(cock) < 0)
            {
                _board[boardIndex].Push(cock);

                if (currentCock != null)
                {
                    if (currentCock.Owner == null)
                        throw new Exception();

                    var currentId = currentCock.Owner.Id;
                    _lines.TryGetValue(currentId, out Line currentLine);
                    currentLine.SetLine(command.Location, -1);
                }

                _lines.TryGetValue(command.PlayerId, out Line line);
                line.SetLine(command.Location, 1);

                if (line.IsLine())
                    this._winnerId = command.PlayerId;

                player.RemoveHandCock(command.HandCockIndex);

                _round++;
            }
            else
            {
                throw new ArgumentOutOfRangeException("Illegal put cock");
            }

            return domainEvent;
        }

        public MoveCockCockEvent MoveCock(MoveCockCommand command)
        {
            CheckPlayerIsAction(command.PlayerId);

            var domainEvent = new MoveCockCockEvent();
            var fromIndex = command.FromLocation.X + this.CheckerboardSize * command.FromLocation.Y;
            var toIndex = command.ToLocation.X + this.CheckerboardSize * command.ToLocation.Y;

            if (_board[fromIndex].TryPeek(out var tempfromCock) && tempfromCock.Owner?.Id == command.PlayerId)
            {
                if (!_board[toIndex].TryPeek(out var toCock) || toCock.CompareTo(tempfromCock) < 0)
                {
                    // 拿起要移動的奇雞
                    var fromCock = _board[fromIndex].Pop();
                    _lines.TryGetValue(command.PlayerId, out Line fromLine);
                    fromLine.SetLine(command.FromLocation, -1);


                    // 如果拿起奇雞, 底下有奇雞
                    if (_board[fromIndex].TryPeek(out var currentCock))
                    {
                        if (currentCock.Owner == null)
                            throw new Exception();

                        var currentId = currentCock.Owner.Id;
                        _lines.TryGetValue(currentId, out Line currentLine);
                        currentLine.SetLine(command.FromLocation, 1);

                        if (currentLine.IsLine())
                            this._winnerId = currentCock.Owner.Id;
                    }

                    if (this._winnerId == null)
                    {
                        // 放下奇雞到指定位置
                        _board[toIndex].Push(fromCock);
                        _lines.TryGetValue(command.PlayerId, out Line line);
                        line.SetLine(command.ToLocation, 1);

                        if (line.IsLine())
                            this._winnerId = command.PlayerId;
                    }

                    _round++;
                }
                else
                {
                    throw new ArgumentOutOfRangeException("Illegal move cock");
                }
            }
            else
            {
                throw new ArgumentOutOfRangeException("Illegal move cock");
            }

            return domainEvent;
        }

        public Cock? GetCock(Location location)
        {
            var index = location.X + this.CheckerboardSize * location.Y;

            return _board[index].TryPeek(out var c) ? c : default;
        }


        public bool Gameover()
        {
            return _winnerId.HasValue;
        }

        public Player GetWinner()
        {
            return GetPlayer(_winnerId!.Value);
        }
    }
}