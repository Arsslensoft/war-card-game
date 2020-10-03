using System.Collections.Generic;

namespace CardGames.Core.Contracts
{
    /// <summary>
    /// Represents the cards tray slot interface.
    /// </summary>
    /// <typeparam name="TPlayer">The <see cref="IPlayer{TDeck,TCard}"/> subtype.</typeparam>
    /// <typeparam name="TDeck">The <see cref="IDeck{TCard}"/> subtype.</typeparam>
    /// <typeparam name="TCard">The <see cref="ICard"/> subtype.</typeparam>
    public interface ICardTraySlot<TPlayer, TDeck, TCard>
        where TCard : class, ICard
        where TDeck : class, IDeck<TCard>
        where TPlayer : class, IPlayer<TDeck, TCard>
    {
        /// <summary>
        /// Represents the owner player.
        /// </summary>
        TPlayer Player { get; set; }
        /// <summary>
        /// Represents the list of cards that the player has put in the tray.
        /// </summary>
        IEnumerable<TCard> Cards { get; }
        /// <summary>
        /// Puts a card in the tray.
        /// </summary>
        /// <param name="card">The card to put in the tray.</param>
        void Put(TCard card);
    }
}