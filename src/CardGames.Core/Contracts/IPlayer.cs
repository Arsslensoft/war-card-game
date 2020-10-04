using CardGames.Core.Enums;

namespace CardGames.Core.Contracts
{
    /// <summary>
    ///     Represents the player interface.
    /// </summary>
    /// <typeparam name="TDeck">The <see cref="IDeck{TCard}" /> subtype.</typeparam>
    /// <typeparam name="TCard">The <see cref="ICard" /> subtype.</typeparam>
    public interface IPlayer<TDeck, TCard>
        where TDeck : class, IDeck<TCard>
        where TCard : class, ICard
    {
        /// <summary>
        ///     Represents the player name.
        /// </summary>
        string Name { get; set; }

        /// <summary>
        ///     Represents the status of the player, see <see cref="PlayerStatus" />.
        /// </summary>
        PlayerStatus Status { get; set; }

        /// <summary>
        ///     Represents the player deck of cards.
        /// </summary>
        TDeck Deck { get; set; }
    }
}