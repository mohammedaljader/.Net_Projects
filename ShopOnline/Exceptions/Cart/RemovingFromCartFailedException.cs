using System;

namespace Exceptions.Cart
{
    [Serializable]
    public class RemovingFromCartFailedException : Exception
    {
        public RemovingFromCartFailedException() { }
        public RemovingFromCartFailedException(string message) : base(message) { }
        public RemovingFromCartFailedException(string message, Exception inner) : base(message, inner) { }
        protected RemovingFromCartFailedException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}