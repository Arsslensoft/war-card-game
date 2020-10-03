using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CardGames.Core.Contracts;
using CardGames.Core.Enums;
using CardGames.War.StandardFiftyTwo;
using CardGames.War.StandardFiftyTwo.Enums;
using Serilog;

namespace CardGames.War
{
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

        private static IEnumerable<FiftyTwoCardGamePlayer> CreatePlayers(IEnumerable<string> playerNames)
        {
            return playerNames.Select(playerName => new FiftyTwoCardGamePlayer
            {
                Name = playerName,
                Deck = new FiftyTwoCardGameDeck(),
                Status = PlayerStatus.Competing
            });
        }

        private static FiftyTwoCardGameDeck GetInitialDeck(IEnumerable<FiftyTwoCardGamePlayer> players)
        {
            var deck = new FiftyTwoCardGameDeck();
            foreach (var player in players)
                deck.Put(player.Deck.Cards);
            return deck;
        }

        public static WarCardGame Play(IEnumerable<string> playerNames, ILogger logger)
        {
            var players = CreatePlayers(playerNames).ToList();
            var deck = CreateDummyWar();
            var game = new WarCardGame(players, deck) { Logger = logger };
            game.DistributeCards();
            game.Play();
            return game;
        }

    }
}

