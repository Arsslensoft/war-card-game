using CardGames.Core;
using CardGames.Core.Contracts;
using CardGames.War.StandardFiftyTwo;

namespace CardGames.War.Moves
{
    /// <summary>
    ///     Represents the simple move controller class.
    ///     This controller will draw one card and put it in the tray on call.
    /// </summary>
    public class
        SimplePlayerMoveController : MoveController<FiftyTwoCardGamePlayer, FiftyTwoCardGameDeck, FiftyTwoCardGameCard>
    {
        /// <inheritdoc />
        public override bool Execute(FiftyTwoCardGamePlayer player,
            ICardTray<FiftyTwoCardGamePlayer, FiftyTwoCardGameDeck, FiftyTwoCardGameCard> cardTray)
        {
            var card = player.Deck.Draw();
            if (card == null) // deck is empty the player has lost
                return false;
            cardTray.Place<WarCardTraySlot>(player, card);

            return true;
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return "Simple Move Mode";
        }
    }
}