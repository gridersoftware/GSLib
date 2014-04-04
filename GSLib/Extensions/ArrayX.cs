using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GSLib.Extensions
{
    public static class ArrayX
    {
        /// <summary>
        /// Gets the index of the last element in an array.
        /// </summary>
        /// <param name="array">Array to get the last index from.</param>
        /// <returns>Returns the index of the last element in the array. If the array has zero elements, the result is negative.</returns>
        public static int GetLastIndex(this Array array)
        {
            return array.Length - 1;
        }

        /// <summary>
        /// Gets the index of the last element in a List.
        /// </summary>
        /// <typeparam name="T">Type of elements in List.</typeparam>
        /// <param name="list">List to get last index from.</param>
        /// <returns>Returns the index of the last element in the list. If the list has zero elements, the result is negative.</returns>
        public static int GetLastIndex<T>(this List<T> list)
        {
            return list.Count - 1;
        }
    }
}
