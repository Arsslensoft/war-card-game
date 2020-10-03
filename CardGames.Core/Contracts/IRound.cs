using System.Collections.Generic;

namespace CardGames.Core.Contracts
{
    public interface IRound<TPlayer, TDeck, TCard>
        where TCard : class, ICard
        where TDeck : class, IDeck<TCard>
        where TPlayer : class, IPlayer<TDeck, TCard>
    {
        int Number { get; }
        IEnumerable<IRoundIteration<TPlayer, TDeck, TCard>> Iterations { get; }
        void Play();
    }
}