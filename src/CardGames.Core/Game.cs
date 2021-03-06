﻿using System;
using System.Collections.Generic;
using System.Linq;
using CardGames.Core.Contracts;

namespace CardGames.Core
{
    /// <summary>
    /// Represents the base class of a card game.
    /// </summary>
    /// <typeparam name="TPlayer">The  type that represents the customized <see cref="Player{TDeck,TCard}" /> for a given game.</typeparam>
    /// <typeparam name="TDeck">The  type that represents the customized <see cref="Deck{TCard}" /> for a given game.</typeparam>
    /// <typeparam name="TCard">The type that represents the customized <see cref="Card" /> for a given game.</typeparam>
    public abstract class Game<TPlayer, TDeck, TCard> : IGame<TPlayer, TDeck, TCard>
        where TCard : class, ICard
        where TDeck : class, IDeck<TCard>
        where TPlayer : class, IPlayer<TDeck, TCard>
    {
        private readonly List<IRound<TPlayer, TDeck, TCard>> _rounds = new List<IRound<TPlayer, TDeck, TCard>>();

        #region Properties
        /// <inheritdoc />
        public abstract TPlayer Winner { get; }

        /// <inheritdoc />
        public TDeck InitialDeck { get; set; }

        /// <inheritdoc />
        public virtual IEnumerable<TPlayer> Players { get; protected set; }

        /// <inheritdoc />
        public virtual IEnumerable<IRound<TPlayer, TDeck, TCard>> Rounds => _rounds;
        #endregion

        #region Methods
        /// <inheritdoc />
        public abstract void Play();

        /// <inheritdoc />
        public virtual TRound CreateRound<TRound>(IEnumerable<TPlayer> players, int roundNumber)
            where TRound : class, IRound<TPlayer, TDeck, TCard>, new()
        {
            var round = new TRound
            {
                Number = roundNumber,
                Players = players
            };
            _rounds.Add(round);
            return round;
        }

        /// <inheritdoc />
        public virtual void DistributeCards()
        {
            var cardsPerPlayer = InitialDeck.Cards.Count() / Players.Count();
            if (cardsPerPlayer < 1)
                throw new InvalidOperationException("War card game can only be played by 52 players max.");

            var i = 0;
            foreach (var player in Players)
                player.Deck.Put(InitialDeck.Cards.Skip(i++ * cardsPerPlayer).Take(cardsPerPlayer));
        }
        #endregion
    }
}