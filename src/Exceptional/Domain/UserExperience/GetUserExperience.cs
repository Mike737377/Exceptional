using Exceptional.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exceptional.Domain.UserExperience
{
    public class GetUserExperience : IMessage
    {
        public Guid ApplicationUserId { get; set; }
    }
}