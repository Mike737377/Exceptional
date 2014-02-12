using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exceptional.Model
{
    public class User : IEntity
    {
        public Guid UserId { get; set; }
        public DateTime DateCreated { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
    }
}