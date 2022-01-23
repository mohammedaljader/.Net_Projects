using System;

namespace Exceptions.User
{
    [Serializable]
    public class GetUserByIdFailedException : Exception
    {
        public GetUserByIdFailedException() { }
        public GetUserByIdFailedException(string message) : base(message) { }
        public GetUserByIdFailedException(string message, Exception inner) : base(message, inner) { }
        protected GetUserByIdFailedException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}