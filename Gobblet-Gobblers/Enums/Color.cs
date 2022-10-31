namespace Gobblet_Gobblers.Enums
{
    internal enum Color
    {
        /// <summary>
        /// 橘色
        /// </summary>
        Orange = 1,

        /// <summary>
        /// 藍色
        /// </summary>
        Blue = 2,
    }

    internal static class ColorExtensions
    {
        internal static void ToPrint(this Color color, string content)
        {
            var consoleColor = ConsoleColor.Black;
            switch (color)
            {
                case Color.Orange:
                    consoleColor = ConsoleColor.Red;
                    break;
                case Color.Blue:
                    consoleColor = ConsoleColor.Blue;
                    break;
            }
            Console.ForegroundColor = consoleColor;
            Console.Write(content);
            Console.ResetColor();
        }
    }
}
