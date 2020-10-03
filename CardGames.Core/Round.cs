using System.Collections.Generic;
using System.Linq;
using CardGames.Core.Contracts;

namespace CardGames.Core
{
    public abstract class Round<TPlayer, TDeck, TCard> : IRound<TPlayer, TDeck, TCard>
        where TCard : class, ICard
        where TDeck : class, IDeck<TCard>
        where TPlayer : class, IPlayer<TDeck, TCard>
    {
        private readonly List<IRoundIteration<TPlayer, TDeck, TCard>> _iterations = new List<IRoundIteration<TPlayer, TDeck, TCard>>();
        /// <inheritdoc cref="IRound{TPlayer,TDeck,TCard}"/>
        public int Number { get; set; }
        /// <inheritdoc cref="IRound{TPlayer,TDeck,TCard}"/>
        public virtual TPlayer Winner => _iterations.Count > 0 ? _iterations?.LastOrDefault()?.Winner : null;
        /// <inheritdoc cref="IRound{TPlayer,TDeck,TCard}"/>
        public IEnumerable<TCard> AllPlayedCards =>
            from iteration in Iterations
            from cardTraySlot in iteration.CurrentCardTray.PlayedCards
            from card in cardTraySlot.Cards
            select card;
        /// <inheritdoc cref="IRound{TPlayer,TDeck,TCard}"/>
        public IEnumerable<TPlayer> Players { get; set; }
        /// <inheritdoc cref="IRound{TPlayer,TDeck,TCard}"/>
        public IEnumerable<IRoundIteration<TPlayer, TDeck, TCard>> Iterations => _iterations;
        /// <inheritdoc cref="IRound{TPlayer,TDeck,TCard}"/>
        public abstract void Play();
        /// <inheritdoc cref="IRound{TPlayer,TDeck,TCard}"/>
        public virtual TRoundIteration CreateIteration<TRoundIteration, TMoveController, TCardTray>(IEnumerable<TPlayer> players = null)
            where TRoundIteration : class, IRoundIteration<TPlayer, TDeck, TCard>, new()
            where TMoveController : class, IMoveController<TPlayer, TDeck, TCard>, new()
            where TCardTray : class, ICardTray<TPlayer, TDeck, TCard>, new()
        {
            var roundIteration = new TRoundIteration()
            {
                CurrentRound = this
            };
            _iterations.Add(roundIteration);
            roundIteration.Initialize<TMoveController, TCardTray>(players ?? Players);
            return roundIteration;
        }
    }
}