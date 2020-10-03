using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CardGames.Core;
using CardGames.Core.Enums;
using CardGames.War.Contracts;
using CardGames.War.StandardFiftyTwo;
using Serilog;

namespace CardGames.War
{
    public class WarCardRoundIteration : RoundIteration<FiftyTwoCardGamePlayer, FiftyTwoCardGameDeck, FiftyTwoCardGameCard>, ILogged
    {
        public bool HasConflict
        {
            get
            {
                var conflictingPlayersByTopCard = CurrentCardTray.PlayedCards
                    .Select(x => new { x.Player, Card = x.Cards.FirstOrDefault() })
                    .GroupBy(x => x.Card.Face)
                    .OrderByDescending(x => x.Key)
                    .FirstOrDefault();
                return conflictingPlayersByTopCard != null && conflictingPlayersByTopCard.Count() > 1;
            }
        }
        public IEnumerable<FiftyTwoCardGamePlayer> PlayersInConflict
        {
            get
            {
                var conflictingPlayersByTopCard = CurrentCardTray.PlayedCards
                    .Select(x => new { x.Player, Card = x.Cards.FirstOrDefault() })
                    .GroupBy(x => x.Card.Face)
                    .OrderByDescending(x => x.Key)
                    .FirstOrDefault();
                if (conflictingPlayersByTopCard == null || conflictingPlayersByTopCard.Count() <= 1) yield break;

                foreach (var playerCards in conflictingPlayersByTopCard)
                    yield return playerCards.Player;
            }
        }
        public override FiftyTwoCardGamePlayer Winner => CurrentCardTray
            .PlayedCards
            .Aggregate((l, r) => l?.Cards?.FirstOrDefault()?.CompareTo(r.Cards.FirstOrDefault()) == 1 ? l : r).Player;
        public override void Play()
        {
            foreach (var player in Players)
                if (!MoveController.Execute(player, CurrentCardTray))
                    player.Status = PlayerStatus.Lost;

            Log();
        }


        public ILogger Logger { get; set; }
        public void Log()
        {
            Logger.Information($"--------------------Round Iteration---------------------");
            Logger.Information($"Mode: {MoveController}");
            StringBuilder playerNamesBuilder = new StringBuilder();
            //Print players
            foreach (var fiftyTwoCardGamePlayer in Players)
                playerNamesBuilder.Append($"{fiftyTwoCardGamePlayer}, ");

            Logger.Information($"Players: {playerNamesBuilder}");
            // Print moves
            foreach (var playerCards in CurrentCardTray.PlayedCards)
            {
                var cards = playerCards.Cards.ToList();
                for (var j = cards.Count - 1; j >= 0; j--)
                    Logger.Information($"Player {playerCards.Player} has placed {cards[j]}");

            }
        }
    }
}