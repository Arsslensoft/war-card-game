using CardGames.Core;

namespace CardGames.War.StandardFiftyTwo
{
    /// <summary>
    ///     Represents the standard 52-card deck.
    /// </summary>
    public class FiftyTwoCardGameDeck : Deck<FiftyTwoCardGameCard>
    {
        /// <inheritdoc cref="Deck{TCard}"/>
        public override string ToString() => $"[Standard-52 Deck, {base.ToString()}]";
    }
}