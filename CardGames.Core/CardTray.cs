using System.Collections.Generic;
using System.Linq;
using CardGames.Core.Contracts;

namespace CardGames.Core
{
    public abstract class CardTray<TPlayer, TDeck, TCard> : ICardTray<TPlayer, TDeck, TCard>
        where TCard : class, ICard
        where TDeck : class, IDeck<TCard>
        where TPlayer : class, IPlayer<TDeck, TCard>
    {
        /// <inheritdoc cref="ICardTray{TPlayer,TDeck,TCard}"/>
        public virtual IEnumerable<ICardTraySlot<TPlayer, TDeck, TCard>> PlayedCards { get; protected set; }
        /// <inheritdoc cref="ICardTray{TPlayer,TDeck,TCard}"/>
        public virtual void Place<TCardTraySlot>(TPlayer player, TCard card)
            where TCardTraySlot : class, ICardTraySlot<TPlayer, TDeck, TCard>, new()
        {
            var cardTraySlot = PlayedCards.FirstOrDefault(x => x.Player == player);
            if (cardTraySlot == null)
            {
                var newCardTraySlot = new TCardTraySlot()
                {
                    Player = player
                };
                newCardTraySlot.Put(card);
                PlayedCards = PlayedCards.Union(new ICardTraySlot<TPlayer, TDeck, TCard>[]
                {
                    newCardTraySlot
                });
            }
            else
                cardTraySlot.Put(card);
        }
    }
}