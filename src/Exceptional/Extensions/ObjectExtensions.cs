using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exceptional
{
    public static class ObjectExtensions
    {
        public static string ToJsonString(this object theObject)
        {
            return JsonConvert.SerializeObject(theObject);
        }
    }
}