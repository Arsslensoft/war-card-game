using System.Collections.Generic;
using CardGames.Core.Contracts;

namespace CardGames.Core
{
    public abstract class CardTraySlot<TPlayer, TDeck, TCard> : ICardTraySlot<TPlayer, TDeck, TCard>
        where TCard : class, ICard
        where TDeck : class, IDeck<TCard>
        where TPlayer : class, IPlayer<TDeck, TCard>
    {
        /// <inheritdoc cref="ICardTraySlot{TPlayer,TDeck,TCard}"/>
        public TPlayer Player { get; set; }
        /// <inheritdoc cref="ICardTraySlot{TPlayer,TDeck,TCard}"/>
        public virtual IEnumerable<TCard> Cards { get; protected set; }

        /// <inheritdoc cref="ICardTraySlot{TPlayer,TDeck,TCard}"/>
        public abstract void Put(TCard card);
    }
}