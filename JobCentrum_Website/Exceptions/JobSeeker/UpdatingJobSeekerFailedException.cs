using System;

namespace Exceptions.JobSeeker
{
    [Serializable]
    // Important: This attribute is NOT inherited from Exception, and MUST be specified 
    // otherwise serialization will fail with a SerializationException stating that
    public class UpdatingJobSeekerFailedException : Exception
    {
        public UpdatingJobSeekerFailedException() { }
        public UpdatingJobSeekerFailedException(string message) : base(message) { }
        public UpdatingJobSeekerFailedException(string message, Exception inner) : base(message, inner) { }
        // Without this constructor, deserialization will fail
        protected UpdatingJobSeekerFailedException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
