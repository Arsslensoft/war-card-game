using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using CardGames.War.StandardFiftyTwo;
using CardGames.War.StandardFiftyTwo.Enums;
using NUnit.Framework;

namespace CardGames.War.Tests
{
    public class FiftyTwoCardGameDeckTests
    {
        [Test]
        public void PutCollectionTests()
        {
            var sampleDeck = new FiftyTwoCardGameDeck();
            var cards = new List<FiftyTwoCardGameCard>()
            {
                new FiftyTwoCardGameCard(Suite.Clubs, Face.Ace),
                new FiftyTwoCardGameCard(Suite.Spades, Face.Ace),
            };

            sampleDeck.Put(cards);

            Assert.AreEqual(sampleDeck.Cards.Count(), 2);
            Assert.AreEqual(sampleDeck.Cards.First(), cards[0]);
            Assert.AreEqual(sampleDeck.Cards.First(), cards[1]);
        }
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