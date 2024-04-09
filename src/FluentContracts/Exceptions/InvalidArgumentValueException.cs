using System;
using System.Runtime.Serialization;

namespace FluentContracts.Exceptions
{
    public class InvalidArgumentValueException : ArgumentException
    {
        public InvalidArgumentValueException(string? message)
            : base(message)
        {
        }

        public InvalidArgumentValueException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        public InvalidArgumentValueException(string paramName, string? message)
            : base(message, paramName)
        {
        }

        public InvalidArgumentValueException(string paramName, string? message, Exception innerException)
            : base(message, paramName, innerException)
        {
        }

        protected InvalidArgumentValueException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}