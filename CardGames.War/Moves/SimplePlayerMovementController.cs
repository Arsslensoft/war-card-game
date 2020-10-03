using CardGames.Core;
using CardGames.Core.Contracts;
using CardGames.War.StandardFiftyTwo;

namespace CardGames.War.Moves
{
    public class SimplePlayerMovementController : MoveController<FiftyTwoCardGamePlayer, FiftyTwoCardGameDeck, FiftyTwoCardGameCard>
    {
        public override bool Execute(FiftyTwoCardGamePlayer player, ICardTray<FiftyTwoCardGamePlayer, FiftyTwoCardGameDeck, FiftyTwoCardGameCard> cardTray)
        {
            var card = player.Deck.Draw();
            if (card == null) // deck is empty the player has lost
                return false;
            cardTray.Place<WarCardTraySlot>(player, card);

            return true;
        }
    }
}