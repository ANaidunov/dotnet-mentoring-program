using System;
using NUnit.Framework;
using OddEven;

namespace OddEvenTest
{
    [TestFixture]
    public class OddEvenGeneratorTest
    {
        private OddEvenGenerator _generator;

        [SetUp]
        public void SetUp()
        {
            _generator = new OddEvenGenerator();
        }

        [Test]
        [TestCase(1, 100, 100)]
        [TestCase(1, 10, 10)]
        [TestCase(10, 20, 11)]
        public void ItShouldGenerateArrayWithGivenCountOfElements(int start, int end, int expected)
        {
            // act
            var result = _generator.GetNumbers(start, end);
            var resultCount = result.Length;

            // assert 
            Assert.AreEqual(expected, resultCount);
        }

        [TestCase(25,25, new []{"Odd"})]
        [TestCase(27, 27, new[] { "Odd" })]
        [TestCase(35, 35, new[] { "Odd" })]
        [TestCase(9, 9, new[] { "Odd" })]
        public void ItShouldReplaceOddNumberByWordOdd(int start, int end, string [] expected)
        {
            // act
            var resultArray = _generator.GetNumbers(start, end);

            // assert 
            Assert.AreEqual(expected, resultArray);
        }

        [TestCase(12, 12, new[] { "Even" })]
        [TestCase(18, 18, new[] { "Even" })]
        [TestCase(4, 4, new[] { "Even" })]
        [TestCase(98, 98, new[] { "Even" })]
        public void ItShouldReplaceEvenNumberByWordEven(int start, int end, string[] expected)
        {
            // act
            var resultArray = _generator.GetNumbers(start, end);

            // assert 
            Assert.AreEqual(expected, resultArray);
        }

        [Test]
        [TestCase(5, 5, new[] { "5" })]
        [TestCase(7, 7, new[] { "7" })]
        [TestCase(13, 13, new[] { "13" })]
        [TestCase(719, 719, new[] { "719" })]
        public void ItShouldNotReplacePrimeNumbers(int start, int end, string[] expected)
        {
            // act
            var resultArray = _generator.GetNumbers(start, end);

            // assert 
            Assert.AreEqual(expected, resultArray);
        }

        [Test]
        [TestCase(-5, 5)]
        [TestCase(5, -5)]
        [TestCase(5, 1)]
    
        public void ItShouldThrowArgumentExceptionIfBoundariesIsLessThanZero(int start, int end)
        {
            // assert
            Assert.Throws<ArgumentException>(() => _generator.GetNumbers(start, end));
        }
    }
}
