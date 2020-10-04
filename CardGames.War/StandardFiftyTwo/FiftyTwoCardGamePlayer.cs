using CardGames.Core;

namespace CardGames.War.StandardFiftyTwo
{
    /// <summary>
    ///     Represents the player that will use a standard 52-card deck.
    /// </summary>
    public class FiftyTwoCardGamePlayer : Player<FiftyTwoCardGameDeck, FiftyTwoCardGameCard>
    {
        /// <inheritdoc cref="Player{TDeck,TCard}"/>
        public override string ToString() => $"[Standard-52 Player, {base.ToString()}]";
    }
}