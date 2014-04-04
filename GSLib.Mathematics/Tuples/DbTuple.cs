using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GSLib.Mathematics.Tuples
{
    public class DbTuple<TKey, TValue> : NTuple<KeyValuePair<TKey,TValue>>
    {
        public DbTuple(int n) : base(n)
        {

        }
    }
}
