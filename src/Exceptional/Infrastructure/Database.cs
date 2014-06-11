using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NPoco;

namespace Exceptional.Infrastructure
{
    public class Database : NPoco.Database
    {

        public Database()
            : this("Exceptional")
        { }

        public Database(string connectionStringName)
            : base(connectionStringName)
        { }

    }
}