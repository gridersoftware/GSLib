using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GSLib.Languages
{
    public class Block : CodeElement 
    {
        public List<CodeElement> Elements { get; private set; }

        public Block()
        {
            Elements = new List<CodeElement>();
        }
    }
}
