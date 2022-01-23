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
    public class GetAllJobsFailedException : Exception
    {
        public GetAllJobsFailedException() { }
        public GetAllJobsFailedException(string message) : base(message) { }
        public GetAllJobsFailedException(string message, Exception inner) : base(message, inner) { }
        // Without this constructor, deserialization will fail
        public GetAllJobsFailedException(
         SerializationInfo info,
         StreamingContext context) : base(info, context) { }
    }
}
