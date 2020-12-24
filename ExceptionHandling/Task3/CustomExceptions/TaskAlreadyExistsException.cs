using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Task3.CustomExceptions
{
    internal class TaskAlreadyExistsException : Exception
    {
        public TaskAlreadyExistsException()
        {
        }

        public TaskAlreadyExistsException(string message) : base(message)
        {
        }

        public TaskAlreadyExistsException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected TaskAlreadyExistsException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
