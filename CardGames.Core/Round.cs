using System.Collections.Generic;
using CardGames.Core.Contracts;

namespace CardGames.Core
{
    public abstract class Round<TPlayer, TDeck, TCard> : IRound<TPlayer, TDeck, TCard>
        where TCard : class, ICard
        where TDeck : class, IDeck<TCard>
        where TPlayer : class, IPlayer<TDeck, TCard>
    {
        private readonly List<IRoundIteration<TPlayer, TDeck, TCard>> _iterations = new List<IRoundIteration<TPlayer, TDeck, TCard>>();
        protected Round(int number)
        {
            Number = number;
        }
        public int Number { get; }
        public virtual IEnumerable<IRoundIteration<TPlayer, TDeck, TCard>> Iterations => _iterations;
        public abstract void Play();
    }
}