using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GSLib.Mathematics
{
    public interface IVariable
    {
        string Name { get; }
        bool IsConstant { get;}
    }
}
