using System;

namespace StringSum
{
    public class SimpleStringCalculator
    {
        public string Sum(string numString1, string numString2)
        {
            CheckArgs(numString1, numString2);

            var num1 = double.Parse(numString1);
            var num2 = double.Parse(numString2);

            num1 = IsNatural(num1) ? num1 : 0;
            num2 = IsNatural(num2) ? num2 : 0;

            var sum = checked((long)num1 + (long)num2);

            return sum.ToString();
        }

        private void CheckArgs(string num1, string num2)
        {
            if (string.IsNullOrEmpty(num1))
            {
                throw new ArgumentException(nameof(num1));
            }

            if (string.IsNullOrEmpty(num2))
            {
                throw new ArgumentException(nameof(num2));
            }
        }

        private bool IsNatural(double num)
        {
            return Math.Abs(num % 1) < double.Epsilon * 100;
        }
    }
}
