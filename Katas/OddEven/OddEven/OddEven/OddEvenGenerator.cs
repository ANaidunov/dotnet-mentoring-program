using System;
using System.Collections.Generic;

namespace OddEven
{
    public class OddEvenGenerator
    {
        public string[] GetNumbers(int start, int end)
        {
            CheckBoundaries(start, end);

            var numbers = new List<string>();

            for (var i = start; i < end + 1; i++)
            {
                if (IsNumberPrime(i))
                {
                    numbers.Add(i.ToString());
                }
                else if (IsNumberOdd(i))
                {
                    numbers.Add("Odd");
                }
                else
                {
                    numbers.Add("Even");
                }
            }

            return numbers.ToArray();
        }

        private void CheckBoundaries(int start, int end)
        {
            if (start < 0)
            {
                throw new ArgumentException("Start should be greater than zero", nameof(start));
            }

            if (end < 0)
            {
                throw new ArgumentException("End should be greater than zero", nameof(end));
            }

            if (end < start)
            {
                throw new ArgumentException("End should be greater than start");
            }
        }

        private bool IsNumberPrime(int number)
        {
            if (number <= 1) return false;
            if (number == 2) return true;
            if (number % 2 == 0) return false;

            var boundary = (int)Math.Floor(Math.Sqrt(number));

            for (int i = 3; i <= boundary; i += 2)
            {
                if (number % i == 0)
                {
                    return false;
                }
            }

            return true;
        }

        private bool IsNumberOdd(int number)
        {
            return number % 2 != 0;
        }
    }
}
