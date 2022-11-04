using Gobblet_Gobblers.Enums;
using Gobblet_Gobblers.Sizes;

namespace Gobblet_Gobblers
{
    internal class Cock : IEquatable<Cock>, IComparable<Cock>
    {
        internal virtual int Test => 0;

        internal Color Color { get; private set; }

        internal ISize Size { get; private set; }

        internal Cock(Color color, ISize size)
        {
            Color = color;
            Size = size;
        }

        public void Print()
        {
            this.Color.ToPrint(this.Size.Symbol);
        }

        public int Compare(Cock? x, Cock? y)
        {
            if (x == null)
                return -1;

            if (y == null)
                return 1;

            return x.Size.Number - y.Size.Number;
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

            return this.Size.Number - other.Size.Number;
        }
    }
}
