using System.Linq;

namespace Wsa.Gaas.GobbletGobblers.Domain
{
    public class Line
    {
        private readonly int _checkerboardSize;

        private readonly int[] _data;


        public Line(int checkerboardSize)
        {
            // ex: _checkerboardSize:3 
            // 水平:3, 垂直:3, 斜線:2
            this._checkerboardSize = checkerboardSize;
            this._data = new int[checkerboardSize * 2 + 2];
        }

        public void SetLine(Location location, int diff)
        {
            // 水平
            _data[location.X] += diff;

            // 垂直
            _data[location.Y + _checkerboardSize] += diff;

            // 斜線:/
            if (location.X == location.Y)
                _data[^2] += diff;

            // 斜線:\
            if (location.X + location.Y == this._checkerboardSize - 1)
                _data[^1] += diff;
        }

        public bool IsLine()
        {
            return _data.Any(l => l == this._checkerboardSize);
        }
    }
}