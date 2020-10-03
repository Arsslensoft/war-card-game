using System.Collections.Generic;

namespace CardGames.Core.Contracts
{
    public interface IDeck<TCard> where TCard : class, ICard
    {
        IEnumerable<TCard> Cards { get; }
        TCard Draw();
        IEnumerable<TCard> Draw(int numberOfCards);
        void Put(TCard card);
        void Put(IEnumerable<TCard> cards);
    }
}