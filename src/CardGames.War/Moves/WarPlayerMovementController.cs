using CardGames.Core;
using CardGames.Core.Contracts;
using CardGames.War.StandardFiftyTwo;

namespace CardGames.War.Moves
{
    /// <summary>
    ///     Represents the war mode move controller class.
    ///     This controller will draw 2 cards and put them in the tray on call.
    ///     The first card will be put face down, while the other facing up on top of the first.
    /// </summary>
    public class
        WarPlayerMovementController : MoveController<FiftyTwoCardGamePlayer, FiftyTwoCardGameDeck, FiftyTwoCardGameCard>
    {
        /// <inheritdoc />
        public override bool Execute(FiftyTwoCardGamePlayer player,
            ICardTray<FiftyTwoCardGamePlayer, FiftyTwoCardGameDeck, FiftyTwoCardGameCard> cardTray)
        {
            var isVisible = false;
            foreach (var fiftyTwoCardGameCard in player.Deck.Draw(2)
            ) // the player must put his card on the tray if he has one
            {
                if (fiftyTwoCardGameCard == null) // deck is empty the player has lost
                    return false;
                if (!isVisible) // set the first card as invisible
                {
                    fiftyTwoCardGameCard.IsVisible = false;
                    isVisible = true;
                }

                cardTray.Place<WarCardTraySlot>(player, fiftyTwoCardGameCard);
            }

            return true;
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return "War Move Mode";
        }
    }
}