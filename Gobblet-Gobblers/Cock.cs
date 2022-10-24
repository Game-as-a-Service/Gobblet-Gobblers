using Gobblet_Gobblers.Enums;

namespace Gobblet_Gobblers
{
    internal class Cock : IEquatable<Cock>, IComparable<Cock>
    {
        internal Color Color { get; private set; }

        internal Size Size { get; private set; }

        internal Cock(Color color, Size size)
        {
            Color = color;
            Size = size;
        }

        public int Compare(Cock? x, Cock? y)
        {
            if (x == null)
                return -1;

            if (y == null)
                return 1;

            return x.Size - y.Size;
        }

        public bool Equals(Cock? other)
        {
            if (other == null)
                return false;

            return this.Color == other.Color;
        }

        public int CompareTo(Cock? other)
        {
            if (other == null)
                return 1;

            return this.Size - other.Size;
        }
    }
}
