using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exceptional.Infrastructure
{
    public interface IHandler<TMessage>
    {
        void Execute(TMessage message);
    }

    public interface IHandler<TMessage, TReply>
    {
        TReply Execute(TMessage message);
    }
}