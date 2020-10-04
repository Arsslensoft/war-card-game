using System.Collections.Generic;
using System.Linq;
using CardGames.Core;
using CardGames.Core.Enums;
using CardGames.War.Contracts;
using CardGames.War.StandardFiftyTwo;
using Serilog;

namespace CardGames.War
{
    /// <summary>
    ///     Represents the war card game class.
    /// </summary>
    public class WarCardGame : Game<FiftyTwoCardGamePlayer, FiftyTwoCardGameDeck, FiftyTwoCardGameCard>, ILogged
    {
        public WarCardGame(IEnumerable<FiftyTwoCardGamePlayer> players, FiftyTwoCardGameDeck initialDeck)
        {
            Players = players;
            InitialDeck = initialDeck;
        }

        /// <inheritdoc cref="Game{TPlayer,TDeck,TCard}" />
        public override FiftyTwoCardGamePlayer Winner
        {
            get
            {
                var candidates = Players.Where(player => player.Status <= PlayerStatus.Won).ToList();
                return candidates.Count == 1 ? candidates.FirstOrDefault() : null;
            }
        }

        /// <inheritdoc cref="ILogged" />
        public ILogger Logger { get; set; }

        /// <inheritdoc cref="ILogged" />
        public void Log()
        {
            Logger?.Information($"Game Winner: {Winner}");
        }

        /// <inheritdoc cref="Game{TPlayer,TDeck,TCard}" />
        public override void Play()
        {
            var roundNumber = 0;
            var candidates = Players.Where(player => player.Status == PlayerStatus.Competing).ToList();
            // while there are some players keep the game going
            while (candidates.Count > 1)
            {
                // create a new round and play
                var round = CreateRound<WarCardRound>(candidates, ++roundNumber);
                round.Logger = Logger;
                round.Play();
                // update candidates list
                candidates = Players.Where(player => player.Status == PlayerStatus.Competing).ToList();
            }
            // mark the winner as won
            Winner.Status = PlayerStatus.Won;
            Log();
        }
    }
}