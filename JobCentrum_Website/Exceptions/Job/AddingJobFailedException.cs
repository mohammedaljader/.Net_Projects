using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Exceptions.Job
{
    [Serializable]
    // Important: This attribute is NOT inherited from Exception, and MUST be specified 
    // otherwise serialization will fail with a SerializationException stating that
    public class AddingJobFailedException : Exception
    {
        public AddingJobFailedException() { }
        public AddingJobFailedException(string message) : base(message) { }
        public AddingJobFailedException(string message, Exception inner) : base(message, inner) { }
        // Without this constructor, deserialization will fail
        public AddingJobFailedException(
          SerializationInfo info,
          StreamingContext context) : base(info, context) { }
    }
}
