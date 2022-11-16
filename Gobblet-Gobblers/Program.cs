// See https://aka.ms/new-console-template for more information

using Gobblet_Gobblers;
using Gobblet_Gobblers.Enums;

Console.OutputEncoding = System.Text.Encoding.UTF8;

var playerA = new Player()
    .Nameself("Josh")
    .AddCocks(Cock.StandardEditionCocks(Color.Orange));

var playerB = new Player()
    .Nameself("Tom")
    .AddCocks(Cock.StandardEditionCocks(Color.Blue));

var checkerboard = new Checkerboard(3)
    .JoinPlayer(playerA)
    .JoinPlayer(playerB);

checkerboard.Start();
