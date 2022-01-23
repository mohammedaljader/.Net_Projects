using System;

namespace Exceptions.Billing
{
    [Serializable]
    public class AddingBillingFailedException : Exception
    {
        public AddingBillingFailedException() { }
        public AddingBillingFailedException(string message) : base(message) { }
        public AddingBillingFailedException(string message, Exception inner) : base(message, inner) { }
        protected AddingBillingFailedException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}