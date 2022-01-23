using System;

namespace Exceptions.Order
{
    [Serializable]
    public class RemovingOrderFailedException : Exception
    {
        public RemovingOrderFailedException() { }
        public RemovingOrderFailedException(string message) : base(message) { }
        public RemovingOrderFailedException(string message, Exception inner) : base(message, inner) { }
        protected RemovingOrderFailedException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}