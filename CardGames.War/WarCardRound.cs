using System.Linq;
using CardGames.Core;
using CardGames.Core.Enums;
using CardGames.War.Contracts;
using CardGames.War.Moves;
using CardGames.War.StandardFiftyTwo;
using Serilog;

namespace CardGames.War
{
    /// <summary>
    ///     Represents the round of the war card game class.
    /// </summary>
    public class WarCardRound : Round<FiftyTwoCardGamePlayer, FiftyTwoCardGameDeck, FiftyTwoCardGameCard>, ILogged
    {
        /// <inheritdoc cref="ILogged" />
        public ILogger Logger { get; set; }

        /// <inheritdoc cref="ILogged" />
        public void Log()
        {
            Logger?.Information($"--------------------{this}---------------------");
        }

        /// <inheritdoc cref="Round{TPlayer,TDeck,TCard}" />
        public override void Play()
        {
            Log();
            // create the first iteration with a simple movement controller
            var currentIteration = CreateIteration<WarCardRoundIteration, SimplePlayerMoveController, WarCardTray>();
            currentIteration.Logger = Logger;
            currentIteration.Play();
            // detect conflict and enter war mode if possible
            while (currentIteration.HasConflict)
            {
                var playersInConflict = currentIteration.PlayersInConflict;
                Logger?.Information("---------------Conflict--------------");
                Logger?.Information("Conflict has been found between players");
                foreach (var player in playersInConflict)
                    Logger?.Information($"Player: {player}");
                Logger?.Information("-------------------------------------");

                currentIteration =
                    CreateIteration<WarCardRoundIteration, WarPlayerMovementController, WarCardTray>(playersInConflict);
                currentIteration.Logger = Logger;
                currentIteration.Play();
            }

            Logger?.Information($"{Winner} Won the round and will claim {AllPlayedCards.Count()} cards");
            Winner?.Deck.Put(AllPlayedCards);
            // Eliminate players with an empty deck
            foreach (var player in Players)
                if (!player.Deck.CanPick)
                    player.Status = PlayerStatus.Lost;
        }

        /// <inheritdoc cref="object" />
        public override string ToString()
        {
            return $"Round {Number}";
        }
    }
}