using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Channels;
using CardGames.Core.Enums;
using CardGames.War;
using CardGames.War.StandardFiftyTwo;
using Serilog;

namespace WarCardGameConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var log = new LoggerConfiguration()
                .WriteTo.Console()
                .CreateLogger();
            var game = GameManager.Play(new List<string>()
            {
                "Jane",
                "John"
            }, log);
            Console.Read();
        }

    }
}
