using System;

namespace Exceptions.Review
{
    [Serializable]
    public class GetAllReviewsFailedException : Exception
    {
        public GetAllReviewsFailedException() { }
        public GetAllReviewsFailedException(string message) : base(message) { }
        public GetAllReviewsFailedException(string message, Exception inner) : base(message, inner) { }
        protected GetAllReviewsFailedException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}