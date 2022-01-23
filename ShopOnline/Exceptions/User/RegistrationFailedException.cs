using System;

namespace Exceptions.User
{
    [Serializable]
    public class RegistrationFailedException : Exception
    {
        public RegistrationFailedException() { }
        public RegistrationFailedException(string message) : base(message) { }
        public RegistrationFailedException(string message, Exception inner) : base(message, inner) { }
        protected RegistrationFailedException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}