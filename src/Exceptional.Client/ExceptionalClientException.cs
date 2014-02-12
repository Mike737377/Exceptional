using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Exceptional.Client
{
    public class ExceptionalClientException : Exception
    {
        public ExceptionalClientException()
        { }

        public ExceptionalClientException(string message)
            : base(message)
        { }

        public ExceptionalClientException(string message, Exception innerException)
            : base(message, innerException)
        { }

        public ExceptionalClientException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        { }
    }
}