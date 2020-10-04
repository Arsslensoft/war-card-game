using CardGames.Core.Contracts;

namespace CardGames.Core
{
    /// <summary>
    /// Represents the base representation class of a game card.
    /// </summary>
    public abstract class Card : ICard
    {
        protected Card(bool isVisible = false)
        {
            IsVisible = isVisible;
        }

        /// <inheritdoc />
        public bool IsVisible { get; set; }

        /// <inheritdoc />
        public override string ToString()
        {
            return $"IsVisible={IsVisible}";
        }
    }
}