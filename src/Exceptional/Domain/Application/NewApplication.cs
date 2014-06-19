﻿using Exceptional.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exceptional.Domain.Application
{
    public class NewApplication : IMessage
    {
        public string Name { get; set; }
        public string Website { get; set; }
    }
}