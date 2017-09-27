using System;
using System.Runtime.Serialization;

namespace GeoUsers.Services.SQLExceptions
{
    public class ReferencedEntityException : Exception
    {
        public ReferencedEntityException()
        {
        }

        public ReferencedEntityException(string message) : base(message)
        {
        }

        public ReferencedEntityException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ReferencedEntityException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
