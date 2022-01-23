using System;

namespace Exceptions.Order
{
    [Serializable]
    public class GetAllOrdersByUserFailedException : Exception
    {
        public GetAllOrdersByUserFailedException() { }
        public GetAllOrdersByUserFailedException(string message) : base(message) { }
        public GetAllOrdersByUserFailedException(string message, Exception inner) : base(message, inner) { }
        protected GetAllOrdersByUserFailedException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}