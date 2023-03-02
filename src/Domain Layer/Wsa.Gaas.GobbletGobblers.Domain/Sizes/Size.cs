namespace Wsa.Gaas.GobbletGobblers.Domain.Sizes
{
    public abstract class Size : IComparable<Size>
    {
        public virtual int Number { get; }

        public virtual string Symbol { get; }

        public int CompareTo(Size? other)
        {
            if (other == null)
            {
                return 1;
            }

            return this.Number - other.Number;
        }
    }
}
