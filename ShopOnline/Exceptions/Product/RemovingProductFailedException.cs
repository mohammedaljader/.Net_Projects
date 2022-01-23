using System;

namespace Exceptions.Product
{
    [Serializable]
    public class RemovingProductFailedException : Exception
    {
        public RemovingProductFailedException() { }
        public RemovingProductFailedException(string message) : base(message) { }
        public RemovingProductFailedException(string message, Exception inner) : base(message, inner) { }
        protected RemovingProductFailedException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}