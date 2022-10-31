namespace Gobblet_Gobblers.Enums
{
    internal enum Size
    {
        Small = 0,

        Medium = 1,

        Large = 2,
    }

    internal static class SizeExtensions
    {
        internal static string ToSymbol(this Size size)
        {
            return size switch
            {
                Size.Small => "①",
                Size.Medium => "②",
                Size.Large => "③",
                _ => throw new ArgumentException("Cock Symbol Exception"),
            };
        }
    }
}
