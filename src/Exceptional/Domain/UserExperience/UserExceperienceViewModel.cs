using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exceptional.Domain.UserExperience
{
    public class UserExceperienceViewModel
    {
        public Guid ApplicationUserId { get; set; }
        public string UserName { get; set; }

        public class ExceptionInstance
        {
            public Guid ExceptionDetailId { get; set; }
            public DateTime DateTime { get; set; }
        }
    }
}