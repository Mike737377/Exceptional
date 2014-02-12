using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Exceptional
{
    public class ExceptionalException : Exception
    {
        public ExceptionalException()
        { }

        public ExceptionalException(string message)
            : base(message)
        { }

        public ExceptionalException(string message, Exception innerException)
            : base(message, innerException)
        { }

        public ExceptionalException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        { }
    }
}