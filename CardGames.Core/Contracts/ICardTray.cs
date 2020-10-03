using System.Collections.Generic;

namespace CardGames.Core.Contracts
{
    public interface ICardTray<TPlayer, TDeck, TCard>
        where TCard : class, ICard
        where TDeck : class, IDeck<TCard>
        where TPlayer : class, IPlayer<TDeck, TCard>
    {
        IEnumerable<ICardTraySlot<TPlayer, TDeck, TCard>> PlayedCards { get; }
        void Place<TCardTraySlot>(TPlayer player, TCard card)
            where TCardTraySlot : class, ICardTraySlot<TPlayer, TDeck, TCard>, new();
    }
}