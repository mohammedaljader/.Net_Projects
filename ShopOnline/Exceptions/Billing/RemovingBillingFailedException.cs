using System;

namespace Exceptions.Billing
{
    [Serializable]
    public class RemovingBillingFailedException : Exception
    {
        public RemovingBillingFailedException() { }
        public RemovingBillingFailedException(string message) : base(message) { }
        public RemovingBillingFailedException(string message, Exception inner) : base(message, inner) { }
        protected RemovingBillingFailedException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}