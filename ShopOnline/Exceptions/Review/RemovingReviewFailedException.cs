using System;

namespace Exceptions.Review
{
    [Serializable]
    public class RemovingReviewFailedException : Exception
    {
        public RemovingReviewFailedException() { }
        public RemovingReviewFailedException(string message) : base(message) { }
        public RemovingReviewFailedException(string message, Exception inner) : base(message, inner) { }
        protected RemovingReviewFailedException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}   