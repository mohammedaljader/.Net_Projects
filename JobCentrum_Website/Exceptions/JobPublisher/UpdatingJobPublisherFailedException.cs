using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exceptions.JobPublisher
{
    [Serializable]
    // Important: This attribute is NOT inherited from Exception, and MUST be specified 
    // otherwise serialization will fail with a SerializationException stating that
    public class UpdatingJobPublisherFailedException : Exception
    {
        public UpdatingJobPublisherFailedException() { }
        public UpdatingJobPublisherFailedException(string message) : base(message) { }
        public UpdatingJobPublisherFailedException(string message, Exception inner) : base(message, inner) { }
        // Without this constructor, deserialization will fail
        protected UpdatingJobPublisherFailedException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
