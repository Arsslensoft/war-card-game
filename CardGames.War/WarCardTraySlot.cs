using System;
using System.Collections.Generic;
using System.IO.Enumeration;
using System.Text;
using CardGames.Core;
using CardGames.Core.Exception;
using CardGames.War.StandardFiftyTwo;

namespace CardGames.War
{
    public class WarCardTraySlot : CardTraySlot<FiftyTwoCardGamePlayer, FiftyTwoCardGameDeck, FiftyTwoCardGameCard>
    {
        #region Fields
        private readonly Stack<FiftyTwoCardGameCard> _cards = new Stack<FiftyTwoCardGameCard>();
        #endregion

        #region Properties
        public override IEnumerable<FiftyTwoCardGameCard> Cards => _cards;
        #endregion

        #region Methods
        public override void Put(FiftyTwoCardGameCard card)
        {
            _cards.Push(card);
        }
        #endregion
    }
}
