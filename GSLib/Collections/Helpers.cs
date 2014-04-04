using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GSLib.Collections
{
    /// <summary>
    /// The Helpers class provides helper functions for other Collections classes, including comparison functions.
    /// </summary>
    /// VB: Public NotInheritable Class Helpers
    public sealed class Helpers
    {
        /// <summary>
        /// Deep clones a LinkedList, returning a copy of the original.
        /// </summary>
        /// <typeparam name="T">Type of LinkedList.</typeparam>
        /// <param name="original">Original LinkedList to make a copy of.</param>
        /// <returns>Returns a copy of the original LinkedList.</returns>
        /// VB: Public Shared Function DeepCloneLinkedList(Of T)(ByVal original As LinkedList(Of T)) As LinkedList(Of T)
        public static LinkedList<T> DeepCloneLinkedList<T>(LinkedList<T> original)
        {
            if (original == null)
            {
                return null;
            }

            LinkedList<T> newList = new LinkedList<T>();
            LinkedListNode<T> item = original.First;

            while (item != null)
            {
                newList.AddLast(item.Value);
                item = item.Next;
            }
            return newList;
        }

        /// <summary>
        /// Determines if the given type implements System.IComparable.
        /// </summary>
        /// <typeparam name="T">Type to check</typeparam>
        /// <returns>Returns true if T implements IComparable, otherwise returns false.</returns>
        /// VB: Public Shared Function IsComparable(Of T)() As Boolean
        public static bool IsComparable<T>()
        {
            return (IsComparable(typeof(T)));
        }

        /// <summary>
        /// Determines if the given Type implements System.IComparable.
        /// </summary>
        /// <param name="t">Type to check.</param>
        /// <returns>Returns true if the type implements IComparable, otherwise returns false.</returns>
        /// <exception cref="ArgumentNullException">Thrown if the Type is null.</exception>
        /// VB: Public Shared Function IsComparable(ByVal t As Type) As Boolean
        public static bool IsComparable(Type t)
        {
            if (t == null) throw new ArgumentNullException();
            return (t.GetInterface("System.IComparable") != null);
        }

        /// <summary>
        /// Determines if the given type is a System.String or System.Char.
        /// </summary>
        /// <typeparam name="T">Type to check</typeparam>
        /// <returns>Returns true if the type is a string or char, otherwise returns false.</returns>
        /// VB: Public Shared Function IsStringOrChar(Of T)() As Boolean
        public static bool IsStringOrChar<T>()
        {
            return IsStringOrChar(typeof(T));
        }

        /// <summary>
        /// Determines if the given Type is a System.String or System.Char.
        /// </summary>
        /// <param name="t">Type to check.</param>
        /// <returns>Returns true if the type is a string or char, otherwise returns false.</returns>
        /// <exception cref="ArgumentNullException">Thrown if the Type is null.</exception>
        /// VB: Public Shared Function IsStringOrChar(ByVal t As Type) As Boolean
        public static bool IsStringOrChar(Type t)
        {
            if (t == null) throw new ArgumentNullException();
            return ((t.IsAssignableFrom(typeof(string))) | (t.IsAssignableFrom(typeof(char))));
        }

        /// <summary>
        /// Compares two values of type T using the given System.Collection.IComparer, if T does not implement 
        /// System.IComparable. Otherwise, returns the result of Compare&lt;T&gt;(T,T).
        /// </summary>
        /// <typeparam name="T">Type of value.</typeparam>
        /// <param name="value1">First value to check.</param>
        /// <param name="value2">Second value to check.</param>
        /// <param name="comparer">IComparer to use.</param>
        /// 
        /// <returns>Returns a signed integer indicating the relative values of X (value1) and Y (value2), 
        /// where: value is less than zero, X is less than Y; value is zero, X and Y are equal; and value 
        /// is greater than zero, X is greater than Y.</returns>
        /// 
        /// <remarks>This is best used to compare two values of any type, where the type does not implement 
        /// System.IComparable. In this case, you should build a custom class that implements
        /// System.Collections.IComparer, and pass that along to this. If T implements IComparable, then
        /// the function will call Compare&lt;T&gt;(T,T) and return the result, ignoring the IComparer provided.
        /// 
        /// This function is used by GSLib.Collections classes, and is not intended
        /// to replace, but rather supplement, IComparer.Compare() and IComparable.CompareTo()
        /// for the purposes of sorting.
        /// 
        /// This function cannot compare null values. If you need to check for nulls, you should compare the object(s)
        /// to null using the equality or inequality operators.
        /// </remarks>
        /// 
        /// <exception cref="CompareException">Thrown if IComparer.Compare() throws an exception; or if T is 
        /// IComparable, thrown when Compare&lt;T&gt;(T,T) throws an exception.</exception>
        /// <exception cref="ArgumentNullException">Thrown if T does not implement IComparable, and comparer is null.</exception>
        /// <seealso cref="Compare{T}(T,T)"/>
        /// 
        /// <example>
        /// using System;
        /// using System.Collections;
        /// using GSLib.Collections;
        /// 
        /// namespace Example
        /// {
        ///     // Notice that ExampleType does not use IComparable. Instead, we have to build a class that implements IComparer.
        ///     class ExampleType
        ///     {
        ///         public int value;
        ///         
        ///         public ExampleType(int value)
        ///         {
        ///             this.value = value;
        ///         }
        ///     }
        ///     
        ///     // Our example IComparer class.
        ///     class ExampleComparer : IComparer&lt;ExampleType&gt;
        ///     {
        ///         public int IComparer&lt;Example&gt;.Compare(ExampleType x, ExampleType y)
        ///         {
        ///             return x.value - y.value;
        ///         }
        ///     }
        ///     
        ///     class Program
        ///     {
        ///         void Main()
        ///         {
        ///             ExampleType a = new ExampleType(5);
        ///             ExampleType b = new ExampleType(10);
        ///             
        ///             int result = Helpers.Compare&lt;ExampleType&gt;(ExampleType a, ExampleType b, new ExampleComparer());
        ///             Console.WriteLine(Result: {0}, result);
        ///         }
        ///     }
        ///     
        ///     /*
        ///     Output:
        ///     Result: -5
        ///     */
        /// }
        /// </example>
        /// VB: Public Shared Function Compare(Of T)(ByVal value1 As T, ByVal value2 As T, ByVal comparer As IComparer(Of T)) As Integer
        public static int Compare<T>(T value1, T value2, IComparer<T> comparer)
        {
            if (!IsComparable<T>())
            {
                if (comparer == null) throw new ArgumentNullException();
                try
                {
                    return comparer.Compare(value1, value2);
                }
                catch (ArgumentException ex)
                {
                    throw new CompareException("Values cannot be compared because one or more arguments were invalid. See InnerException for details.", ex);
                }
                catch (Exception ex)
                {
                    throw new CompareException("Values cannot be compared with given comparer. See InnerException for details.", ex);
                }
            }
            else
            {
                try
                {
                    return Compare<T>(value1, value2);
                }
                catch
                {
                    throw;
                }
            }
        }

        /// <summary>
        /// Compares two values of type T if T implements System.IComparable.
        /// </summary>
        /// <typeparam name="T">Type of objects to compare.</typeparam>
        /// <param name="value1">First object to compare.</param>
        /// <param name="value2">Second object to compare.</param>
        /// <returns>Returns less than zero if the first object is less than the second. Returns zero 
        /// if the two objects are equal. Returns greater than zero if the first object is greater than the second.</returns>
        /// <exception cref="CompareException">Thrown when type T does not implement System.IComparable. If the type does
        /// not implement IComparable, try using Compare&lt;T&gt;(T,T,IComparer&lt;T&gt;).</exception>
        /// <exception cref="ArgumentNullException">Thrown if value1 or value2 is null.</exception>
        /// <remarks>This function cannot compare null values. If you need to check for nulls, you should compare the object(s) to null using the equality or inequality operators.</remarks>
        /// VB: Public Shared Function Compare(Of T)(ByVal value1 As T, ByVal value2 As T) As Integer
        public static int Compare<T>(T value1, T value2)
        {
            if (value1 == null | value2 == null)
            {
                throw new ArgumentNullException();
            }

            if (IsComparable<T>())
            {
                IComparable o1 = (IComparable)value1;
                IComparable o2 = (IComparable)value2;
                int result = o1.CompareTo(o2);
                return result;
            }
            else
            {
                throw new CompareException("Values must implement IComparable. Compare using Compare<T>(T, T, IComparer<T>).");
            }
        }

        /// <summary>
        /// Compares two arrays of objects.
        /// </summary>
        /// <typeparam name="T">Type of objects to compare.</typeparam>
        /// <param name="array1">First array to compare.</param>
        /// <param name="array2">Second array to compare.</param>
        /// <returns>If the arrays are of equal length, returns the first non-zero result of comparing each item to its partner. 
        /// If both arrays are completely equal, returns zero. If array1 is shorter than array2 returns less than zero. If 
        /// array1 is longer than array2, returns greater than zero.</returns>
        /// <exception cref="CompareException">Thrown if T does not implement IComparable.</exception>
        /// <exception cref="ArgumentNullException">Thrown if one or both arrays are null.</exception>
        /// <remarks>This function cannot compare null values. If you need to check for nulls, you should compare the object(s) to null using the equality or inequality operators.</remarks>
        /// VB: Public Shared Function CompareArrays(Of T)(ByVal array1() As T, ByVal array2() As T) As Integer
        public static int CompareArrays<T>(T[] array1, T[] array2)
        {
            if (array1 == null | array2 == null) throw new ArgumentNullException();

            if (array1.Length != array2.Length)
            {
                return array1.Length - array2.Length;
            }
            else
            {
                for (int i = 0; i < array1.Length; i++)
                {
                    try
                    {
                        int c = Compare<T>(array1[i], array2[i]);
                        if (c != 0) return c;
                    }
                    catch (CompareException)
                    {
                        throw new CompareException("Array types must implement IComparable. Compare using CompareArrays<T>(T[], T[], IComparer<T>).");
                    }
                }
                return 0;
            }
        }

        /// <summary>
        /// Compares two arrays of objects using the given IComparer&lt;T&gt;.
        /// </summary>
        /// <typeparam name="T">Type of objects to compare.</typeparam>
        /// <param name="array1">First array to compare.</param>
        /// <param name="array2">Second array to compare.</param>
        /// <param name="comparer">IComparer&lt;T&gt; to use.</param>
        /// <returns>If the arrays are of equal length, returns the first non-zero result of comparing each item to its partner. 
        /// If both arrays are completely equal, returns zero. If array1 is shorter than array2 returns less than zero. If 
        /// array1 is longer than array2, returns greater than zero.</returns>
        /// <exception cref="ArgumentNullException">Thrown if one or both arrays are null, or if any array values are null.</exception>
        /// <exception cref="CompareException">Thrown if IComparer.Compare() throws an exception; or if T is 
        /// IComparable, thrown when Compare&lt;T&gt;(T,T) throws an exception.</exception>
        /// <remarks>This function cannot compare null values. If you need to check for nulls, you should compare the object(s) to null using the equality or inequality operators.</remarks>
        /// VB: Public Shared Function CompareArrays(Of T)(ByVal array1() As T, ByVal array2() As T, ByVal comparer As IComparer(Of T)) As Integer
        public static int CompareArrays<T>(T[] array1, T[] array2, IComparer<T> comparer)
        {
            if (array1 == null | array2 == null | comparer == null) throw new ArgumentNullException();

            if (array1.Length != array2.Length)
            {
                return array1.Length - array2.Length;
            }
            else
            {
                for (int i = 0; i < array1.Length; i++)
                {
                    try
                    {
                        int c = Compare<T>(array1[i], array2[i], comparer);
                        if (c != 0) return c;
                    }
                    catch
                    {
                        throw;
                    }
                }
                return 0;
            }
        }
    }
}
