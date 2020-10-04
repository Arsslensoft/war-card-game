using CardGames.War.StandardFiftyTwo;
using NUnit.Framework;

namespace CardGames.War.Tests
{
    public class FiftyTwoCardGamePlayerTests
    {
        private FiftyTwoCardGamePlayer _john, _jane, _johnDuplicate;
        [SetUp]
        public void SetUp()
        {
            _john = new FiftyTwoCardGamePlayer()
            {
                Name = "John"
            };
            _jane = new FiftyTwoCardGamePlayer()
            {
                Name = "Jane"
            };
            _johnDuplicate = new FiftyTwoCardGamePlayer()
            {
                Name = "John",
            };
        }
        [Test]
        public void EqualityTests()
        {
            var janeEqualToJohn = _jane.Equals(_john);
            var janeEqualToJane = _jane.Equals(_jane);
            var janeEqualToNull = _jane.Equals(null);
            var johnEqualToJohnDuplicate = _john.Equals(_johnDuplicate);

            Assert.IsTrue(janeEqualToJane);
            Assert.IsTrue(johnEqualToJohnDuplicate);
            Assert.IsFalse(janeEqualToJohn);
            Assert.IsFalse(janeEqualToNull);
        }


    }
}