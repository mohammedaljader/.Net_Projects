using System;

namespace Exceptions.Shop
{
    [Serializable]
    public class PaymentFailedException : Exception
    {
        public PaymentFailedException() { }
        public PaymentFailedException(string message) : base(message) { }
        public PaymentFailedException(string message, Exception inner) : base(message, inner) { }
        protected PaymentFailedException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}