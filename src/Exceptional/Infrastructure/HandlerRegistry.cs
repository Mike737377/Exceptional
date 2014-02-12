using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exceptional.Infrastructure
{
    public interface IHandlerRegistry
    {
        IHandler<TMessage> GetHandlerForMessage<TMessage>() where TMessage : IMessage;
    }

    public class HandlerRegistry : IHandlerRegistry
    {
        public IHandler<TMessage> GetHandlerForMessage<TMessage>()
            where TMessage : IMessage
        {
            return ServiceFactory.GetInstance<IHandler<TMessage>>();
        }
    }
}