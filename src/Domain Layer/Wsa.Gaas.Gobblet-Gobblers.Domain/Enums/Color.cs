namespace Wsa.Gaas.Gobblet_Gobblers.Domain.Enums
{
    public enum Color
    {
        /// <summary>
        /// 橘色
        /// </summary>
        Orange = 0,

        /// <summary>
        /// 藍色
        /// </summary>
        Blue = 1,
    }

    internal static class ColorExtensions
    {
        // TODO : 拔除
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
