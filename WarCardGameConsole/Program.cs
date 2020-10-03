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
        static void NumberOfPlayersAndGames()
        {
            Console.Write("Enter the number of players: ");
            int numberOfPlayers = int.Parse(Console.ReadLine());
            Console.Write("Enter the number of games: ");
            int numberOfGames = int.Parse(Console.ReadLine());
            var log = new LoggerConfiguration()
                .WriteTo.Console()
                .CreateLogger();
            GameManager.Play(numberOfPlayers, numberOfGames, log).ToList();
        }

        static Tuple<string, string[]> PlayerCardsInput()
        {
            Console.Write("Enter the player name: ");
            var playerName = Console.ReadLine();
            Console.Write("Enter the player cards (e.g: Ace,Spades|Ten,Hearts): ");
            var cardsText = Console.ReadLine();
            var cards = cardsText.Split('|');
            return new Tuple<string, string[]>(playerName, cards);

        }
        static void PlayersWithCards()
        {
            Console.Write("Enter the number of players: ");
            var numberOfPlayers = int.Parse(Console.ReadLine());

            var players = new List<Tuple<string, string[]>>();
            for (var i = 0; i < numberOfPlayers; i++)
                players.Add(PlayerCardsInput());


            var log = new LoggerConfiguration()
                .WriteTo.Console()
                .CreateLogger();
            GameManager.Play(players, log);
        }
        static void Main(string[] args)
        {
            //Console.WriteLine("1) Number of games and players");
            //Console.WriteLine("Else) Players and their assigned cards");
            //Console.Write("Enter the choice: ");
            //int option = int.Parse(Console.ReadLine());
            //if (option == 1) NumberOfPlayersAndGames();
            //else PlayersWithCards();
            var log = new LoggerConfiguration()
                .WriteTo.Console()
                .CreateLogger();
            GameManager.Play(new List<Tuple<string, string[]>>()
            {
                new Tuple<string, string[]>("A", "ace,spades|ten,hearts".Split('|')),
                new Tuple<string, string[]>("B", "king,clubs|ten,spades".Split('|')),
                new Tuple<string, string[]>("C", "king,hearts|ten,clubs".Split('|'))
            }, log);
            Console.Read();
        }

    }
}
