using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exceptional.Model
{
    public class ApplicationUser : IEntity
    {
        public Guid ApplicationUserId { get; set; }
        public string UserId { get; set; }
        public DateTime DateCreated { get; set; }
    }
}