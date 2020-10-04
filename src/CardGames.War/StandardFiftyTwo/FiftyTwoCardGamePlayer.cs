using System;
using CardGames.Core;
using CardGames.Core.Enums;

namespace CardGames.War.StandardFiftyTwo
{
    /// <summary>
    ///     Represents the player that will use a standard 52-card deck.
    /// </summary>
    public class FiftyTwoCardGamePlayer : Player<FiftyTwoCardGameDeck, FiftyTwoCardGameCard>
    {
        /// <summary>
        /// Represents the time when the player got out of the game.
        /// </summary>
        public DateTimeOffset OutOfGameAt { get; set; }
        /// <inheritdoc />
        public override PlayerStatus Status
        {
            get => base.Status;
            set
            {
                if (value >= PlayerStatus.Won && base.Status != value)
                    OutOfGameAt = DateTimeOffset.UtcNow;

                base.Status = value;
            }
        }

        /// <inheritdoc />
        public override string ToString() => $"[Standard-52 Player, {base.ToString()}]";
    }
}