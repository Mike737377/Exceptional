using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exceptional.Infrastructure
{
    public interface IHandlerRegistry
    {
        IHandler<TMessage> GetHandlerForMessage<TMessage>();
        IHandler<TMessage, TReply> GetHandlerForMessage<TMessage, TReply>();
    }

    public class HandlerRegistry : IHandlerRegistry
    {
        public IHandler<TMessage> GetHandlerForMessage<TMessage>()
        {
            return ServiceFactory.GetInstance<IHandler<TMessage>>();
        }

        public IHandler<TMessage, TReply> GetHandlerForMessage<TMessage, TReply>()
        {
            return ServiceFactory.GetInstance<IHandler<TMessage, TReply>>();
        }
    }
}