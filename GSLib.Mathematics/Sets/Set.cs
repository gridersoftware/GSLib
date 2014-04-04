using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GSLib.Mathematics.Sets
{
    /// <summary>
    /// Represents a collection of unique objects that can be modified with set operations.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Set<T> : ICollection<T>, IEnumerable<T>, IVariable
    {
        protected List<T> items;

        /// <summary>
        /// Creates an empty set to which items may be added.
        /// </summary>
        public Set()
        {
            items = new List<T>();
            Name = "";
            IsConstant = false;
        }

        public Set(string name, bool isConstant = false)
        {
            items = new List<T>();
            Name = name;
            IsConstant = isConstant;
        }

        public Set(T[] values)
        {
            if (values == null) throw new ArgumentNullException();

            for (int i = 0; i < values.Length - 1; i++)
            {
                for (int j = i + 1; j < values.Length; j++)
                {
                    if (values[i].Equals(values[j])) throw new ArgumentException();
                }
            }
        }

        public Set(string name, T[] values, bool isConstant = false)
        {
            if (values == null) throw new ArgumentNullException();

            for (int i = 0; i < values.Length - 1; i++)
            {
                for (int j = i + 1; j < values.Length; j++)
                {
                    if (values[i].Equals(values[j])) throw new ArgumentException();
                }
            }

            Name = name;
            IsConstant = isConstant;
        }

        public Set(Set<T> set)
        {
            items = new List<T>(set.items);
            Name = "";
            IsConstant = false;
        }

        public Set(string name, Set<T> set, bool isConstant = false)
        {
            items = new List<T>(set.items);
            Name = name;
            IsConstant = isConstant;
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

        /// <summary>
        /// Adds an item to the Set.
        /// </summary>
        /// <param name="item"></param>
        public void Add(T item)
        {
            if (items.Contains(item)) throw new ArgumentException();
            items.Add(item);
        }

        public void Clear()
        {
            items.Clear();
        }

        public bool Contains(T item)
        {
            return items.Contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            items.CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get
            {
                return items.Count;
            }
        }

        public bool IsReadOnly
        {
            get
            {
                return false;
            }
        }

        public bool Remove(T item)
        {
            return items.Remove(item);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return items.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <summary>
        /// Combines another set with this set to create one set with all elements of both.
        /// </summary>
        /// <param name="otherSet"></param>
        /// <returns></returns>
        public Set<T> Union(Set<T> otherSet)
        {
            Set<T> result = new Set<T>(this);

            foreach (T item in otherSet)
            {
                if (!result.Contains(item)) result.Add(item);
            }
            
            return result;
        }

        public static Set<T> Union(Set<T> set1, Set<T> set2)
        {
            return set1.Union(set2);
        }

        public Set<T> Intersection(Set<T> otherSet)
        {
            Set<T> result = new Set<T>();

            foreach (T item in this)
            {
                if (otherSet.Contains(item)) result.Add(item);
            }
            return result;
        }

        public static Set<T> Intersection(Set<T> set1, Set<T> set2)
        {
            return set1.Intersection(set2);
        }

        public Set<T> Compliment(Set<T> otherSet)
        {
            Set<T> result = new Set<T>(this);

            foreach (T item in otherSet)
            {
                if (result.Contains(item)) result.Remove(item);
            }
            return result;
        }

        public static Set<T> Compliment(Set<T> set1, Set<T> set2)
        {
            return set1.Compliment(set2);
        }

        public Set<Tuples.OrderedPair<T>> CartesianProduct(Set<T> otherSet)
        {
            Set<Tuples.OrderedPair<T>> result = new Set<Tuples.OrderedPair<T>>();

            foreach (T a in this)
            {
                foreach (T b in otherSet)
                {
                    result.Add(new Tuples.OrderedPair<T>(a, b));
                }
            }

            return result;
        }

        public static Set<Tuples.OrderedPair<T>> CartesianProduct(Set<T> set1, Set<T> set2)
        {
            return set1.CartesianProduct(set2);
        }

        public override string ToString()
        {
            StringBuilder s = new StringBuilder();
            s.Append("{");

            for (int i = 0; i < items.Count; i++)
            {
                s.Append(items[i].ToString());
                if (i < items.Count - 1) s.Append(", ");
            }
            s.Append("}");

            return s.ToString();
        }

        public string Name
        {
            get;
            private set;
        }

        public bool IsConstant
        {
            get;
            private set;
        }
    }
}
