using System;
using NUnit.Framework;
using StringSum;

namespace StringSumTests
{
    public class SimpleStringCalculatorTest
    {
        private SimpleStringCalculator _simpleStringCalculator;
        [SetUp]
        public void Setup()
        {
            _simpleStringCalculator = new SimpleStringCalculator();
        }

        [Test]
        [TestCase("", "")]
        [TestCase(null, "5")]
        [TestCase("5", null)]
        [TestCase("", "5")]
        [TestCase("5", "")]
        public void SumShouldThrowArgumentExceptionIfArgsAreNullOrEmpty(string num1, string num2)
        {
            Assert.Throws<ArgumentException>(() => _simpleStringCalculator.Sum(num1, num2));
        }

        [Test]
        [TestCase("5", "5", "10")]
        [TestCase("-1", "1", "0")]
        [TestCase("50", "5", "55")]
        public void SumShouldReturnCorrectResult(string num1, string num2, string expected)
        {
            // act
            var actual = _simpleStringCalculator.Sum(num1, num2);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        [TestCase("5.5", "5", "5")]
        [TestCase("-1.5", "1", "1")]
        [TestCase("50.5", "5", "5")]
        public void SumShouldReplaceDoubleNumToZeros(string num1, string num2, string expected)
        {
            // act
            var actual = _simpleStringCalculator.Sum(num1, num2);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        [TestCase("165646465156541656464651565466", "165646465156546")]
        public void SumShouldThrowOverflowExceptionIfLongOverflows(string num1, string num2)
        {
            Assert.Throws<OverflowException>(() => _simpleStringCalculator.Sum(num1, num2));
        }
    }
}