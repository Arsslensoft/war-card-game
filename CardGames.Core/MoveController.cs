using CardGames.Core.Contracts;

namespace CardGames.Core
{
    public abstract class MoveController<TPlayer, TDeck, TCard> : IMoveController<TPlayer, TDeck, TCard>
        where TCard : class, ICard
        where TDeck : class, IDeck<TCard>
        where TPlayer : class, IPlayer<TDeck, TCard>
    {
        public abstract bool Execute(TPlayer player, ICardTray<TPlayer, TDeck, TCard> cardTray);
    }
}