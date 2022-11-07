// See https://aka.ms/new-console-template for more information

using Gobblet_Gobblers;
using Gobblet_Gobblers.Enums;
using Gobblet_Gobblers.Sizes;

Console.OutputEncoding = System.Text.Encoding.UTF8;

var players = new Player[2];
var cocksA = new List<Cock>
{
    new Cock(Color.Orange, new Small()),
    new Cock(Color.Orange, new Small()),
    new Cock(Color.Orange, new Medium()),
    new Cock(Color.Orange, new Medium()),
    new Cock(Color.Orange, new Large()),
    new Cock(Color.Orange, new Large()),
};
players[0] = new Player(Color.Orange)
    .Nameself("Josh")
    .AddCocks(cocksA);

var cocksB = new List<Cock>
{
    new Cock(Color.Blue, new Small()),
    new Cock(Color.Blue, new Small()),
    new Cock(Color.Blue, new Medium()),
    new Cock(Color.Blue, new Medium()),
    new Cock(Color.Blue, new Large()),
    new Cock(Color.Blue, new Large()),
};
players[1] = new Player(Color.Blue)
    .Nameself("Tom")
    .AddCocks(cocksB);

var checkerboard = new Checkerboard(3, players);
checkerboard.Start();
