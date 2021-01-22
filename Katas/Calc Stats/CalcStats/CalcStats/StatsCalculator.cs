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
            throw new NotImplementedException();
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