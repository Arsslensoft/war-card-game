using System.Collections.Generic;

namespace CardGames.Core.Contracts
{
    /// <summary>
    /// Represents the game interface.
    /// </summary>
    /// <typeparam name="TPlayer">The <see cref="IPlayer{TDeck,TCard}"/> subtype.</typeparam>
    /// <typeparam name="TDeck">The <see cref="IDeck{TCard}"/> subtype.</typeparam>
    /// <typeparam name="TCard">The <see cref="ICard"/> subtype.</typeparam>
    public interface IGame<TPlayer, TDeck, TCard>
        where TCard : class, ICard
        where TDeck : class, IDeck<TCard>
        where TPlayer : class, IPlayer<TDeck, TCard>
    {
        /// <summary>
        /// Represents the winning player.
        /// </summary>
        TPlayer Winner { get; }
        /// <summary>
        /// Represents the players that are enrolled in the game.
        /// </summary>
        IEnumerable<TPlayer> Players { get; }
        /// <summary>
        /// Represents the list of rounds that were created during the game.
        /// </summary>
        IEnumerable<IRound<TPlayer, TDeck, TCard>> Rounds { get; }
        /// <summary>
        /// Represents the initial deck of cards.
        /// </summary>
        TDeck InitialDeck { get; set; }
        /// <summary>
        /// Executes the game play logic.
        /// </summary>
        void Play();
        /// <summary>
        /// Creates a game round.
        /// </summary>
        /// <typeparam name="TRound">The custom round type.</typeparam>
        /// <param name="players">The players that are eligible to play in the next round.</param>
        /// <param name="roundNumber">The index of the round.</param>
        /// <returns>The created game round.</returns>
        TRound CreateRound<TRound>(IEnumerable<TPlayer> players, int roundNumber)
            where TRound : class, IRound<TPlayer, TDeck, TCard>, new();

        /// <summary>
        /// Distributes the initial deck of cards to the competing players.
        /// </summary>
        void DistributeCards();
    }
}