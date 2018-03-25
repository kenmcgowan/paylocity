using System;
using System.Runtime.Serialization;

namespace Paylocity.Benefits.Registration.Api.Exceptions
{
    [Serializable]
    public class NonexistentDataException : InvalidRequestException
    {
        private const string DefaultMessage = "The requested data does not exist";

        public string Id
        {
            get;
            private set;
        }

        public NonexistentDataException(string id) :
            base(NonexistentDataException.DefaultMessage)
        {
            Id = id;
        }

        public NonexistentDataException(string message, string id) :
            base(message ?? NonexistentDataException.DefaultMessage)
        {
            Id = id;
        }

        public NonexistentDataException(string message, Exception innerException, string id) :
            base(message ?? NonexistentDataException.DefaultMessage, innerException)
        {
            Id = id;
        }

        public override string ToString()
        {
            return $"(Id = \"{Id}\") {base.ToString()}";
        }

        public NonexistentDataException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
    }
}
