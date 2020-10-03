using CardGames.Core.Contracts;

namespace CardGames.Core
{
    public abstract class RoundIteration<TPlayer, TDeck, TCard, TCardTray> : IRoundIteration<TPlayer, TDeck, TCard>
        where TCard : class, ICard
        where TDeck : class, IDeck<TCard>
        where TPlayer : class, IPlayer<TDeck, TCard>
        where TCardTray : class, ICardTray<TPlayer, TDeck, TCard>, new()
    {
        protected RoundIteration(IMoveController<TPlayer, TDeck, TCard> moveController)
        {
            CurrentCardTray = new TCardTray();
            MoveController = moveController;
        }

        public ICardTray<TPlayer, TDeck, TCard> CurrentCardTray { get; }
        public IMoveController<TPlayer, TDeck, TCard> MoveController { get; }

        public abstract TPlayer Winner { get; }
        public abstract void Play();

    }
}