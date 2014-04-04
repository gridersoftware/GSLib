using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GSLib.Mathematics.Tuples
{
    public class NTuple<T> : IEnumerable<T>
    {
        T[] items;

        public int Count 
        {
            get
            {
                return items.Length;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="n"></param>
        public NTuple(int n)
        {
            items = new T[n];
        }

        public T this[int index]
        {
            get
            {
                if ((index < 0) | (index >= Count)) throw new ArgumentOutOfRangeException();
                return items[index];
            }
            set
            {
                if ((index < 0) | (index >= Count)) throw new ArgumentOutOfRangeException();
                items[index] = value;
            }
        }

        public override string ToString()
        {
            StringBuilder s = new StringBuilder();
            s.Append("(");

            for (int i = 0; i < items.Length; i++)
            {
                s.Append(items[i].ToString());
                if (i < items.Length - 1) s.Append(", ");
            }
            s.Append(")");

            return s.ToString();
        }

        public string ToString(char openDelimeter, char closeDelimeter)
        {
            StringBuilder s = new StringBuilder();
            s.Append(openDelimeter);

            for (int i = 0; i < items.Length; i++)
            {
                s.Append(items[i].ToString());
                if (i < items.Length - 1) s.Append(", ");
            }
            s.Append(closeDelimeter);

            return s.ToString();
        }

        public IEnumerator<T> GetEnumerator()
        {
            return items.ToList<T>().GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
