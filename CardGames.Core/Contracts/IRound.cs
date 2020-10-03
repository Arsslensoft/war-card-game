using System.Collections.Generic;
using System.Linq;

namespace CardGames.Core.Contracts
{
    public interface IRound<TPlayer, TDeck, TCard>
        where TCard : class, ICard
        where TDeck : class, IDeck<TCard>
        where TPlayer : class, IPlayer<TDeck, TCard>
    {
        int Number { get; set; }
        IEnumerable<TCard> AllPlayedCards { get; }
        TPlayer Winner { get; }
        IEnumerable<TPlayer> Players { get; set; }
        IEnumerable<IRoundIteration<TPlayer, TDeck, TCard>> Iterations { get; }
        void Play();
        TRoundIteration CreateIteration<TRoundIteration, TMoveController, TCardTray>(IEnumerable<TPlayer> players = null)
            where TRoundIteration : class, IRoundIteration<TPlayer, TDeck, TCard>, new()
            where TMoveController : class, IMoveController<TPlayer, TDeck, TCard>, new()
            where TCardTray : class, ICardTray<TPlayer, TDeck, TCard>, new();
    }
}