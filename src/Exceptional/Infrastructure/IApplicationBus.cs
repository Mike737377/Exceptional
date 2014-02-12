﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exceptional.Infrastructure
{
    public interface IApplicationBus
    {
        void Send<TMessage>(TMessage message) where TMessage : IMessage;

        void Reply<TMessage>(TMessage message) where TMessage : IMessage;
    }
}