// See https://aka.ms/new-console-template for more information

using Wsa.Gaas.GobbletGobblers.Domain;
using Wsa.Gaas.GobbletGobblers.Domain.Enums;

Console.OutputEncoding = System.Text.Encoding.UTF8;

var playerA = new Player()
    .Nameself("Josh");

var playerB = new Player()
    .Nameself("Tom");

var checkerboard = (GameConsole)new GameConsole(3)
    .JoinPlayer(playerA)
    .JoinPlayer(playerB);

checkerboard.Start();


public class GameConsole : Game
{
    public GameConsole(int checkerboardSize) : base(checkerboardSize)
    {
    }

    public void Start()
    {
        base.Start();

        ShowCheckBoard(this);

        Process();

        var winner = GetWinner();
        Console.WriteLine($"Winner:{winner.Name}");
    }

    private void Process()
    {
        while (true)
        {
            var currentPlayerIndex = base.CurrentPlayerId;
            var player = base.GetPlayer(currentPlayerIndex);

            Console.WriteLine($"{player.Name} [1]:Place, [2]:Move");
            var control = Console.ReadLine();

            if (control == "1")
            {
                ShowPlayerCocks(player);
                var handCockIndex = int.Parse(Console.ReadLine());

                Console.WriteLine($"{player.Name} Pacle Location X");
                var x = int.Parse(Console.ReadLine());

                Console.WriteLine($"{player.Name} Pacle Location Y");
                var y = int.Parse(Console.ReadLine());

                var putEvent = PutCock(new PutCockCommand(player.Id, handCockIndex, new Location(x, y)));
            }
            else if (control == "2")
            {
                //Console.WriteLine($"{player.Name} Move 0~9 From Location");
                //fromIndex = int.Parse(Console.ReadLine());

                //Console.WriteLine($"{player.Name} Move 0~9 To Location");
                //toIndex = int.Parse(Console.ReadLine());

                //isNext = Move(fromIndex, toIndex);

                //if (Gameover(fromIndex))
                //{
                //    return;
                //}
            }


            ShowCheckBoard(this);

            if (Gameover())
            {
                return;
            }
        }
    }


    public void ShowCheckBoard(Game game)
    {
        var checkerboardSize = game.CheckerboardSize;
        var board = game.Board;

        var bound = string.Join("\u3000", Enumerable.Range(0, checkerboardSize).Select(x => "—"));
        Console.WriteLine($"\u3000{bound}\u3000");

        for (var i = 0; i < board.Length; i++)
        {
            var cocks = board[i];

            if ((i + 1) % checkerboardSize == 1)
            {
                Console.Write("｜");
            }

            if (cocks.TryPeek(out var cock))
                ShowCocks(cock);
            else
                Console.Write("\u3000");

            Console.Write("｜");

            if ((i + 1) % checkerboardSize == 0)
            {
                Console.WriteLine();
                Console.WriteLine($"\u3000{bound}\u3000");
            }
        }
    }


    private void ShowPlayerCocks(Player player)
    {
        var cocks = player.GetCocks();
        for (int i = 0; i < cocks.Count; i++)
        {
            Console.Write($"[{i}]:");
            ShowCocks(cocks.ElementAt(i));
            Console.Write($" ");
        }

        Console.WriteLine();
    }

    private void ShowCocks(Cock cock)
    {
        var consoleColor = ConsoleColor.Black;

        var color = cock.Color;
        var symbol = cock.Size.Symbol;

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
        Console.Write(symbol);
        Console.ResetColor();
    }
}
