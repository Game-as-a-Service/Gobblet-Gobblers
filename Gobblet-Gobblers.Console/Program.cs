// See https://aka.ms/new-console-template for more information
using Gobblet_Gobblers.Shared;
using Gobblet_Gobblers.Shared.Enums;

Console.OutputEncoding = System.Text.Encoding.UTF8;

var playerA = new Player()
    .Nameself("Josh")
    .AddCocks(Cock.StandardEditionCocks(Color.Orange));

var playerB = new Player()
    .Nameself("Tom")
    .AddCocks(Cock.StandardEditionCocks(Color.Blue));

var checkerboard = (CheckerboardConsole)new CheckerboardConsole(3)
    .JoinPlayer(playerA)
    .JoinPlayer(playerB);

checkerboard.Start();


public class CheckerboardConsole : Checkerboard
{
    public CheckerboardConsole(int checkerboardSize) : base(checkerboardSize)
    {
    }

    public void Start()
    {
        Print();

        Process();

        ShowWinner();
    }

    private void Process()
    {
        while (true)
        {
            foreach (var player in _players)
            {
                var isNext = false;
                var fromIndex = 0;
                var toIndex = 0;

                while (!isNext)
                {
                    Console.WriteLine($"{player.Name} [1]:Place, [2]:Move");
                    var control = Console.ReadLine();

                    if (control == "1")
                    {
                        player.Print();
                        var cockIndex = int.Parse(Console.ReadLine());
                        var cock = player.GetCock(cockIndex);

                        Console.WriteLine($"{player.Name} Pacle 0~9 Location");
                        toIndex = int.Parse(Console.ReadLine());

                        isNext = Place(cock, toIndex);

                        if (isNext)
                        {
                            player.RemoveCock(cockIndex);
                        }
                    }
                    else if (control == "2")
                    {
                        // TODO: 需要調整
                        isNext = Move(fromIndex, toIndex);

                        if (Gameover(fromIndex))
                        {
                            return;
                        }
                    }
                }

                Print();

                if (Gameover(toIndex))
                {
                    return;
                }
            }
        }
    }
}