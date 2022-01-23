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
    public class GetingAllApplicationsByJobSeekerFailedException : Exception
    {
        public GetingAllApplicationsByJobSeekerFailedException() { }
        public GetingAllApplicationsByJobSeekerFailedException(string message) : base(message) { }
        public GetingAllApplicationsByJobSeekerFailedException(string message, Exception inner) : base(message, inner) { }
        // Without this constructor, deserialization will fail
        public GetingAllApplicationsByJobSeekerFailedException(
          SerializationInfo info,
          StreamingContext context) : base(info, context) { }
    }
}
