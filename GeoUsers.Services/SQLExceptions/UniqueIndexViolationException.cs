using System;
using System.Runtime.Serialization;

namespace GeoUsers.Services.SQLExceptions
{
    public class UniqueIndexViolationException : Exception
    {
        public UniqueIndexViolationException()
        {
        }

        public UniqueIndexViolationException(string message) : base(message)
        {
        }

        public UniqueIndexViolationException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected UniqueIndexViolationException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
