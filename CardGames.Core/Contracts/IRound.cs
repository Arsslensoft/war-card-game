using System.Collections.Generic;
using System.Linq;

namespace CardGames.Core.Contracts
{
    /// <summary>
    /// Represents the round interface.
    /// This interface will only represent the base round logic, that is specific to the game.
    /// </summary>
    /// <typeparam name="TPlayer">The <see cref="IPlayer{TDeck,TCard}"/> subtype.</typeparam>
    /// <typeparam name="TDeck">The <see cref="IDeck{TCard}"/> subtype.</typeparam>
    /// <typeparam name="TCard">The <see cref="ICard"/> subtype.</typeparam>
    public interface IRound<TPlayer, TDeck, TCard>
        where TCard : class, ICard
        where TDeck : class, IDeck<TCard>
        where TPlayer : class, IPlayer<TDeck, TCard>
    {
        /// <summary>
        /// Represents the round number.
        /// </summary>
        int Number { get; set; }
        /// <summary>
        /// Represents all the cards that were played during this round.
        /// </summary>
        IEnumerable<TCard> AllPlayedCards { get; }
        /// <summary>
        /// Represents the winning player.
        /// </summary>
        TPlayer Winner { get; }
        /// <summary>
        /// Represents the list of players that participated in this round.
        /// </summary>
        IEnumerable<TPlayer> Players { get; set; }
        /// <summary>
        /// Represents the number of iterations that were created under this round.
        /// </summary>
        IEnumerable<IRoundIteration<TPlayer, TDeck, TCard>> Iterations { get; }
        /// <summary>
        /// Represents the play logic.
        /// </summary>
        void Play();
        /// <summary>
        /// Represents the create round iteration logic.
        /// </summary>
        /// <typeparam name="TRoundIteration">The round iteration type.</typeparam>
        /// <typeparam name="TMoveController">The movement controller type.</typeparam>
        /// <typeparam name="TCardTray">The card tray type.</typeparam>
        /// <param name="players">The players to participate in the iteration.</param>
        /// <returns>The created round iteration.</returns>
        TRoundIteration CreateIteration<TRoundIteration, TMoveController, TCardTray>(IEnumerable<TPlayer> players = null)
            where TRoundIteration : class, IRoundIteration<TPlayer, TDeck, TCard>, new()
            where TMoveController : class, IMoveController<TPlayer, TDeck, TCard>, new()
            where TCardTray : class, ICardTray<TPlayer, TDeck, TCard>, new();
    }
}