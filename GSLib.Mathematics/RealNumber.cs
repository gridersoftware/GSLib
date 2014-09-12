using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GSLib.Mathematics
{
    public class RealNumber
    {
        double Value { get; private set; }

        public RealNumber(double value)
        {
            Value = value;
        }

        public static implicit operator RealNumber(double x)
        {
            return new RealNumber(x);
        }

        public static implicit operator double(RealNumber x)
        {
            return x.Value;
        }
    }
}
