using CardGames.Core.Contracts;

namespace CardGames.Core
{
    public abstract class RoundIteration<TPlayer, TDeck, TCard> : IRoundIteration<TPlayer, TDeck, TCard>
        where TCard : class, ICard
        where TDeck : class, IDeck<TCard>
        where TPlayer : class, IPlayer<TDeck, TCard>
    {

        public ICardTray<TPlayer, TDeck, TCard> CurrentCardTray { get; protected set; }
        public IMoveController<TPlayer, TDeck, TCard> MoveController { get; protected set; }

        public abstract TPlayer Winner { get; }
        public abstract void Play();
        public virtual void Initialize<TMoveController, TCardTray>()
            where TMoveController : class, IMoveController<TPlayer, TDeck, TCard>, new()
            where TCardTray : class, ICardTray<TPlayer, TDeck, TCard>, new()
        {
            MoveController = new TMoveController();
            CurrentCardTray = new TCardTray();
        }
    }
}