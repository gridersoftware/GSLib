using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GSLib.Database.GDB
{
    public struct Record
    {
        Table sourceTable;
        int id;
        object[] values;

        public Record(Table table, int index, object[] values)
        {
            sourceTable = table;
            id = index;
            this.values = values;
        }

        public Record Next()
        {
            try
            {
                return sourceTable[id + 1];
            }
            catch
            {
                throw;
            }
        }


    }
}
