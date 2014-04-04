using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GSLib.Collections.Trees;

namespace GSLib.Setup
{
    class DirectoryDataNameGetter : INameGetter<DirectoryData>
    {

        public string GetName(DirectoryData item)
        {
            return item.Name;
        }

        public string GetName(object item)
        {
            return GetName((DirectoryData)item);
        }
    }
}
