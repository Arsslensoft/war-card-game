using System.Collections.Generic;
using System.Linq;
using CardGames.Core;
using CardGames.Core.Enums;
using CardGames.War.StandardFiftyTwo;

namespace CardGames.War
{
    public class WarCardRoundIteration : RoundIteration<FiftyTwoCardGamePlayer, FiftyTwoCardGameDeck, FiftyTwoCardGameCard>
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
        }
    }
}