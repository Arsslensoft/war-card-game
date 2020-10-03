﻿using System.Collections.Generic;
using System.Linq;
using CardGames.Core.Contracts;

namespace CardGames.Core
{
    /// <summary>
    /// Represents the round iteration concerning a set of players class.
    /// </summary>
    /// <typeparam name="TPlayer">The <see cref="Player{TDeck,TCard}"/> subtype.</typeparam>
    /// <typeparam name="TDeck">The <see cref="Deck{TCard}"/> subtype.</typeparam>
    /// <typeparam name="TCard">The <see cref="Card"/> subtype.</typeparam>
    public abstract class RoundIteration<TPlayer, TDeck, TCard> : IRoundIteration<TPlayer, TDeck, TCard>
        where TCard : class, ICard
        where TDeck : class, IDeck<TCard>
        where TPlayer : class, IPlayer<TDeck, TCard>
    {
        public virtual IEnumerable<TPlayer> Players { get; protected set; }
        public IRound<TPlayer, TDeck, TCard> CurrentRound { get; set; }
        public ICardTray<TPlayer, TDeck, TCard> CurrentCardTray { get; protected set; }
        public IMoveController<TPlayer, TDeck, TCard> MoveController { get; protected set; }

        public abstract TPlayer Winner { get; }
        public abstract void Play();
        public virtual void Initialize<TMoveController, TCardTray>(IEnumerable<TPlayer> players)
            where TMoveController : class, IMoveController<TPlayer, TDeck, TCard>, new()
            where TCardTray : class, ICardTray<TPlayer, TDeck, TCard>, new()
        {
            MoveController = new TMoveController();
            CurrentCardTray = new TCardTray();
            Players = players;
        }
    }
}