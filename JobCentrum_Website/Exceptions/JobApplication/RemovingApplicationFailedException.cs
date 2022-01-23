using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Exceptions.JobApplication
{
    [Serializable]
    // Important: This attribute is NOT inherited from Exception, and MUST be specified 
    // otherwise serialization will fail with a SerializationException stating that
    public class RemovingApplicationFailedException : Exception
    {
        public RemovingApplicationFailedException() { }
        public RemovingApplicationFailedException(string message) : base(message) { }
        public RemovingApplicationFailedException(string message, Exception inner) : base(message, inner) { }
        // Without this constructor, deserialization will fail
        public RemovingApplicationFailedException(
          SerializationInfo info,
          StreamingContext context) : base(info, context) { }
    }
}
