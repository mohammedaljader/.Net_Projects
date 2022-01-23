using System;

namespace Exceptions.Wishes
{
    [Serializable]  
    public class RemovingWishesFailedException : Exception
    {
        public RemovingWishesFailedException() { }
        public RemovingWishesFailedException(string message) : base(message) { }
        public RemovingWishesFailedException(string message, Exception inner) : base(message, inner) { }
        protected RemovingWishesFailedException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}