using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GSLib.Mathematics
{
    public class Constants
    {
        public static Variable Pi
        {
            get
            {
                return new Variable("π", Math.PI, true);
            }
        }

        public static Variable E
        {
            get
            {
                return new Variable("e", Math.E, true);
            }
        }
    }
}
