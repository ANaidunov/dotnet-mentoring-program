﻿using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Task3.CustomExceptions
{
    internal class InvalidUserIdException : Exception
    {
        public InvalidUserIdException()
        {
        }

        public InvalidUserIdException(string message) : base(message)
        {
        }

        public InvalidUserIdException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected InvalidUserIdException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
