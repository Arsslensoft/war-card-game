namespace CardGames.Core.Contracts
{
    public interface IRoundIteration<TPlayer, TDeck, TCard>
        where TCard : class, ICard
        where TDeck : class, IDeck<TCard>
        where TPlayer : class, IPlayer<TDeck, TCard>
    {
        ICardTray<TPlayer, TDeck, TCard> CurrentCardTray { get; }
        IMoveController<TPlayer, TDeck, TCard> MoveController { get; }
        TPlayer Winner { get; }
        void Play();

        void Initialize<TMoveController, TCardTray>()
            where TMoveController : class, IMoveController<TPlayer, TDeck, TCard>, new()
            where TCardTray : class, ICardTray<TPlayer, TDeck, TCard>, new();

    }
}