using System;

namespace Exceptions.Cart
{
    [Serializable]
    public class AddingToCartFailedException : Exception
    {
        public AddingToCartFailedException() { }
        public AddingToCartFailedException(string message) : base(message) { }
        public AddingToCartFailedException(string message, Exception inner) : base(message, inner) { }
        protected AddingToCartFailedException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}