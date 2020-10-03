using CardGames.Core.Contracts;
using CardGames.Core.Enums;

namespace CardGames.Core
{
    public abstract class Player<TDeck, TCard> : IPlayer<TDeck, TCard>
        where TDeck : class, IDeck<TCard>
        where TCard : class, ICard
    {
        public string Name { get; set; }
        public PlayerStatus Status { get; set; }
        public TDeck Deck { get; set; }

        public override string ToString()
        {
            return $"[{Name}, Deck={Deck}, Status={Status}]";
        }
    }
}