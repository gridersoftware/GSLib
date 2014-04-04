using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GSLib.Mathematics.Tuples
{
    public class OrderedPair<T> : NTuple<T>
    {
        public OrderedPair(T value1, T value2)
            : base(2)
        {
            this[0] = value1;
            this[1] = value2;
        }
    }
}
