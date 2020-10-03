using System;
using System.Diagnostics.CodeAnalysis;
using CardGames.Core;
using CardGames.War.StandardFiftyTwo.Enums;

namespace CardGames.War.StandardFiftyTwo
{
    public class FiftyTwoCardGameCard : Card, IEquatable<FiftyTwoCardGameCard>, IComparable<FiftyTwoCardGameCard>
    {
        public Face Face { get; }
        public Suite Suite { get; }
        public FiftyTwoCardGameCard(Face face, Suite suite, bool isVisible = false)
            : base(isVisible)
        {
            Face = face;
            Suite = suite;
        }

        public int CompareTo(FiftyTwoCardGameCard other)
        {
            if (ReferenceEquals(other, null))
                return 1;
            return ReferenceEquals(this, other) ? 0 : Face.CompareTo(other.Face);
        }

        public bool Equals([AllowNull] FiftyTwoCardGameCard other)
        {
            if (ReferenceEquals(other, null)) return false;
            return ReferenceEquals(this, other) || Face.Equals(other.Face);
        }
    }

}
