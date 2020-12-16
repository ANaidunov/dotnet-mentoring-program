using AdvancedCSharp;
using System;

namespace AdvancedSCharp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Write path here:");
            string path = Console.ReadLine();

            var visitor = new FileSystemVisitor(() => "*.txt");

            visitor.Start += ConsoleLog;
            visitor.Finish += ConsoleLog;
            visitor.FileFound += ConsoleLog;
            visitor.FilteredFileFound += ConsoleLog;
            visitor.DirectoryFound += ConsoleLog;
            visitor.FilteredDirectoryFound += ConsoleLog;

            foreach(var item in visitor.Search(path))
            {
                Console.WriteLine(item);
            }

            foreach (var item in visitor.Search(path, true))
            {
                Console.WriteLine(item);
            }

            Console.ReadKey();
        }

        public static void ConsoleLog(object sender, ProgressArgs args)
        {
            Console.WriteLine(args.Message);
        }
    }
}
