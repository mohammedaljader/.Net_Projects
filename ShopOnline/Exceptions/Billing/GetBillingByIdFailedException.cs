using System;

namespace Exceptions.Billing
{
    [Serializable]
    public class GetBillingByIdFailedException : Exception
    {
        public GetBillingByIdFailedException() { }
        public GetBillingByIdFailedException(string message) : base(message) { }
        public GetBillingByIdFailedException(string message, Exception inner) : base(message, inner) { }
        protected GetBillingByIdFailedException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}