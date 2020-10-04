using System.Collections.Generic;
using CardGames.Core.Contracts;
using CardGames.Core.Helpers;

namespace CardGames.Core
{
    public abstract class Deck<TCard> : IDeck<TCard>, IShuffleable
        where TCard : class, ICard
    {
        private readonly List<TCard> _cards = new List<TCard>();

        #region Properties
        /// <inheritdoc cref="IDeck{TCard}" />
        public virtual bool CanPick => _cards.Count > 0;

        /// <inheritdoc cref="IDeck{TCard}" />
        public virtual IEnumerable<TCard> Cards => _cards;
        #endregion

        #region Methods
        /// <inheritdoc cref="IDeck{TCard}" />
        public virtual TCard Draw()
        {
            if (_cards.Count <= 0) return null;
            var card = _cards[0];
            card.IsVisible = true;
            _cards.RemoveAt(0);
            return card;
        }

        /// <inheritdoc cref="IDeck{TCard}" />
        public IEnumerable<TCard> Draw(int numberOfCards)
        {
            for (var i = 0; i < numberOfCards; i++)
                yield return Draw();
        }

        /// <inheritdoc cref="IDeck{TCard}" />
        public virtual void Put(TCard card)
        {
            card.IsVisible = false;
            _cards.Add(card);
        }

        /// <inheritdoc cref="IDeck{TCard}" />
        public virtual void Put(IEnumerable<TCard> cards)
        {
            foreach (var card in cards)
            {
                card.IsVisible = false;
                _cards.Add(card);
            }
        }
        #endregion

        #region Implementation of IShuffleable
        /// <inheritdoc cref="IDeck{TCard}" />
        public virtual void Shuffle()
        {
            for (var i = _cards.Count - 1; i > 1; i--)
            {
                var randomIndex = Utils.Random.Next(i);
                var randomCard = _cards[randomIndex];
                _cards[randomIndex] = _cards[i];
                _cards[i] = randomCard;
            }
        }
        #endregion

        /// <inheritdoc cref="object" />
        public override string ToString()
        {
            return $"{_cards.Count} cards";
        }
    }
}