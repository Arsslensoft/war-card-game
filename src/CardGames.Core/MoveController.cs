using CardGames.Core.Contracts;

namespace CardGames.Core
{
    /// <summary>
    /// Represents the base class of the player move during the game.
    /// </summary>
    /// <typeparam name="TPlayer">The  type that represents the customized <see cref="Player{TDeck,TCard}" /> for a given game.</typeparam>
    /// <typeparam name="TDeck">The  type that represents the customized <see cref="Deck{TCard}" /> for a given game.</typeparam>
    /// <typeparam name="TCard">The type that represents the customized <see cref="Card" /> for a given game.</typeparam>
    public abstract class MoveController<TPlayer, TDeck, TCard> : IMoveController<TPlayer, TDeck, TCard>
        where TCard : class, ICard
        where TDeck : class, IDeck<TCard>
        where TPlayer : class, IPlayer<TDeck, TCard>
    {
        /// <inheritdoc />
        public abstract bool Execute(TPlayer player, ICardTray<TPlayer, TDeck, TCard> cardTray);
    }
}