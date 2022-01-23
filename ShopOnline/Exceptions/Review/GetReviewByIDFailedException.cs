using System;

namespace Exceptions.Review
{
    [Serializable]
    public class GetReviewByIDFailedException : Exception
    {
        public GetReviewByIDFailedException() { }
        public GetReviewByIDFailedException(string message) : base(message) { }
        public GetReviewByIDFailedException(string message, Exception inner) : base(message, inner) { }
        protected GetReviewByIDFailedException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}