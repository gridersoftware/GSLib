using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GSLib.Languages
{
    public class Assignment : CodeElement
    {
        public Variable Destination { get; set; }
        public Expression Source { get; set; }
    }
}
