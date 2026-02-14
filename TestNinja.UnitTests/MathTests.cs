using NUnit.Framework;
using System.Linq;
using TestNinja.Fundamentals;

namespace TestNinja.UnitTests
{
    [TestFixture]
    public class MathTests
    {
        private Math _math;

        //Setup
        //Teardown
        [SetUp]
        public void Setup()
        {
            _math = new Math();
        }

        [Test]
        //[Ignore("just because")]
        public void Add_WhenCalled_ReturnTheSumOfArguments()
        {
            //act
            var result = _math.Add(1, 2);
            
            //assert
            Assert.AreEqual(3, result);
        }

        [Test]
        [TestCase(2,1,2)]
        [TestCase(1, 2, 2)]
        [TestCase(2, 2, 2)]
        public void Max_WhenCalled_ReturnTheGreaterArgument(int a, int b, int expectedResult)
        {
            var result = _math.Max(a, b);

            Assert.That(result, Is.EqualTo(expectedResult));
        }

        [Test]
        public void GetOddNumbers_LimitIsGreaterThanZero_ReturnOddNumbersUpToLimit()
        {
            var result = _math.GetOddNumbers(5);

            // Assert.That(result, Is.Not.Empty);
            // Assert.That(result.Count(), Is.EqualTo(3));

            //Assert.That(result, Does.Contain(1));
            //Assert.That(result, Does.Contain(3));
            //Assert.That(result, Does.Contain(5));

            Assert.That(result, Is.EquivalentTo(new[] { 1, 3, 5 }));

            //Assert.That(result, Is.Ordered);
            //Assert.That(result, Is.Unique);
        }

        [Test]
        [TestCase(0, TestName = "GetOddNumbers_LimitIsZero_ReturnEmptyList")]
        [TestCase(-2, TestName = "GetOddNumbers_LimitIsNegative_ReturnEmptyList")]
        public void GetOddNumbers_LimitIsLessThanOrEqualToZero_ReturnEmptyList(int number)
        {
            var result = _math.GetOddNumbers(number);

            Assert.That(result, Is.EquivalentTo(new int[] { }));
        }
    }
}
