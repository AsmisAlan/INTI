using System;
using System.Runtime.Serialization;

namespace GeoUsers.Services.SQLExceptions
{
    public class UniqueConstraintViolationException : Exception
    {
        public UniqueConstraintViolationException()
        {
        }

        public UniqueConstraintViolationException(string message)
            : base(message)
        {
        }

        public UniqueConstraintViolationException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        protected UniqueConstraintViolationException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
