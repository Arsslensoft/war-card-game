using System.Collections.Generic;

namespace CardGames.Core.Contracts
{
    /// <summary>
    /// The pla
    /// </summary>
    /// <typeparam name="TPlayer"></typeparam>
    /// <typeparam name="TDeck"></typeparam>
    /// <typeparam name="TCard"></typeparam>
    public interface ICardTraySlot<TPlayer, TDeck, TCard>
        where TCard : class, ICard
        where TDeck : class, IDeck<TCard>
        where TPlayer : class, IPlayer<TDeck, TCard>
    {
        TPlayer Player { get; set; }
        IEnumerable<TCard> Cards { get; }
        void Put(TCard card);
    }
}