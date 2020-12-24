using System;

namespace Task1
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("Write your string here");
            string inputString = Console.ReadLine();

            char firstSymbol;
            try
            {
                firstSymbol = StringHandler.GetFirstStringSymbol(inputString);
                Console.WriteLine($"First letter in inputed string is {firstSymbol}");
            }
            catch(ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}