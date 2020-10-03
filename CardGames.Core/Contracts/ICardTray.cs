using System.Collections.Generic;

namespace CardGames.Core.Contracts
{
    /// <summary>
    /// A Representation of the tray where players put their cards.
    /// </summary>
    /// <typeparam name="TPlayer">The <see cref="IPlayer{TDeck,TCard}"/> subtype.</typeparam>
    /// <typeparam name="TDeck">The <see cref="IDeck{TCard}"/> subtype.</typeparam>
    /// <typeparam name="TCard">The <see cref="ICard"/> subtype.</typeparam>
    public interface ICardTray<TPlayer, TDeck, TCard>
        where TCard : class, ICard
        where TDeck : class, IDeck<TCard>
        where TPlayer : class, IPlayer<TDeck, TCard>
    {
        /// <summary>
        /// Represents the list of players and their corresponding placed cards.
        /// </summary>
        IEnumerable<ICardTraySlot<TPlayer, TDeck, TCard>> PlayedCards { get; }
        /// <summary>
        /// Places a card for a player in the tray.
        /// </summary>
        /// <typeparam name="TCardTraySlot">The <see cref="ICardTraySlot{TPlayer,TDeck,TCard}" /> subtype.</typeparam>
        /// <param name="player">The calling player.</param>
        /// <param name="card">The card to place.</param>
        void Place<TCardTraySlot>(TPlayer player, TCard card)
            where TCardTraySlot : class, ICardTraySlot<TPlayer, TDeck, TCard>, new();
    }
}