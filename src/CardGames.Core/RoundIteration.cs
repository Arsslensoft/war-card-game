using System.Collections.Generic;
using CardGames.Core.Contracts;

namespace CardGames.Core
{
    /// <summary>
    ///     Represents the round iteration concerning a set of players class.
    /// </summary>
    /// <typeparam name="TPlayer">The  type that represents the customized <see cref="Player{TDeck,TCard}" /> for a given game.</typeparam>
    /// <typeparam name="TDeck">The  type that represents the customized <see cref="Deck{TCard}" /> for a given game.</typeparam>
    /// <typeparam name="TCard">The type that represents the customized <see cref="Card" /> for a given game.</typeparam>
    public abstract class RoundIteration<TPlayer, TDeck, TCard> : IRoundIteration<TPlayer, TDeck, TCard>
        where TCard : class, ICard
        where TDeck : class, IDeck<TCard>
        where TPlayer : class, IPlayer<TDeck, TCard>
    {
        #region Properties
        /// <inheritdoc />
        public virtual IEnumerable<TPlayer> Players { get; set; }

        /// <inheritdoc />
        public IRound<TPlayer, TDeck, TCard> CurrentRound { get; set; }

        /// <inheritdoc />
        public ICardTray<TPlayer, TDeck, TCard> CurrentCardTray { get; set; }

        /// <inheritdoc />
        public IMoveController<TPlayer, TDeck, TCard> MoveController { get; set; }

        /// <inheritdoc />
        public abstract TPlayer Winner { get; }
        #endregion

        #region Methods
        /// <inheritdoc />
        public abstract void Play();

        /// <inheritdoc />
        public virtual void Initialize<TMoveController, TCardTray>(IEnumerable<TPlayer> players)
            where TMoveController : class, IMoveController<TPlayer, TDeck, TCard>, new()
            where TCardTray : class, ICardTray<TPlayer, TDeck, TCard>, new()
        {
            MoveController = new TMoveController();
            CurrentCardTray = new TCardTray();
            Players = players;
        }
        #endregion
    }
}