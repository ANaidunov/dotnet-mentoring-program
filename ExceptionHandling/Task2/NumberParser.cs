using System;

namespace Task2
{
    public class NumberParser : INumberParser
    {
        public int Parse(string stringValue)
        {
            if (stringValue is null)
            {
                throw new ArgumentNullException();
            }

            string trimedString = stringValue.Trim();
            if (trimedString.Length == 0)
            {
                throw new FormatException();
            }

            bool isNegative = false;
            int startPosition = 0;

            char sign = trimedString[0];
            if (sign == '-')
            {
                startPosition = 1;
                isNegative = true;
            }
            else if(sign == '+')
            {
                startPosition = 1;
            }

            int result = 0;
            char symbol;
            for (int i = startPosition; i < trimedString.Length; i++)
            {
                symbol = trimedString[i];
                if (symbol < '0' || symbol > '9')
                {
                    throw new FormatException();
                }

                result = checked(result * 10 - CharToInt(symbol)); 
            }

            return (isNegative) ? result : checked(-result);
        }

        private static int CharToInt(char c)
        {
            return c - '0';
        }
    }
}