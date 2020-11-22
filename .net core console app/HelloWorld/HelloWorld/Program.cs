using HelloWorld.Helpers;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;

namespace HelloWorld
{
    class Program
    {
        static void Main(string[] args)
        {
            ConfigurationBuilder builder = new ConfigurationBuilder();
            builder.AddCommandLine(args, new Dictionary<string, string>
            {
                ["-Name"] = "Name"
            });

            IConfigurationRoot config = builder.Build();
            string name = config["Name"];
            KeyValuePair<IEnumerable<string>, bool> nameCheckResult = NameChecker.CheckIsNameCorrect(name);

            bool isNameCorrect = nameCheckResult.Value;
            if (!isNameCorrect)
            {
                IEnumerable<string> nameCheckErrors = nameCheckResult.Key;

                string labelMessage = $"Name \"{name}\" is incorrect!\n";
                string errorList = MessageBuilder.CreateErrorList(nameCheckErrors);

                Console.WriteLine(labelMessage + errorList);
            }
            else
            {
                Console.WriteLine($"Hello {name}!");
            }
        }
    }
}
