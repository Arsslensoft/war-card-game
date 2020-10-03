using System.Collections.Generic;

namespace CardGames.Core.Contracts
{
    public interface IGame<TPlayer, TDeck, TCard>
        where TCard : class, ICard
        where TDeck : class, IDeck<TCard>
        where TPlayer : class, IPlayer<TDeck, TCard>
    {
        TPlayer Winner { get; }
        IEnumerable<TPlayer> Players { get; }
        IEnumerable<IRound<TPlayer, TDeck, TCard>> Rounds { get; }
        TDeck InitialDeck { get; set; }
        void Play();
        TRound CreateRound<TRound>(IEnumerable<TPlayer> players, int roundNumber)
            where TRound : class, IRound<TPlayer, TDeck, TCard>, new();
        void DistributeCards();
    }
}