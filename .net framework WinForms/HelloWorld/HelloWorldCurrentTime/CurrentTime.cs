using System;

namespace HelloWorldCurrentTime
{
    public static class CurrentTime
    {
        public static string GetCurrentTime()
        {
            return DateTime.Now.ToString("h:mm:ss tt");
        }
    }
}
