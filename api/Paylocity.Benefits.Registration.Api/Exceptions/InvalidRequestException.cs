using System;
using System.Runtime.Serialization;

namespace Paylocity.Benefits.Registration.Api.Exceptions
{
    /// <summary>
    /// An exception that indicates an operation could not be completed because the request was invalid.
    /// </summary>
    [Serializable]
    public class InvalidRequestException : Exception
    {
        private const string DefaultMessage = "The request was not valid and the operation could not be completed";

        public InvalidRequestException() :
            base(InvalidRequestException.DefaultMessage)
        {
        }

        public InvalidRequestException(string message) :
            base(message ?? InvalidRequestException.DefaultMessage)
        {
        }

        public InvalidRequestException(string message, Exception innerException) :
            base(message ?? InvalidRequestException.DefaultMessage, innerException)
        {
        }

        public InvalidRequestException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
    }
}
