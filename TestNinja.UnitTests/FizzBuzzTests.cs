using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestNinja.Fundamentals;

namespace TestNinja.UnitTests
{
    [TestFixture]
    public class FizzBuzzTests
    {
        [Test]
        [TestCase(15)]
        [TestCase(30)]
        [TestCase(45)]
        public void GetOutput_InputIsDivisibleBy3And5_ReturnFizzBuzz(int input)
        {
            var result = FizzBuzz.GetOutput(input);

            Assert.That(result, Is.EqualTo("FizzBuzz"));
        }

        [Test]
        [TestCase(3)]
        [TestCase(6)]
        [TestCase(9)]
        public void GetOutput_InputIsDivisibleBy3Only_ReturnFizz(int input)
        {
            var result = FizzBuzz.GetOutput(input);

            Assert.That(result, Is.EqualTo("Fizz"));
        }


        [Test]
        [TestCase(5)]
        [TestCase(10)]
        [TestCase(20)]
        public void GetOutput_InputIsDivisibleBy5Only_ReturnBuzz(int input)
        {
            var result = FizzBuzz.GetOutput(input);

            Assert.That(result, Is.EqualTo("Buzz"));
        }

        [Test]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(4)]
        public void GetOutput_InputIsNotDivisibleBy3Or5_ReturnTheSameNumber(int input)
        {
            var result = FizzBuzz.GetOutput(input);

            Assert.That(result, Is.EqualTo(input.ToString()));
        }
    }
}
