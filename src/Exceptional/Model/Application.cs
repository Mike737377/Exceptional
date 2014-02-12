using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exceptional.Model
{
    public class Application : IEntity
    {
        public Guid ApplicationId { get; set; }
        public string Name { get; set; }
        public string Website { get; set; }
        public Guid ApiKey { get; set; }
        public DateTime DateCreated { get; set; }
    }
}