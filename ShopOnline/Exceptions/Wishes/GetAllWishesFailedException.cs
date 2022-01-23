using System;

namespace Exceptions.Wishes
{
    [Serializable]
    public class GetAllWishesFailedException : Exception
    {
        public GetAllWishesFailedException() { }
        public GetAllWishesFailedException(string message) : base(message) { }
        public GetAllWishesFailedException(string message, Exception inner) : base(message, inner) { }
        protected GetAllWishesFailedException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}