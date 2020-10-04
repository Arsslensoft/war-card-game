using System.Collections.Generic;
using CardGames.Core;
using CardGames.War.StandardFiftyTwo;

namespace CardGames.War
{
    /// <summary>
    ///     Represents the war card game card tray slot class.
    /// </summary>
    public class WarCardTraySlot : CardTraySlot<FiftyTwoCardGamePlayer, FiftyTwoCardGameDeck, FiftyTwoCardGameCard>
    {
        #region Fields

        private readonly Stack<FiftyTwoCardGameCard> _cards = new Stack<FiftyTwoCardGameCard>();

        #endregion

        #region Properties

        /// <inheritdoc />
        public override IEnumerable<FiftyTwoCardGameCard> Cards => _cards;

        #endregion

        #region Methods

        /// <inheritdoc />
        public override void Put(FiftyTwoCardGameCard card)
        {
            _cards.Push(card);
        }

        #endregion
    }
}