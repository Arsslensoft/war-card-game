using System;
using System.Collections.Generic;
using System.Linq;
using CardGames.Core.Enums;
using CardGames.War.StandardFiftyTwo;
using CardGames.War.StandardFiftyTwo.Enums;
using Serilog;

namespace CardGames.War
{
    /// <summary>
    ///     Represents the Façade of the war game class.
    /// </summary>
    public static class GameManager
    {
        private static FiftyTwoCardGameDeck CreateDummyWar()
        {
            var deck = new FiftyTwoCardGameDeck();
            deck.Put(new List<FiftyTwoCardGameCard>
            {
                new FiftyTwoCardGameCard(Suite.Clubs, Face.Ace),
                new FiftyTwoCardGameCard(Suite.Clubs, Face.Three),
                new FiftyTwoCardGameCard(Suite.Clubs, Face.Eight),

                new FiftyTwoCardGameCard(Suite.Spades, Face.Ace),
                new FiftyTwoCardGameCard(Suite.Spades, Face.Three),
                new FiftyTwoCardGameCard(Suite.Spades, Face.Seven)
            });

            return deck;
        }

        private static FiftyTwoCardGameDeck CreateDummy()
        {
            var deck = new FiftyTwoCardGameDeck();
            deck.Put(new List<FiftyTwoCardGameCard>
            {
                new FiftyTwoCardGameCard(Suite.Clubs, Face.Ace),
                new FiftyTwoCardGameCard(Suite.Spades, Face.Ace)
            });

            return deck;
        }

        private static FiftyTwoCardGameDeck CreateInitialShuffledDeck()
        {
            var deck = new FiftyTwoCardGameDeck();
            foreach (var suite in Enum.GetValues(typeof(Suite)))
                foreach (var face in Enum.GetValues(typeof(Face)))
                    deck.Put(new FiftyTwoCardGameCard((Suite)suite, (Face)face));
            deck.Shuffle();

            return deck;
        }

        private static IEnumerable<FiftyTwoCardGamePlayer> CreatePlayersAndAssignDecks(
            IDictionary<string, IEnumerable<FiftyTwoCardGameCard>> playerCards)
        {
            foreach (var playerCard in playerCards)
            {
                var player = new FiftyTwoCardGamePlayer
                {
                    Name = playerCard.Key,
                    Deck = new FiftyTwoCardGameDeck(),
                    Status = PlayerStatus.Competing
                };
                foreach (var card in playerCard.Value)
                    player.Deck.Put(card);

                yield return player;
            }
        }

        private static IEnumerable<FiftyTwoCardGamePlayer> CreatePlayers(int numberOfPlayers)
        {
            for (var i = 0; i < numberOfPlayers; i++)
                yield return new FiftyTwoCardGamePlayer
                {
                    Name = "Player " + i,
                    Deck = new FiftyTwoCardGameDeck(),
                    Status = PlayerStatus.Competing
                };
        }

        private static IEnumerable<FiftyTwoCardGamePlayer> CreatePlayers(IEnumerable<string> playerNames)
        {
            return playerNames.Select(playerName => new FiftyTwoCardGamePlayer
            {
                Name = playerName,
                Deck = new FiftyTwoCardGameDeck(),
                Status = PlayerStatus.Competing
            });
        }

        private static IEnumerable<FiftyTwoCardGamePlayer> CreatePlayers(List<Tuple<string, string[]>> playerCards)
        {
            return playerCards.Select(playerCard => new FiftyTwoCardGamePlayer
            {
                Name = playerCard.Item1,
                Deck = GetPlayerDeck(playerCard.Item2),
                Status = PlayerStatus.Competing
            });
        }

        private static FiftyTwoCardGameDeck GetPlayerDeck(string[] cards)
        {
            var deck = new FiftyTwoCardGameDeck();
            foreach (var cardText in cards)
            {
                var cardInfos = cardText.Split(',');
                deck.Put(new FiftyTwoCardGameCard(Enum.Parse<Suite>(cardInfos[1], true),
                    Enum.Parse<Face>(cardInfos[0], true)));
            }

            return deck;
        }

        private static FiftyTwoCardGameDeck GetInitialDeck(IEnumerable<FiftyTwoCardGamePlayer> players)
        {
            var deck = new FiftyTwoCardGameDeck();
            foreach (var player in players)
                deck.Put(player.Deck.Cards);
            return deck;
        }

        private static WarCardGame Play(IEnumerable<FiftyTwoCardGamePlayer> players, FiftyTwoCardGameDeck deck,
            ILogger logger, bool distribute)
        {
            var game = new WarCardGame(players, deck) { Logger = logger };
            if (distribute)
                game.DistributeCards();
            game.Play();
            return game;
        }

        /// <summary>
        ///     Creates a game using a list of player names.
        /// </summary>
        /// <param name="playerNames">The player names list.</param>
        /// <param name="logger">The logger.</param>
        /// <returns>The created and simulated game.</returns>
        public static WarCardGame Play(IEnumerable<string> playerNames, ILogger logger)
        {
            var players = CreatePlayers(playerNames).ToList();
            var deck = CreateInitialShuffledDeck();
            return Play(players, deck, logger, true);
        }

        /// <summary>
        ///     Creates a set of games.
        /// </summary>
        /// <param name="numberOfPlayers">The number of players.</param>
        /// <param name="numberOfGames">The number of games to create.</param>
        /// <param name="logger">The logger.</param>
        /// <returns>The simulated games.</returns>
        public static IEnumerable<WarCardGame> Play(int numberOfPlayers, int numberOfGames, ILogger logger)
        {
            for (var i = 0; i < numberOfGames; i++)
            {
                var players = CreatePlayers(numberOfPlayers).ToList();
                yield return Play(players, CreateInitialShuffledDeck(), logger, true);
            }
        }

        public static WarCardGame Play(List<Tuple<string, string[]>> playerCards, ILogger logger)
        {
            var players = CreatePlayers(playerCards).ToList();
            var deck = GetInitialDeck(players);
            return Play(players, deck, logger, false);
        }
    }
}