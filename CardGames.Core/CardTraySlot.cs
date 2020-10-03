using System.Collections.Generic;
using CardGames.Core.Contracts;

namespace CardGames.Core
{
    public abstract class CardTraySlot<TPlayer, TDeck, TCard> : ICardTraySlot<TPlayer, TDeck, TCard>
        where TCard : class, ICard
        where TDeck : class, IDeck<TCard>
        where TPlayer : class, IPlayer<TDeck, TCard>
    {
        public TPlayer Player { get; set; }
        public virtual IEnumerable<TCard> Cards { get; set; }
    }
}