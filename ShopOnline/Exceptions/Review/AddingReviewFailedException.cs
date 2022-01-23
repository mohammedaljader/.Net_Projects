using System;

namespace Exceptions.Review
{
    [Serializable]
    public class AddingReviewFailedException : Exception
    {
        public AddingReviewFailedException() { }
        public AddingReviewFailedException(string message) : base(message) { }
        public AddingReviewFailedException(string message, Exception inner) : base(message, inner) { }
        protected AddingReviewFailedException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}