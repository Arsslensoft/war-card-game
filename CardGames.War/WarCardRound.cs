using System;
using CardGames.Core;
using CardGames.Core.Enums;
using CardGames.War.Contracts;
using CardGames.War.Moves;
using CardGames.War.StandardFiftyTwo;
using Serilog;

namespace CardGames.War
{
    public class WarCardRound : Round<FiftyTwoCardGamePlayer, FiftyTwoCardGameDeck, FiftyTwoCardGameCard>, ILogged
    {
        public override void Play()
        {
            Log();
            // create the first iteration with a simple movement controller
            var currentIteration = CreateIteration<WarCardRoundIteration, SimplePlayerMovementController, WarCardTray>();
            currentIteration.Logger = this.Logger;
            currentIteration.Play();
            // detect conflict and enter war mode if possible
            while (currentIteration.HasConflict)
            {

                var playersInConflict = currentIteration.PlayersInConflict;
                Logger.Information("---------------Conflict--------------");
                Logger.Information("Conflict has been found between players");
                foreach (var player in playersInConflict)
                    Logger.Information($"Player: {player}");
                Logger.Information("-------------------------------------");

                currentIteration = CreateIteration<WarCardRoundIteration, WarPlayerMovementController, WarCardTray>(playersInConflict);
                currentIteration.Logger = this.Logger;
                currentIteration.Play();
            }
            Winner?.Deck.Put(AllPlayedCards);
            // Eliminate players with an empty deck
            foreach (var player in Players)
                if (!player.Deck.CanPick)
                    player.Status = PlayerStatus.Lost;
        }

        public override string ToString() => $"Round {Number}";
        public ILogger Logger { get; set; }
        public void Log()
        {
            Logger.Information($"--------------------{this}---------------------");
        }
    }
}