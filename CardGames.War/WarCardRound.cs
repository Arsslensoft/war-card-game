using CardGames.Core;
using CardGames.Core.Enums;
using CardGames.War.Moves;
using CardGames.War.StandardFiftyTwo;

namespace CardGames.War
{
    public class WarCardRound : Round<FiftyTwoCardGamePlayer, FiftyTwoCardGameDeck, FiftyTwoCardGameCard>
    {
        public override void Play()
        {
            // create the first iteration with a simple movement controller
            var currentIteration = CreateIteration<WarCardRoundIteration, SimplePlayerMovementController, WarCardTray>();
            currentIteration.Play();
            // detect conflict and enter war mode if possible
            while (currentIteration.HasConflict)
            {
                var playersInConflict = currentIteration.PlayersInConflict;
                currentIteration = CreateIteration<WarCardRoundIteration, WarPlayerMovementController, WarCardTray>(playersInConflict);
                currentIteration.Play();
            }
            Winner?.Deck.Put(AllPlayedCards);
            // Eliminate players with an empty deck
            foreach (var player in Players)
                if (!player.Deck.CanPick)
                    player.Status = PlayerStatus.Lost;
        }
    }
}