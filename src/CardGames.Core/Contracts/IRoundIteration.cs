using System.Collections.Generic;

namespace CardGames.Core.Contracts
{
    /// <summary>
    ///     Represents the interface of the round iteration.
    ///     This interface will define the detailed round logic which is specific to the game.
    /// </summary>
    /// <typeparam name="TPlayer">The <see cref="IPlayer{TDeck,TCard}" /> subtype.</typeparam>
    /// <typeparam name="TDeck">The <see cref="IDeck{TCard}" /> subtype.</typeparam>
    /// <typeparam name="TCard">The <see cref="ICard" /> subtype.</typeparam>
    public interface IRoundIteration<TPlayer, TDeck, TCard>
        where TCard : class, ICard
        where TDeck : class, IDeck<TCard>
        where TPlayer : class, IPlayer<TDeck, TCard>
    {
        /// <summary>
        ///     Represents the players that are part of this iteration.
        /// </summary>
        IEnumerable<TPlayer> Players { get; }

        /// <summary>
        ///     Represents the current round this iteration is attached to.
        /// </summary>
        IRound<TPlayer, TDeck, TCard> CurrentRound { get; set; }

        /// <summary>
        ///     Represents the current card tray.
        /// </summary>
        ICardTray<TPlayer, TDeck, TCard> CurrentCardTray { get; }

        /// <summary>
        ///     Represents the movement controller that is specific to this round iteration.
        /// </summary>
        IMoveController<TPlayer, TDeck, TCard> MoveController { get; }

        /// <summary>
        ///     Represents the winning player.
        /// </summary>
        TPlayer Winner { get; }

        /// <summary>
        ///     Represents the round iteration play logic.
        /// </summary>
        void Play();

        /// <summary>
        ///     Represents the initialization code to be executed just after the creation and before round iteration execution.
        /// </summary>
        /// <typeparam name="TMoveController">The movement controller type.</typeparam>
        /// <typeparam name="TCardTray">The card tray type.</typeparam>
        /// <param name="players">The players of the current round iteration.</param>
        void Initialize<TMoveController, TCardTray>(IEnumerable<TPlayer> players)
            where TMoveController : class, IMoveController<TPlayer, TDeck, TCard>, new()
            where TCardTray : class, ICardTray<TPlayer, TDeck, TCard>, new();
    }
}