using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exceptional.Infrastructure
{
    [Serializable]
    public class MessageEnvelope
    {
        public MessageEnvelope()
        {
            MessageId = Guid.NewGuid();
        }

        public Guid MessageId { get; private set; }
        public Guid? ReplyToMessageId { get; set; }
        public DateTime DateSent { get; set; }
        public Guid? UserId { get; set; }
        public MessageExceptionInfo Error { get; set; }
    }

    [Serializable]
    public class MessageExceptionInfo
    {
        public string Message { get; set; }
        public string StackTrace { get; set; }

        public MessageExceptionInfo InnerExceptionInfo { get; set; }
    }
}