using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GSLib.Mathematics.Sets
{
    public class Multiset<T> : Set<T>
    {
        public new void Add(T item)
        {
            items.Add(item);    
        }

        public void AddRange(T[] items)
        {
            foreach (T item in items)
            {
                Add(item);
            }
        }
    }
}
