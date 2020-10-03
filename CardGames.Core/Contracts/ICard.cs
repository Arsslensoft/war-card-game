namespace CardGames.Core.Contracts
{
    /// <summary>
    /// Represents the base interface of all cards.
    /// </summary>
    public interface ICard
    {
        /// <summary>
        /// Indicates whether the card value is visible to the players or not.
        /// </summary>
        bool IsVisible { get; set; }
    }
}