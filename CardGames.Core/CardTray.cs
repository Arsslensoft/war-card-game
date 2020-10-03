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
        public virtual IEnumerable<ICardTraySlot<TPlayer, TDeck, TCard>> PlayedCards { get; private set; }

        public virtual void Place<TCardTraySlot>(TPlayer player, TCard card)
            where TCardTraySlot : class, ICardTraySlot<TPlayer, TDeck, TCard>, new()
        {
            var cardTraySlot = PlayedCards.FirstOrDefault(x => x.Player == player);
            if (cardTraySlot == null)
                PlayedCards = PlayedCards.Union(new ICardTraySlot<TPlayer, TDeck, TCard>[]
                {
                    new TCardTraySlot()
                    {
                        Player = player,
                        Cards = new List<TCard>()
                        {
                            card
                        }
                    },
                });
            else
                cardTraySlot.Cards = cardTraySlot.Cards.Union(new List<TCard>()
                {
                    card
                });
        }
    }
}