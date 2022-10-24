// See https://aka.ms/new-console-template for more information

using Gobblet_Gobblers;

Console.OutputEncoding = System.Text.Encoding.UTF8;

Console.WriteLine("Hello, World!");
Console.WriteLine("① ② ③");
Console.WriteLine("① ② ③");

var checkerboard = new Checkerboard();

checkerboard.Gameover(2);
