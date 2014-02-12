using Exceptional.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exceptional.Domain.Application
{
    public class NewApplicationHandler : IHandler<NewApplication>
    {
        public void Execute(NewApplication message)
        {
            throw new NotImplementedException();
        }
    }
}