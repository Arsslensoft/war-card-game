using System.Collections.Generic;
using CardGames.Core.Contracts;
using CardGames.Core.Enums;
using CardGames.War.StandardFiftyTwo;
using CardGames.War.StandardFiftyTwo.Enums;
using Moq;
using NUnit.Framework;

namespace CardGames.War.Tests
{
    public class RoundIterationTests
    {

        private Mock<IMoveController<FiftyTwoCardGamePlayer, FiftyTwoCardGameDeck, FiftyTwoCardGameCard>>
            _moveControllerMock;
        private Mock<ICardTray<FiftyTwoCardGamePlayer, FiftyTwoCardGameDeck, FiftyTwoCardGameCard>>
            _cardTray;

        private FiftyTwoCardGamePlayer _jane;
        private FiftyTwoCardGameDeck _sampleDeck;
        private List<FiftyTwoCardGameCard> _cards;
        [SetUp]
        public void SetUp()
        {
            _moveControllerMock = new Mock<IMoveController<FiftyTwoCardGamePlayer, FiftyTwoCardGameDeck, FiftyTwoCardGameCard>>();
            _cardTray = new Mock<ICardTray<FiftyTwoCardGamePlayer, FiftyTwoCardGameDeck, FiftyTwoCardGameCard>>();
            _cards = new List<FiftyTwoCardGameCard>()
            {
                new FiftyTwoCardGameCard(Suite.Clubs, Face.Ace),
                new FiftyTwoCardGameCard(Suite.Hearts, Face.Ten),
                new FiftyTwoCardGameCard(Suite.Hearts, Face.Ace),
            };
            _sampleDeck = new FiftyTwoCardGameDeck();
            _sampleDeck.Put(_cards);
            _jane = new FiftyTwoCardGamePlayer()
            {
                Name = "Jane",
                Status = PlayerStatus.Competing,
                Deck = _sampleDeck
            };
        }

        [Test]
        public void PlayMoveTests()
        {
            var attemptedToPlaceACard = false;
            _moveControllerMock.Setup(p => p.Execute(_jane, _cardTray.Object)).Callback(() =>
            {
                attemptedToPlaceACard = true;
            }).Returns(true);
            var roundIteration = new WarCardRoundIteration()
            {
                Players = new[] { _jane },
                MoveController = _moveControllerMock.Object,
                CurrentCardTray = _cardTray.Object
            };

            roundIteration.Play();


            Assert.IsTrue(attemptedToPlaceACard);
            Assert.AreEqual(_jane.Status, PlayerStatus.Competing);
        }
        [Test]
        public void PlayDeclareLossTests()
        {

            _moveControllerMock.Setup(p => p.Execute(_jane, null)).Returns(false);
            var roundIteration = new WarCardRoundIteration()
            {
                Players = new[] { _jane },
                MoveController = _moveControllerMock.Object,
            };

            roundIteration.Play();

            Assert.AreEqual(_jane.Status, PlayerStatus.Lost);
        }

    }
}