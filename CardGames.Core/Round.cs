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
        public int Number { get; set; }
        public IEnumerable<IRoundIteration<TPlayer, TDeck, TCard>> Iterations => _iterations;
        public abstract void Play();

        public virtual TRoundIteration CreateIteration<TRoundIteration, TMoveController, TCardTray>()
            where TRoundIteration : class, IRoundIteration<TPlayer, TDeck, TCard>, new()
            where TMoveController : class, IMoveController<TPlayer, TDeck, TCard>, new()
            where TCardTray : class, ICardTray<TPlayer, TDeck, TCard>, new()
        {
            var roundIteration = new TRoundIteration();
            _iterations.Add(roundIteration);
            roundIteration.Initialize<TMoveController, TCardTray>();
            return roundIteration;
        }
    }
}