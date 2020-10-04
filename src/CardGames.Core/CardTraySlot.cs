using System.Collections.Generic;
using CardGames.Core.Contracts;

namespace CardGames.Core
{
    /// <summary>
    /// Represents the card tray slot base class containing the placed cards information specific to a player.
    /// </summary>
    /// <typeparam name="TPlayer">The  type that represents the customized <see cref="Player{TDeck,TCard}" /> for a given game.</typeparam>
    /// <typeparam name="TDeck">The  type that represents the customized <see cref="Deck{TCard}" /> for a given game.</typeparam>
    /// <typeparam name="TCard">The type that represents the customized <see cref="Card" /> for a given game.</typeparam>
    public abstract class CardTraySlot<TPlayer, TDeck, TCard> : ICardTraySlot<TPlayer, TDeck, TCard>
        where TCard : class, ICard
        where TDeck : class, IDeck<TCard>
        where TPlayer : class, IPlayer<TDeck, TCard>
    {
        /// <inheritdoc />
        public TPlayer Player { get; set; }

        /// <inheritdoc />
        public virtual IEnumerable<TCard> Cards { get; protected set; }

        /// <inheritdoc />
        public abstract void Put(TCard card);
    }
}