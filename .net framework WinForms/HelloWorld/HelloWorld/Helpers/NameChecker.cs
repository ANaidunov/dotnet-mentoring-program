using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace HelloWorld.Helpers
{
    public static class NameChecker
    {
        public static KeyValuePair<IEnumerable<string>, bool> CheckIsNameCorrect(string name)
        {
            List<string> errorMesseges = new List<string>();
            bool isNameCorrect = true;
            if (string.IsNullOrEmpty(name) || string.IsNullOrWhiteSpace(name))
            {
                errorMesseges.Add("Name shouldn't be empty or contain spaces only");
                isNameCorrect = false;
                return new KeyValuePair<IEnumerable<string>, bool>(errorMesseges, isNameCorrect);
            }

            string nonSpecialChars = "^[a-zA-Z0-9 ]*$";
            if (!Regex.IsMatch(name, nonSpecialChars))
            {
                errorMesseges.Add("Name shouldn't contain special symbols.\n Only letters and numbers are OK");
                isNameCorrect = false;
            }

            char firstLetter = name[0];
            if (char.IsLower(firstLetter))
            {
                errorMesseges.Add("Name shouldn't start with lower case letter");
                isNameCorrect = false;
            }

            return new KeyValuePair<IEnumerable<string>, bool>(errorMesseges, isNameCorrect);
        }
    }
}
