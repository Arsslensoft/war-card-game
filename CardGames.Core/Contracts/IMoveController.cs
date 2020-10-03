namespace CardGames.Core.Contracts
{
    /// <summary>
    /// Represents the player move logic interface.
    /// This interface will hold the logic of the player actions when his turn comes.
    /// </summary>
    /// <typeparam name="TPlayer">The <see cref="IPlayer{TDeck,TCard}"/> subtype.</typeparam>
    /// <typeparam name="TDeck">The <see cref="IDeck{TCard}"/> subtype.</typeparam>
    /// <typeparam name="TCard">The <see cref="ICard"/> subtype.</typeparam>
    public interface IMoveController<TPlayer, TDeck, TCard>
        where TCard : class, ICard
        where TDeck : class, IDeck<TCard>
        where TPlayer : class, IPlayer<TDeck, TCard>
    {
        /// <summary>
        /// Executes the move logic.
        /// </summary>
        /// <param name="player">The target player.</param>
        /// <param name="cardTray">The target card tray.</param>
        /// <returns>A success or failure of execution due to deck issues.</returns>
        bool Execute(TPlayer player, ICardTray<TPlayer, TDeck, TCard> cardTray);
    }
}