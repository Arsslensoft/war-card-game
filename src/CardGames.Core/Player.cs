using System;
using CardGames.Core.Contracts;
using CardGames.Core.Enums;

namespace CardGames.Core
{
    /// <summary>
    /// Represents the base class of the player.
    /// </summary>
    /// <typeparam name="TDeck">The  type that represents the customized <see cref="Deck{TCard}" /> for a given game.</typeparam>
    /// <typeparam name="TCard">The type that represents the customized <see cref="Card" /> for a given game.</typeparam>
    public abstract class Player<TDeck, TCard> : IPlayer<TDeck, TCard>, IEquatable<Player<TDeck, TCard>>, IComparable<Player<TDeck, TCard>>
        where TDeck : class, IDeck<TCard>
        where TCard : class, ICard
    {
        /// <inheritdoc />
        public virtual string Name { get; set; }

        /// <inheritdoc />
        public virtual PlayerStatus Status { get; set; }

        /// <inheritdoc />
        public virtual TDeck Deck { get; set; }

        #region IEquatable<T> Implementation
        /// <inheritdoc />
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
        /// <inheritdoc />
        public int CompareTo(Player<TDeck, TCard> other)
        {
            if (other == null) return 1;
            return Name?.CompareTo(other.Name) ?? (string.Equals(Name, other.Name) ? 0 : -1);
        }
        #endregion

        /// <inheritdoc />
        public override string ToString() => $"[{Name}, Deck={Deck}, Status={Status}]";

    }
}