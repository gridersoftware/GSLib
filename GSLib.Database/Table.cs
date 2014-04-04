using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GSLib.Database.GDB
{
    class Table
    {
        string name;
        List<string> columns;
        List<Record> records;

        public string Name
        {
            get
            {
                return name;
            }
        }

        public Table(string name)
        {
            this.name = name;
            columns = new List<string>();
            records = new List<Record>();
        }

        public void AddColumn(string column)
        {
            columns.Add(column);
        }

        public void AddRecord(Record record)
        {
            records.Add(record);
        }

        public Record this[int index]
        {
            get
            {
                if (index < 0) throw new ArgumentOutOfRangeException();
                if (index >= records.Count) throw new IndexOutOfRangeException();

                return records[index];
            }
        }
    }
}
