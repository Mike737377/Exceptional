using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exceptional.Domain.Exceptions
{
    public class ExceptionInfoViewModel
    {
        public Guid ExceptionId { get; set; }
        public string ExceptionType { get; set; }
        public string StackTrace { get; set; }
        public string Message { get; set; }
        public bool IsActive { get; set; }

        public ExceptionInstance[] Instances { get; set; }

        public class ExceptionInstance
        {
            public DateTime Date { get; set; }
            public string Url { get; set; }
        }

        public class ExceptionResolution
        {
            public DateTime DateResolved { get; set; }
            public int UserId { get; set; }
        }
    }
}