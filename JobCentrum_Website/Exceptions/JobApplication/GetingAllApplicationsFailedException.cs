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
    public class GetingAllApplicationsFailedException : Exception
    {
        public GetingAllApplicationsFailedException() { }
        public GetingAllApplicationsFailedException(string message) : base(message) { }
        public GetingAllApplicationsFailedException(string message, Exception inner) : base(message, inner) { }
        // Without this constructor, deserialization will fail
        public GetingAllApplicationsFailedException(
          SerializationInfo info,
          StreamingContext context) : base(info, context) { }
    }
}
