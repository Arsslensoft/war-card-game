using System.Collections.Generic;
using System.Linq;
using CardGames.Core.Contracts;

namespace CardGames.Core
{
    /// <summary>
    /// Represents the base class of card tray where the players place their cards during a game.
    /// </summary>
    /// <typeparam name="TPlayer">The  type that represents the customized <see cref="Player{TDeck,TCard}" /> for a given game.</typeparam>
    /// <typeparam name="TDeck">The  type that represents the customized <see cref="Deck{TCard}" /> for a given game.</typeparam>
    /// <typeparam name="TCard">The type that represents the customized <see cref="Card" /> for a given game.</typeparam>
    public abstract class CardTray<TPlayer, TDeck, TCard> : ICardTray<TPlayer, TDeck, TCard>
        where TCard : class, ICard
        where TDeck : class, IDeck<TCard>
        where TPlayer : class, IPlayer<TDeck, TCard>
    {
        #region Properties
        /// <inheritdoc />
        public virtual IEnumerable<ICardTraySlot<TPlayer, TDeck, TCard>> PlayedCards { get; protected set; }
        #endregion 

        #region Methods
        /// <inheritdoc />
        public virtual void Place<TCardTraySlot>(TPlayer player, TCard card)
            where TCardTraySlot : class, ICardTraySlot<TPlayer, TDeck, TCard>, new()
        {
            var cardTraySlot = PlayedCards.FirstOrDefault(x => x.Player == player);
            if (cardTraySlot == null)
            {
                var newCardTraySlot = new TCardTraySlot
                {
                    Player = player
                };
                newCardTraySlot.Put(card);
                PlayedCards = PlayedCards.Union(new ICardTraySlot<TPlayer, TDeck, TCard>[]
                {
                    newCardTraySlot
                });
            }
            else
            {
                cardTraySlot.Put(card);
            }
        }
        #endregion
    }
}