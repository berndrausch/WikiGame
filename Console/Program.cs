using System;
using System.Collections.Generic;
using System.Linq;
using WikiGame.Logic;

namespace WikiGame.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            string randomPage = @"https://de.wikipedia.org/wiki/Spezial:Zuf%C3%A4llige_Seite";
            string goal = "Angela_Merkel";
            string[] preferred = new[] {"CDU", "Bundeskanzler_(Deutschland)", "Deutschland", "NATO", "Große_Koalition"};

            Game game = new Game(randomPage, goal, preferred);
            List<string> way = game.FindWay(10).ToList();

            way.ForEach(System.Console.WriteLine);
        }
    }
}
