using System;

namespace Exceptions.Product
{
    [Serializable]
    public class GetAllProductsFailedException : Exception
    {
        public GetAllProductsFailedException() { }
        public GetAllProductsFailedException(string message) : base(message) { }
        public GetAllProductsFailedException(string message, Exception inner) : base(message, inner) { }
        protected GetAllProductsFailedException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}