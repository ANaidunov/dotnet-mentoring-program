using System.Collections.Generic;
using System.Text;

namespace HelloWorld.Helpers
{
    public static class MessageBuilder
    {
        public static string CreateErrorList(IEnumerable<string> nameCheckErrors)
        {
            StringBuilder stringBuilder = new StringBuilder();

            foreach (string message in nameCheckErrors)
            {
                stringBuilder.Append("- " + message);
                stringBuilder.Append('\n');
            }

            return stringBuilder.ToString();
        }
    }
}
