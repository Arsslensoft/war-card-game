using System.Collections.Generic;
using System.Linq;
using CardGames.Core;
using CardGames.Core.Enums;
using CardGames.War.Contracts;
using CardGames.War.StandardFiftyTwo;
using Serilog;

namespace CardGames.War
{
    public class WarCardGame : Game<FiftyTwoCardGamePlayer, FiftyTwoCardGameDeck, FiftyTwoCardGameCard>, ILogged
    {
        public WarCardGame(IEnumerable<FiftyTwoCardGamePlayer> players, FiftyTwoCardGameDeck initialDeck)
        {
            Players = players;
            InitialDeck = initialDeck;
        }
        public override FiftyTwoCardGamePlayer Winner
        {
            get
            {
                var candidates = Players.Where(player => player.Status == PlayerStatus.Competing).ToList();
                return candidates.Count == 1 ? candidates.FirstOrDefault() : null;
            }
        }
        public override void Play()
        {
            var roundNumber = 0;
            var candidates = Players.Where(player => player.Status == PlayerStatus.Competing).ToList();
            // while there are some players keep the game going
            while (candidates.Count > 1)
            {
                // create a new round and play
                var round = CreateRound<WarCardRound>(candidates, ++roundNumber);
                round.Logger = this.Logger;
                round.Play();
                // update candidates list
                candidates = Players.Where(player => player.Status == PlayerStatus.Competing).ToList();
            }
            Log();
        }

        public ILogger Logger { get; set; }
        public void Log()
        {
            Logger?.Information($"Game Winner: {Winner}");
        }
    }
}