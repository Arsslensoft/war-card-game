using CardGames.Core.Enums;

namespace CardGames.Core.Contracts
{
    public interface IPlayer<TDeck, TCard>
        where TDeck : class, IDeck<TCard>
        where TCard : class, ICard
    {
        string Name { get; set; }
        PlayerStatus Status { get; set; }
        TDeck Deck { get; set; }
    }
}