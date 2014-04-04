using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GSLib.Extensions
{
    public static class ObjectX
    {
        /// <summary>
        /// Determines if the object is equal to one of the given objects.
        /// </summary>
        /// <param name="obj">Individual object.</param>
        /// <param name="values">Object to compare against.</param>
        /// <returns>Returns true if one or more objects equals the object.</returns>
        public static bool HasValue(this object obj, object[] values)
        {
            foreach (object value in values)
            {
                if (value.Equals(obj)) return true;
            }
            return false;
        }
    }
}
