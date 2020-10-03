using System.Linq;
using CardGames.War.StandardFiftyTwo;
using CardGames.War.StandardFiftyTwo.Enums;
using NUnit.Framework;

namespace CardGames.Core.Tests
{
    public class FiftyTwoCardGameDeckTests
    {

        [Test]
        public void PutTests()
        {
            var sampleDeck = new FiftyTwoCardGameDeck();
            var card = new FiftyTwoCardGameCard(Suite.Clubs, Face.Ace);

            sampleDeck.Put(card);

            Assert.AreEqual(sampleDeck.Cards.Count(), 1);
            Assert.AreEqual(sampleDeck.Cards.First(), card);
        }
        [Test]
        public void DrawSingleTests()
        {
            var sampleDeck = new FiftyTwoCardGameDeck();
            var card = new FiftyTwoCardGameCard(Suite.Clubs, Face.Ace);
            sampleDeck.Put(card);

            var drawnCard = sampleDeck.Draw();
            var secondDrawCard = sampleDeck.Draw();

            Assert.AreEqual(drawnCard, card);
            Assert.IsNull(secondDrawCard);
        }

        [Test]
        public void DrawMultipleTests()
        {
            var sampleDeck = new FiftyTwoCardGameDeck();
            var card = new FiftyTwoCardGameCard(Suite.Clubs, Face.Ace);
            sampleDeck.Put(card);

            var drawnCards = sampleDeck.Draw(2).ToList();

            Assert.AreEqual(drawnCards.Count, 2);
            Assert.AreEqual(drawnCards.First(), card);
            Assert.IsNull(drawnCards.Last());
        }
    }
}