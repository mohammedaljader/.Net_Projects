using System;

namespace Exceptions.Product
{
    [Serializable]
    public class GetProductByIdFailedException : Exception
    {
        public GetProductByIdFailedException() { }
        public GetProductByIdFailedException(string message) : base(message) { }
        public GetProductByIdFailedException(string message, Exception inner) : base(message, inner) { }
        protected GetProductByIdFailedException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}