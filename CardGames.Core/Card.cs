using CardGames.Core.Contracts;

namespace CardGames.Core
{
    public abstract class Card : ICard
    {
        protected Card(bool isVisible = false)
        {
            IsVisible = isVisible;
        }

        /// <inheritdoc cref="ICard" />
        public bool IsVisible { get; set; }

        /// <inheritdoc cref="object" />
        public override string ToString()
        {
            return $"IsVisible={IsVisible}";
        }
    }
}