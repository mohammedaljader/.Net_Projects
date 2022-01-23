using System;

namespace Exceptions.User
{
    [Serializable]
    public class UpdatingUserFailedException : Exception
    {
        public UpdatingUserFailedException() { }
        public UpdatingUserFailedException(string message) : base(message) { }
        public UpdatingUserFailedException(string message, Exception inner) : base(message, inner) { }
        protected UpdatingUserFailedException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}