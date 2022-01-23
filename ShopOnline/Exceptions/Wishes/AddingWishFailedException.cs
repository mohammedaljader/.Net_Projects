using System;

namespace Exceptions.Wishes
{
    [Serializable]
    public class AddingWishFailedException : Exception
    {
        public AddingWishFailedException() { }
        public AddingWishFailedException(string message) : base(message) { }
        public AddingWishFailedException(string message, Exception inner) : base(message, inner) { }
        protected AddingWishFailedException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}