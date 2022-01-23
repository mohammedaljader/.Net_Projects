using System;

namespace Exceptions.Product
{
    [Serializable]
    public class AddingProductFailedException : Exception
    {
        public AddingProductFailedException() { }
        public AddingProductFailedException(string message) : base(message) { }
        public AddingProductFailedException(string message, Exception inner) : base(message, inner) { }
        protected AddingProductFailedException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}