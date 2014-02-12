using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace Exceptional.Client
{
    public static class ExceptionalConfiguration
    {
        static ExceptionalConfiguration()
        {
            if (Config == null)
            {
                Config = new ExceptionalConfigurationInstance();
            }
        }

        public static IExceptionalConfiguration Config { get; set; }
    }
}