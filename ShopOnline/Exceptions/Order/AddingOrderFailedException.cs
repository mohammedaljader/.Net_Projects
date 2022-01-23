using System;

namespace Exceptions.Order
{
    [Serializable]
    public class AddingOrderFailedException : Exception
    {
        public AddingOrderFailedException() { }
        public AddingOrderFailedException(string message) : base(message) { }
        public AddingOrderFailedException(string message, Exception inner) : base(message, inner) { }
        protected AddingOrderFailedException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}