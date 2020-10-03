using System.Collections.Generic;
using CardGames.Core.Contracts;

namespace CardGames.Core
{
    public abstract class Game<TPlayer, TDeck, TCard> : IGame<TPlayer, TDeck, TCard>
        where TCard : class, ICard
        where TDeck : class, IDeck<TCard>
        where TPlayer : class, IPlayer<TDeck, TCard>
    {
        public TPlayer Winner { get; }
        public TDeck InitialDeck { get; set; }
        public virtual IEnumerable<TPlayer> Players { get; }
        public virtual IEnumerable<IRound<TPlayer, TDeck, TCard>> Rounds { get; set; }

        public abstract void Play();
    }
}