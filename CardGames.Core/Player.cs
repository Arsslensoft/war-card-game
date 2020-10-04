using System;
using CardGames.Core.Contracts;
using CardGames.Core.Enums;

namespace CardGames.Core
{
    public abstract class Player<TDeck, TCard> : IPlayer<TDeck, TCard>, IEquatable<Player<TDeck, TCard>>, IComparable<Player<TDeck, TCard>>
        where TDeck : class, IDeck<TCard>
        where TCard : class, ICard
    {
        /// <inheritdoc cref="IPlayer{TDeck,TCard}" />
        public string Name { get; set; }

        /// <inheritdoc cref="IPlayer{TDeck,TCard}" />
        public PlayerStatus Status { get; set; }

        /// <inheritdoc cref="IPlayer{TDeck,TCard}" />
        public TDeck Deck { get; set; }

        #region IEquatable<T> Implementation
        /// <inheritdoc cref="IEquatable{T}" />
        public bool Equals(Player<TDeck, TCard> other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;
            return string.Equals(Name, other.Name) &&
                   Deck == other.Deck &&
                   Status == other.Status;
        }
        #endregion

        #region IComparable<T> Implementation
        /// <inheritdoc cref="IComparable{T}" />
        public int CompareTo(Player<TDeck, TCard> other)
        {
            if (other == null) return 1;
            return Name?.CompareTo(other.Name) ?? (string.Equals(Name, other.Name) ? 0 : -1);
        }
        #endregion

        /// <inheritdoc cref="object" />
        public override string ToString() => $"[{Name}, Deck={Deck}, Status={Status}]";

    }
}