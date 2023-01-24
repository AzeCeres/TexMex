using System;
using System.Runtime.Serialization;

namespace OmniDi.Library.Util
{
    [Serializable]
    public class NoValueException : Exception
    {
        public NoValueException()
        {
        }

        public NoValueException(string message) : base(message)
        {
        }

        public NoValueException(string message, Exception inner) : base(message, inner)
        {
        }

        protected NoValueException(SerializationInfo serializationInfo, StreamingContext streamingContext) : base(serializationInfo, streamingContext)
        {
        }
    }
}