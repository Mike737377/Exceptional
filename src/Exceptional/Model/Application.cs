using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exceptional.Model
{
    public class Application : IEntity
    {

        public Application()
        {
            ApplicationId = GuidHelper.GenerateComb();
        }

        public Application(Guid applicationId)
        {
            ApplicationId = applicationId;
        }

        public Guid ApplicationId { get; protected set; }
        public string Name { get; set; }
        public string Website { get; set; }
        public Guid ApiKey { get; set; }
        public DateTime DateCreated { get; set; }
    }
}