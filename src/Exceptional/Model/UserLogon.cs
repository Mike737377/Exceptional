using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exceptional.Model
{
    public class UserLogon
    {
        public Guid UserId { get; set; }
        public DateTime LogonDate { get; set; }
    }
}