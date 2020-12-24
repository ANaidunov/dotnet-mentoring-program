using System;

namespace Task1
{
    public static class StringHandler
    {
        public static char GetFirstStringSymbol(string inputString)
        {
            if (string.IsNullOrEmpty(inputString))
            {
                throw new ArgumentNullException($"Exception: {nameof(inputString)} parameter shouldn't be empty");
            }

            return inputString[0];
        }
    }
}
