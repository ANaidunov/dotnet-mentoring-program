using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace CalcStats
{
    public class StatsCalculator
    {
        public double GetStats(StatsType statsType, int[] array)
        {
            CheckArray(statsType, array);

            switch (statsType)
            {
                case StatsType.Avg:
                    return GetAverage(array);
                case StatsType.Min:
                    return GetMin(array);
                case StatsType.Max:
                    return GetMax(array);
                case StatsType.Count:
                    return array.Length;
                default:
                    throw new ArgumentException(nameof(statsType));
            }
        }

        private void CheckArray(StatsType statsType, int[] array)
        {
            if (array is null || statsType != StatsType.Count & array.Length == 0)
            {
                throw new ArgumentException();
            }
        }

        private double GetAverage(int[] array)
        {
            int sum = 0;
            foreach (var element in array)
            {
                checked
                {
                    sum += element;
                }
            }

            var average = (double)sum / array.Length;
            return average;
        }

        private int GetMin(int[] array)
        {
            var min = array[0];
            if (array.Length == 1)
            {
                return min;
            }

            for (var i = 0; i < array.Length; i++)
            {
                if (min > array[i])
                {
                    min = array[i];
                }
            }

            return min;
        }
        private int GetMax(int[] array)
        {
            var max = array[0];
            if (array.Length == 1)
            {
                return max;
            }

            for (var i = 0; i < array.Length; i++)
            {
                if (max < array[i])
                {
                    max = array[i];
                }
            }

            return max;
        }
    }

    public enum StatsType
    {
        Min,
        Max,
        Count,
        Avg
    }
}