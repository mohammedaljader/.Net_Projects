using System;

namespace Exceptions.User
{
    [Serializable]
    public class AuthenticationFailedException : Exception
    {
        public AuthenticationFailedException() { }
        public AuthenticationFailedException(string message) : base(message) { }
        public AuthenticationFailedException(string message, Exception inner) : base(message, inner) { }
        protected AuthenticationFailedException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}