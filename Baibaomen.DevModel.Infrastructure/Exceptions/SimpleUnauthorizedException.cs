using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Baibaomen.DevModel.Infrastructure
{
    /// <summary>
    /// When the user doesn't have access to the resource according to the business logic( for example, try to access a private document that belongs to some other user), this exception is thrown.
    /// </summary>
    [Serializable]
    public class SimpleUnauthorizedException : Exception
    {
        public SimpleUnauthorizedException() { }
        public SimpleUnauthorizedException(string message) : base(message) { }
        public SimpleUnauthorizedException(string message, Exception inner) : base(message, inner) { }
        protected SimpleUnauthorizedException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context)
        { }
    }
}
