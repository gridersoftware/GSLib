using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GSLib.Collections.Trees
{
    public interface INameGetter
    {
        /// <summary>
        /// Returns the name of an object.
        /// </summary>
        /// <returns>Returns a string containing the name of an object.</returns>
        string GetName(object item);
    }

    public interface INameGetter<T> : INameGetter 
    {
        /// <summary>
        /// Returns the name of an object.
        /// </summary>
        /// <returns>Returns a string containing the name of an object.</returns>
        string GetName(T item);
    }
}
