using System;
using CalcStats;
using NUnit.Framework;

namespace CalcStatsTest
{
    public class StatsCalculatorTests
    {
        private StatsCalculator _statsCalculator;

        [SetUp]
        public void Setup()
        {
            _statsCalculator = new StatsCalculator();
        }

        [Test]
        [TestCase(StatsType.Min, null)]
        [TestCase(StatsType.Min, new int[] { })]
        [TestCase(StatsType.Max, null)]
        [TestCase(StatsType.Max, new int[] { })]
        [TestCase(StatsType.Avg, null)]
        [TestCase(StatsType.Avg, new int[] { })]
        [TestCase(StatsType.Count, null)]
        public void ItShouldThrowArgumentExceptionIfArrayIsNullOrEmpty(StatsType statsType, int[] array)
        {
            // assert
            Assert.Throws<ArgumentException>(() => _statsCalculator.GetStats(statsType, array));
        }

        [Test]
        [TestCase(StatsType.Count, new int[] { })]
        public void ItShouldNotThrowArgumentExceptionIfArrayEmptyGetCount(StatsType statsType, int[] array)
        {
            // assert
            Assert.DoesNotThrow(() => _statsCalculator.GetStats(statsType, array));
        }

        [Test]
        [TestCase(StatsType.Min, new[] {1, 2, 3}, 1)]
        [TestCase(StatsType.Min, new[] {1, 0, 3}, 0)]
        [TestCase(StatsType.Min, new[] {-1, -2, -3}, -3)]
        [TestCase(StatsType.Min, new[] {-1, -2, -3, 10, int.MinValue}, int.MinValue)]
        public void ItShouldReturnMinValueOfArray(StatsType statsType, int[] array, int expected)
        {
            // act 
            var result = _statsCalculator.GetStats(statsType, array);

            // assert
            Assert.AreEqual(expected, result);
        }

        [Test]
        [TestCase(StatsType.Max, new[] {1, 2, 3}, 3)]
        [TestCase(StatsType.Max, new[] {1, 0, 3}, 3)]
        [TestCase(StatsType.Max, new[] {-1, -2, -3}, -1)]
        [TestCase(StatsType.Max, new[] {-1, -2, -3, 10, int.MaxValue}, int.MaxValue)]
        public void ItShouldReturnMaxValueOfArray(StatsType statsType, int[] array, int expected)
        {
            // act 
            var result = _statsCalculator.GetStats(statsType, array);

            // assert
            Assert.AreEqual(expected, result);
        }

        [Test]
        [TestCase(StatsType.Count, new[] {1, 2, 3}, 3)]
        [TestCase(StatsType.Count, new[] {1, 0, 3}, 3)]
        [TestCase(StatsType.Count, new int[] { }, 0)]
        public void ItShouldReturnArrayCount(StatsType statsType, int[] array, int expected)
        {
            // act 
            var result = _statsCalculator.GetStats(statsType, array);

            // assert
            Assert.AreEqual(expected, result);
        }

        [Test]
        [TestCase(StatsType.Avg, new[] {5, 5, 5}, 5)]
        [TestCase(StatsType.Avg, new[] {1, 0, 3, 10}, 3.5)]
        [TestCase(StatsType.Avg, new[] {100, -300, 500, 100}, 100)]
        [TestCase(StatsType.Avg, new[] { 6, 9, 15, -2, 92, 11 }, 21.833333333333332)]
        public void ItShouldReturnArrayAverage(StatsType statsType, int[] array, double expected)
        {
            // act 
            var result = _statsCalculator.GetStats(statsType, array);

            // assert
            Assert.AreEqual(expected, result);
        }

        [Test]
        [TestCase(StatsType.Avg, new[] { int.MaxValue, 5, 5 })]
        public void ItShouldThrowOverflowExceptionIfSumOverflowsOnAverage(StatsType statsType, int[] array)
        {
            // assert
            Assert.Throws<OverflowException>(() => _statsCalculator.GetStats(statsType, array));
        }
    }
}