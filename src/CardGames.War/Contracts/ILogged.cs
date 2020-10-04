using Serilog;

namespace CardGames.War.Contracts
{
    /// <summary>
    ///     Represents the interface that adds history logging capability.
    /// </summary>
    public interface ILogged
    {
        /// <summary>
        ///     Represents the default logger.
        /// </summary>
        ILogger Logger { get; set; }

        /// <summary>
        ///     Logs the trace of the calling component.
        /// </summary>
        void Log();
    }
}