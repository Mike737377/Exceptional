using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exceptional.Model
{
    public interface IEntity
    {
        DateTime DateCreated { get; set; }
    }
}