using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exceptional.Model
{
    public enum ExceptionInstanceStateType : int
    {
        Server = 0,
        QueryString = 1,
        Application = 2,
        Form = 3,
        Cookie = 4,
    }

    public class ExceptionInstanceState
    {
        public Guid ExceptionInstanceId { get; set; }
        public ExceptionInstanceStateType StateType { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
    }
}