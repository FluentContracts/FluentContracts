using System;
using System.Runtime.Serialization;

namespace FluentContracts.Exceptions
{
    public class ArgumentConditionNotSatisfiedException : ArgumentException
    {
        public ArgumentConditionNotSatisfiedException(string message)
            : base(message)
        {
        }

        public ArgumentConditionNotSatisfiedException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        public ArgumentConditionNotSatisfiedException(string paramName, string message)
            : base(message, paramName)
        {
        }

        public ArgumentConditionNotSatisfiedException(string paramName, string message, Exception innerException)
            : base(message, paramName, innerException)
        {
        }

        protected ArgumentConditionNotSatisfiedException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
