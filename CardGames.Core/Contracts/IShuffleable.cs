namespace CardGames.Core.Contracts
{
    /// <summary>
    /// Represents the interface of an object that supports shuffling.
    /// </summary>
    public interface IShuffleable
    {
        /// <summary>
        /// Suffles the object data.
        /// </summary>
        void Shuffle();
    }
}