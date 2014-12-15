using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject
{
    
    [Serializable]
    public class TravelException : Exception
    {
        public TravelException() { }
        public TravelException(string message) : base(message) { }
        public TravelException(string message, Exception inner) : base(message, inner) { }
        protected TravelException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }

}
