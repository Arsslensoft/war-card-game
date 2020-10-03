using System;
using System.Collections.Generic;
using CardGames.War;

namespace WarCardGameConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var game = GameManager.Play(new List<string>()
            {
                "Jane",
                "John"
            });
            Console.WriteLine("Winner: " + game.Winner.Name);
            Console.Read();
        }
    }
}
