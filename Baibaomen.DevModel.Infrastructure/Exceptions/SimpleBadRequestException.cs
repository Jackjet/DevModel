using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Baibaomen.DevModel.Infrastructure
{
    /// <summary>
    /// Throw this exception when you want to notice the client about some obvious mistake and don't need to log the error.
    /// </summary>
    [Serializable]
    public class SimpleBadRequestException : Exception
    {
        public SimpleBadRequestException() { }
        public SimpleBadRequestException(string message) : base(message) { }
        public SimpleBadRequestException(string message, Exception inner) : base(message, inner) { }
        protected SimpleBadRequestException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context)
        { }
    }
}
