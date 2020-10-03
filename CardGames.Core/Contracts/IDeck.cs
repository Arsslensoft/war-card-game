using System.Collections.Generic;

namespace CardGames.Core.Contracts
{
    /// <summary>
    /// Represents the deck of cards interface.
    /// </summary>
    /// <typeparam name="TCard">The <see cref="ICard"/> subtype.</typeparam>
    public interface IDeck<TCard> where TCard : class, ICard
    {
        /// <summary>
        /// Represents the cards list.
        /// </summary>
        IEnumerable<TCard> Cards { get; }
        /// <summary>
        /// Draws a card from the deck if possible.
        /// </summary>
        /// <returns>The drawn card or null.</returns>
        TCard Draw();
        /// <summary>
        /// Draws a set of cards from the deck if possible.
        /// </summary>
        /// <param name="numberOfCards">The number of cards to draw</param>
        /// <returns>The drawn card or null.</returns>
        IEnumerable<TCard> Draw(int numberOfCards);
        /// <summary>
        /// Puts a card in the tray.
        /// </summary>
        /// <param name="card"></param>
        void Put(TCard card);
        /// <summary>
        /// Puts a set of cards in the tray.
        /// </summary>
        /// <param name="cards">The cards to put in the tray.</param>
        void Put(IEnumerable<TCard> cards);
    }
}