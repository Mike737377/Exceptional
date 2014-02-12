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
            where TMessage : IMessage
        {
            var handler = handlerRegistry.GetHandlerForMessage<TMessage>();
            handler.Execute(message);
        }

        public void SendAndAwaitReply<TMessage>(TMessage message)
            where TMessage : IMessage
        {
            Send(message);
        }

        public void Reply<TMessage>(TMessage message)
            where TMessage : IMessage
        {
            throw new NotImplementedException();
        }

        public class MessageMediator
        {
            private Dictionary<Guid, object> waitSignals = new Dictionary<Guid, object>();

            public void Notify(MessageEnvelope envelope)
            {
            }

            public void Await(MessageEnvelope envelope)
            {
                //Task.Factory.StartNew(
            }
        }
    }
}