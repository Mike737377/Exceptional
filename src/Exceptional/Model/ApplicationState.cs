using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exceptional.Model
{
    public enum VariableType
    {
        Cookie,
        QueryString,
        Application,
        Form,
    }

    public class ApplicationState
    {
        public Guid ExceptionInstance { get; set; }
        public VariableType Type { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
    }
}