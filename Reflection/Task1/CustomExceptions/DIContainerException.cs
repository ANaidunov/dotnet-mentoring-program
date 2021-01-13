using System;
using System.Collections.Generic;
using System.Text;

namespace Task1.CustomExceptions
{
    class DIContainerException : Exception
    {
        public DIContainerException()
        {
        }

        public DIContainerException(string message)
            : base(message)
        {
        }
    }
}
