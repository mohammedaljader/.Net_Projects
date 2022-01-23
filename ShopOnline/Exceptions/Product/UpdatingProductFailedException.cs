using System;

namespace Exceptions.Product
{
    [Serializable]
    public class UpdatingProductFailedException : Exception
    {
        public UpdatingProductFailedException() { }
        public UpdatingProductFailedException(string message) : base(message) { }
        public UpdatingProductFailedException(string message, Exception inner) : base(message, inner) { }
        protected UpdatingProductFailedException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}