namespace CardGames.Core.Contracts
{
    public interface IMoveController<TPlayer, TDeck, TCard>
        where TCard : class, ICard
        where TDeck : class, IDeck<TCard>
        where TPlayer : class, IPlayer<TDeck, TCard>
    {
        bool Execute(TPlayer player, ICardTray<TPlayer, TDeck, TCard> cardTray);
    }
}