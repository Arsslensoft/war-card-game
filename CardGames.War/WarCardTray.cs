using System.Collections.Generic;
using System.Linq;
using CardGames.Core;
using CardGames.Core.Contracts;
using CardGames.War.StandardFiftyTwo;

namespace CardGames.War
{
    public class WarCardTray : CardTray<FiftyTwoCardGamePlayer, FiftyTwoCardGameDeck, FiftyTwoCardGameCard>
    {
        #region Fields
        private readonly List<ICardTraySlot<FiftyTwoCardGamePlayer, FiftyTwoCardGameDeck, FiftyTwoCardGameCard>> _cardTraySlots = new List<ICardTraySlot<FiftyTwoCardGamePlayer, FiftyTwoCardGameDeck, FiftyTwoCardGameCard>>();
        #endregion

        #region Properties
        public override IEnumerable<ICardTraySlot<FiftyTwoCardGamePlayer, FiftyTwoCardGameDeck, FiftyTwoCardGameCard>>
            PlayedCards => _cardTraySlots;
        #endregion

        #region Methods
        public override void Place<TCardTraySlot>(FiftyTwoCardGamePlayer player, FiftyTwoCardGameCard card)
        {
            var cardTraySlot = PlayedCards.FirstOrDefault(x => x.Player == player);
            if (cardTraySlot == null)
            {
                var newCardTraySlot = new TCardTraySlot()
                {
                    Player = player
                };
                newCardTraySlot.Put(card);
                _cardTraySlots.Add(newCardTraySlot);
            }
            else
                cardTraySlot.Put(card);
        }
        #endregion
    }
}