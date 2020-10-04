using System;
using System.Diagnostics.CodeAnalysis;
using CardGames.Core;
using CardGames.War.StandardFiftyTwo.Enums;

namespace CardGames.War.StandardFiftyTwo
{
    /// <summary>
    ///     Represents the standard 52-card deck card class.
    /// </summary>
    public class FiftyTwoCardGameCard : Card, IEquatable<FiftyTwoCardGameCard>, IComparable<FiftyTwoCardGameCard>
    {
        public FiftyTwoCardGameCard(Suite suite, Face face, bool isVisible = false)
            : base(isVisible)
        {
            Face = face;
            Suite = suite;
        }

        /// <summary>
        ///     Represents the face of the card.
        /// </summary>
        public Face Face { get; }

        /// <summary>
        ///     Represents the suite of the card.
        /// </summary>
        public Suite Suite { get; }

        /// <inheritdoc />
        public int CompareTo(FiftyTwoCardGameCard other)
        {
            if (ReferenceEquals(other, null))
                return 1;
            return ReferenceEquals(this, other) ? 0 : Face.CompareTo(other.Face);
        }

        /// <inheritdoc />
        public bool Equals([AllowNull] FiftyTwoCardGameCard other)
        {
            if (ReferenceEquals(other, null)) return false;
            return ReferenceEquals(this, other) || Face.Equals(other.Face);
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return $"{base.ToString()}, Face={Face}, Suite={Suite}";
        }

        #region Comparison Operators Implementation
        public static bool operator >(FiftyTwoCardGameCard left, FiftyTwoCardGameCard right)
        {
            if (ReferenceEquals(left, null)) return false;
            return left.CompareTo(right) == 1;
        }
        public static bool operator <(FiftyTwoCardGameCard left, FiftyTwoCardGameCard right)
        {
            if (ReferenceEquals(left, null))
                return !ReferenceEquals(right, null);

            return left.CompareTo(right) == -1;
        }
        public static bool operator ==(FiftyTwoCardGameCard left, FiftyTwoCardGameCard right) => left?.Equals(right) ?? ReferenceEquals(right, null);
        public static bool operator !=(FiftyTwoCardGameCard left, FiftyTwoCardGameCard right) => !(left?.Equals(right) ?? ReferenceEquals(right, null));
        #endregion

    }
}