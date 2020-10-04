using CardGames.War.StandardFiftyTwo;
using CardGames.War.StandardFiftyTwo.Enums;
using NUnit.Framework;

namespace CardGames.War.Tests
{
    public class FiftyTwoCardGameCardTests
    {
        private FiftyTwoCardGameCard _aceOfClubs, _tenOfSpades, _aceOfSpades;
        [SetUp]
        public void SetUp()
        {
            _aceOfClubs = new FiftyTwoCardGameCard(Suite.Clubs, Face.Ace);
            _aceOfSpades = new FiftyTwoCardGameCard(Suite.Spades, Face.Ace);
            _tenOfSpades = new FiftyTwoCardGameCard(Suite.Spades, Face.Ten);
        }
        [Test]
        public void InequalityOperatorTests()
        {
            var aceToAceNotEqual = _aceOfClubs != _aceOfSpades;
            var nullNotEqual = _aceOfClubs != null;
            var commutativeNullEqual = null != _aceOfClubs;

            Assert.IsFalse(aceToAceNotEqual);
            Assert.IsTrue(nullNotEqual);
            Assert.IsTrue(commutativeNullEqual);
        }
        [Test]
        public void EqualityOperatorTests()
        {
            var aceToAceEqual = _aceOfClubs == _aceOfSpades;
            var nullEqual = _aceOfClubs == null;
            var commutativeNullEqual = null == _aceOfClubs;

            Assert.IsTrue(aceToAceEqual);
            Assert.IsFalse(nullEqual);
            Assert.IsFalse(commutativeNullEqual);
        }

        [Test]
        public void GreaterThanOperatorTests()
        {

            var tenToAceGreaterThan = _tenOfSpades > _aceOfClubs;
            var nullGreaterThan = _aceOfClubs > null;

            Assert.IsFalse(tenToAceGreaterThan);
            Assert.IsTrue(nullGreaterThan);
        }

        [Test]
        public void LessThanOperatorTests()
        {

            var tenToAceLessThan = _tenOfSpades < _aceOfClubs;
            var nullLessThan = null < _aceOfClubs;

            Assert.IsTrue(tenToAceLessThan);
            Assert.IsTrue(nullLessThan);
        }


        [Test]
        public void EquatableTests()
        {

            var aceToAceEqual = _aceOfClubs.Equals(_aceOfSpades);
            var tenToAceNotEqual = _tenOfSpades.Equals(_aceOfClubs);
            var nullEqual = _tenOfSpades.Equals(null);

            Assert.IsTrue(aceToAceEqual);
            Assert.IsFalse(nullEqual);
            Assert.IsFalse(tenToAceNotEqual);
        }
    }
}