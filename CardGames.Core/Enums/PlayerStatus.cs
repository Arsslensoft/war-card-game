namespace CardGames.Core.Enums
{
    /// <summary>
    ///     Represents the player status enum.
    /// </summary>
    public enum PlayerStatus : byte
    {
        /// <summary>
        ///     Represents the player in competition mode.
        /// </summary>
        Competing,

        /// <summary>
        ///     Represents the player who won.
        /// </summary>
        Won,

        /// <summary>
        ///     Represents the player who lost.
        /// </summary>
        Lost
    }
}