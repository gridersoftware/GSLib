using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GSLib.Collections.Trees
{
    public sealed class DefaultNameGetter : INameGetter 
    {
        public string GetName(object item)
        {
            return item.ToString();
        }
    }
}
