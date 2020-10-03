using System;

namespace CardGames.Core.Helpers
{
    /// <summary>
    /// Represents the utility class.
    /// </summary>
    public static class Utils
    {
        /// <summary>
        /// Represents the singleton of <see cref="System.Random"/>. 
        /// </summary>
        public static Random Random => new Random();
    }
}