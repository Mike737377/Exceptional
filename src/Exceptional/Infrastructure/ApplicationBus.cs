using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exceptional.Infrastructure
{
    public class ApplicationBus : IApplicationBus
    {
        private readonly IHandlerRegistry handlerRegistry;

        public ApplicationBus(IHandlerRegistry handlerRegistry)
        {
            this.handlerRegistry = handlerRegistry;
        }

        public void Send<TMessage>(TMessage message)
        {
            var handler = handlerRegistry.GetHandlerForMessage<TMessage>();
            handler.Execute(message);
        }

        public TReply Send<TMessage, TReply>(TMessage message)
        {
            var handler = handlerRegistry.GetHandlerForMessage<TMessage, TReply>();
            return handler.Execute(message);
        }

    }
}