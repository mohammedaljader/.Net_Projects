using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exceptions.Categorie
{
    [Serializable]
    // Important: This attribute is NOT inherited from Exception, and MUST be specified 
    // otherwise serialization will fail with a SerializationException stating that
    public class AddingCategorieFailedException : Exception
    {
        public AddingCategorieFailedException() { }
        public AddingCategorieFailedException(string message) : base(message) { }
        public AddingCategorieFailedException(string message, Exception inner) : base(message, inner) { }
        // Without this constructor, deserialization will fail
        protected AddingCategorieFailedException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
