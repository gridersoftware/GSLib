using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GSLib.Collections
{
    /// <summary>
    /// Represents a collection of keys and values that is sortable by either key or value.
    /// </summary>
    /// <typeparam name="TKey">Key type</typeparam>
    /// <typeparam name="TValue">Value type</typeparam>
    /// Please note: there is another file associated with this class.
    /// You can find additional functions in Sorter.cs.
    /// Also, do not document this class with the "partial" keyword in the public docs.
    public partial class SortableDictionary<TKey, TValue> : IDictionary<TKey, TValue>
    {
        /***********************************************************************
         * Private Fields
         **********************************************************************/ 
        LinkedList<KeyValuePair<TKey, TValue>> list;    // stores the items of the dictionary
        bool sortByKey;     // determines whether or not to sort by key
        bool orderAscending;    // determines whether to sort by ascending or not
        bool readOnly;      // determines if this class is read-only or not

        IComparer<TKey> keyComparer;        // comparer for keys
        IComparer<TValue> valueComparer;    // comparer for values

        /*********************************************************************************************************
        /* Properties
        /********************************************************************************************************/
        /// <summary>
        /// Gets the number of elements contained in the SortableDictionary.
        /// </summary>
        /// VB: Public ReadOnly Property Count As Integer
        public int Count
        {
            get
            {
                return Keys.Length;
            }
        }

        /// <summary>
        /// Gets or sets a value that determines if the object can be changed.
        /// </summary>
        /// VB: Public Property Count As Boolean
        public bool IsReadOnly
        {
            get
            {
                return readOnly;
            }
            set
            {
                readOnly = value;
            }
        }

        /// <summary>
        /// Gets all of the keys in the SortableDictionary.
        /// </summary>
        /// VB: Public ReadOnly Property Keys As TKeys()
        public TKey[] Keys
        {
            get
            {
                List<TKey> keys = new List<TKey>();
                foreach (KeyValuePair<TKey, TValue> kvp in this)
                {
                    keys.Add(kvp.Key);
                }
                return keys.ToArray();
            }
        }

        /// <summary>
        /// Gets all of the values in the SortableDictionary.
        /// </summary>
        /// VB: Public ReadOnly Property Values As TValue()
        public TValue[] Values
        {
            get
            {
                List<TValue> values = new List<TValue>();
                foreach (KeyValuePair<TKey, TValue> kvp in this)
                {
                    values.Add(kvp.Value);
                }
                return values.ToArray();
            }
        }

        /// <summary>
        /// Gets the values of the dictionary as an ICollection.
        /// </summary>
        ICollection<TValue> IDictionary<TKey, TValue>.Values
        {
            get
            {
                ICollection<TValue> coll = Values;
                return coll;
            }
        }

        /// <summary>
        /// Gets the keys as an ICollection.
        /// </summary>
        ICollection<TKey> IDictionary<TKey, TValue>.Keys
        {
            get
            {
                ICollection<TKey> coll = Keys;
                return coll;
            }
        }

        /// <summary>
        /// Gets or sets element with the specified key.
        /// </summary>
        /// <param name="key">Key to set or get</param>
        /// <exception cref="KeyNotFoundException">Thrown when the given key does not exist in the dictionary.</exception>
        /// <exception cref="NullReferenceException">Thrown when the given key is null.</exception>
        /// VB: Default Public Property Item(ByVal key As TKey)
        public TValue this[TKey key]
        {
            get
            {
                if (key != null)
                {
                    LinkedListNode<KeyValuePair<TKey, TValue>> item = list.First;
                    while (item != null)
                    {
                        if (Helpers.Compare<TKey>(item.Value.Key, key, keyComparer) == 0)
                            return item.Value.Value;
                        else
                            item = item.Next;
                    }
                    throw new KeyNotFoundException();
                }
                else
                {
                    throw new NullReferenceException();
                }
            }

            set
            {
                if (key != null)
                {
                    LinkedListNode<KeyValuePair<TKey, TValue>> item = list.First;
                    while (item != null)
                    {
                        if (Helpers.Compare<TKey>(item.Value.Key, key, keyComparer) == 0)
                        {
                            item.Value = new KeyValuePair<TKey, TValue>(item.Value.Key, value);
                            SlowSort();
                        }
                        else
                        {
                            item = item.Next;
                        }
                    }
                    throw new KeyNotFoundException();
                }
                else
                {
                    throw new NullReferenceException();
                }
            }
        }

        /// <summary>
        /// Gets or sets the value determining whether or not to sort by key. If set to false, the SortableDictionary will sort by value instead.
        /// </summary>
        /// Public Property SortByKey As Boolean
        public bool SortByKey
        {
            get
            {
                return sortByKey;
            }
            set
            {
                sortByKey = value;
                SlowSort();
            }
        }

        /// <summary>
        /// Gets or sets the value determining whether or not to sort by value. If set to false, the SortableDictionary will sort by key instead.
        /// </summary>
        /// VB: Public Property SortByValue As Boolean
        public bool SortByValue
        {
            get
            {
                return !sortByKey;
            } 
            set
            {
                SortByKey = !value;
            }
        }

        /// <summary>
        /// Gets or sets the value determining whether to sort in ascending or decending order.
        /// </summary>
        /// VB: Public Property OrderAscending As Boolean
        public bool OrderAscending
        {
            get
            {
                return orderAscending;
            }
            set
            {
                orderAscending = value;
                SlowSort();
            }
        }

        /***********************************************************************
         * Constructors
         **********************************************************************/
        /// <summary>
        /// Default constructor. Instantiates a SortableDictionary object.
        /// </summary>
        /// <remarks>If TKey or TValue are not IComparable, you should use the constructor that provides IComparer objects.</remarks>
        /// <exception cref="TypeInitializationException">Thrown if TKey or TValue do not implement IComparable.</exception>
        /// VB: Public Sub New()
        public SortableDictionary()
        {
            list = new LinkedList<KeyValuePair<TKey, TValue>>();
            //bool a = Helpers.IsComparable<TKey>();
            //bool b = Helpers.IsComparable<TValue>();
            bool comparable = ((Helpers.IsComparable<TKey>()) & (Helpers.IsComparable<TValue>()));
            Sort(true, true);
            if (!comparable)
            {
                throw new TypeInitializationException(typeof(SortableDictionary<TKey, TValue>).FullName, new Exception("TKey and TValue must be IComparable."));
            }
        }

        /// <summary>
        /// Constructor for objects that are not IComparable. Instantiates a SortableDictionary object.
        /// </summary>
        /// <param name="tKeyComparer">IComparer for TKey.</param>
        /// <param name="tValueComparer">IComparer for TValue.</param>
        /// <remarks>If TKey and TValue implement IComparable, you can use the default constructor. You can check this by using GScript.Collections.Helpers.IsComparable</remarks>
        /// VB: Public Sub New(ByVal tKeyComparer As IComparer(Of TKey), ByVal tValueComparer As IComparer(Of TValue))
        public SortableDictionary(IComparer<TKey> tKeyComparer, IComparer<TValue> tValueComparer)
        {
            keyComparer = tKeyComparer;
            valueComparer = tValueComparer;
            list = new LinkedList<KeyValuePair<TKey, TValue>>();
            Sort(true, true);
        }

        /***********************************************************************
         * Methods
         **********************************************************************/ 
        /// <summary>
        /// Retrieves all of the KeyValuePair objects stored in the SortableDictionary object.
        /// </summary>
        /// <returns>Returns an IEnumerator of the KeyValuePair objects.</returns>
        /// VB: Public Function GetEnumerator() As IEnumerator(Of KeyValuePair(Of TKey, TValue))
        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            LinkedListNode<KeyValuePair<TKey, TValue>> node = list.First;
            while (node != null)
            {
                yield return node.Value;
                node = node.Next;
            }
        }

        /// <summary>
        /// Non-Generic IEnumerator.
        /// </summary>
        /// <returns></returns>
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <summary>
        /// Adds the given item to the dictionary, and sorts orders it appropriately.
        /// </summary>
        /// <param name="item">Item to add</param>
        /// VB: Public Sub Add(ByVal item As KeyValuePair(Of TKey, TValue))
        public void Add(KeyValuePair<TKey, TValue> item)
        {
            Add(item.Key, item.Value);
        }

        /// <summary>
        /// Adds items in order as they are entered.
        /// </summary>
        /// <param name="key">Key to use.</param>
        /// <param name="value">Value to use.</param>
        /// <exception cref="ArgumentException">Thrown when the key already exists.</exception>
        /// <exception cref="ArgumentNullException">Thrown when the key is null.</exception>
        /// <exception cref="InvalidOperationException">Thrown when the object is set to read-only.</exception>
        /// VB: Public Sub Add(ByVal key As TKey, ByVal value As TValue)
        public void Add(TKey key, TValue value)
        {
            LinkedListNode<KeyValuePair<TKey, TValue>> item = list.First;
            KeyValuePair<TKey, TValue> newItem = new KeyValuePair<TKey, TValue>(key, value);

            if (ContainsKey(key))
            {
                throw new ArgumentException("Key already exists in SortableDictionary.");
            }

            if (((object)key) == null)
            {
                throw new ArgumentNullException("Key cannot be null.");
            }

            if (IsReadOnly)
            {
                throw new InvalidOperationException("Cannot add or remove items when set to read only.");
            }

            AddItem(key, value);
        }

        /// <summary>
        /// Determines if the SortableDictionary contains the given key.
        /// </summary>
        /// <param name="key">Key to search for.</param>
        /// <returns>Returns true if the key exists, otherwise returns false.</returns>
        /// VB: Public Function ContainsKey(ByVal key As TKey) As Boolean
        public bool ContainsKey(TKey key)
        {
            LinkedListNode<KeyValuePair<TKey, TValue>> item = list.First;
            while (item != null)
            {
                if (Helpers.Compare<TKey>(item.Value.Key, key, keyComparer) == 0)
                    return true;
                else
                    item = item.Next;
            }
            return false;
        }

        /// <summary>
        /// Determines if the SortableDictionary contains the given value.
        /// </summary>
        /// <param name="value">Value to search for.</param>
        /// <returns>Returns true if the value exists, otherwise returns false.</returns>
        /// VB: Public Function ContainsValue(ByVal value As TValue) As Boolean
        public bool ContainsValue(TValue value)
        {
            LinkedListNode<KeyValuePair<TKey, TValue>> item = list.First;
            while (item != null)
            {
                if (Helpers.Compare<TValue>(item.Value.Value, value, valueComparer) == 0)
                    return true;
                else
                    item = item.Next;
            }
            return false;
        }

        /// <summary>
        /// Clears the dictionary of all items.
        /// </summary>
        /// VB: Public Sub Clear()
        public void Clear()
        {
            list.Clear();
        }
        
        /// <summary>
        /// Determines if the given item exists.
        /// </summary>
        /// <param name="item">Item to search for.</param>
        /// <returns>Returns true if the item exists, otherwise returns false.</returns>
        /// VB: Public Function Contains(ByVal item As KeyValuePair(Of TKey, TValue)) As Boolean
        public bool Contains(KeyValuePair<TKey, TValue> item)
        {
            if (ContainsKey(item.Key))
            {
                return (this[item.Key].Equals(item.Value));
            }
            return false;
        }

        /// <summary>
        /// Copies items to the given array, starting at the given index.
        /// </summary>
        /// <param name="array">Array to copy the values to</param>
        /// <param name="arrayIndex">Index to start copying to</param>
        /// <exception cref="ArgumentNullException">Thrown if array is null.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if index is less than zero.</exception>
        /// <exception cref="ArgumentException">Thrown if the number of Value elements is greater than the space available from the index to the end of the array.</exception>
        /// VB: Public Sub CopyTo(ByVal array() As KeyValuePair(Of TKey, TValue), ByVal arrayIndex As Integer)
        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            int j = 0;

            if (array == null)
            {
                throw new ArgumentNullException("Array cannot be null.");
            }
            if (arrayIndex < 0)
            {
                throw new ArgumentOutOfRangeException("Index is less than zero.");
            }
            if (array.Length - arrayIndex - 1 < Values.Length)
            {
                throw new ArgumentException("The number of Value elements is greater than the space available between index and the end of the array.");
            }

            for (int i = arrayIndex; i < array.Length; i++)
            {
                array[i] = new KeyValuePair<TKey, TValue>(Keys[j], Values[j]);
            }
        }

        //void ICollection.CopyTo(TValue[] array, int index)
        //{
        //    CopyTo(array, index);
        //}

        /// <summary>
        /// Removes the given item.
        /// </summary>
        /// <param name="item">Item to remove.</param>
        /// <returns>Returns true if the item was found (and thereby removed). Otherwise returns false.</returns>
        /// VB: Public Function Remove(ByVal item As KeyValuePair(Of TKey, TValue) As Boolean
        public bool Remove(KeyValuePair<TKey, TValue> item)
        {
            return Remove(item.Key);
        }

        /// <summary>
        /// Searches the LinkedList for an item with the given key.
        /// </summary>
        /// <param name="key">Key to search for.</param>
        /// <returns>If an item with the given key is found, returns the LinkedListNode containing that item. Otherwise, returns null.</returns>
        /// <exception cref="ArgumentNullException">Thrown if the given key was null</exception>
        private LinkedListNode<KeyValuePair<TKey, TValue>> IterateList(TKey key)
        {
            if (ObjectIsNull(key)) throw new ArgumentNullException("Key cannot be null.");
            LinkedListNode<KeyValuePair<TKey, TValue>> current = list.First;
            while (current != null)
            {
                if (current.Value.Key.Equals(key))
                {
                    return current;
                }
                current = current.Next;
            }
            return null;
        }

        /// <summary>
        /// Removes the item with the given key.
        /// </summary>
        /// <param name="key">Key to search for.</param>
        /// <returns>If the item with the given key was found (and removed), returns true. Otherwise, returns false.</returns>
        /// <exception cref="ArgumentNullException">Thrown if the given key is empty.</exception>
        /// <exception cref="InvalidOperationException">Thrown if the object is set to read-only.</exception>
        /// VB: Public Function Remove(ByVal key As TKey) As Boolean
        public bool Remove(TKey key)
        {
            if (key == null)
            {
                throw new ArgumentNullException();
            }

            if (IsReadOnly)
            {
                throw new InvalidOperationException();
            }

            LinkedListNode<KeyValuePair<TKey, TValue>> node = IterateList(key); // search the list for the given item
            if (node != null)   // if the item was found
            {
                list.Remove(node);  // remove it
                return true;
            }
            return false;
        }

        /// <summary>
        /// If the given key exists, assigns the value associated with that key to value.
        /// </summary>
        /// <param name="key">Key to search for.</param>
        /// <param name="value">Variable to store the resulting value in.</param>
        /// <returns>If the given key exists, returns true, and sets value to the keys associated value. Otherwise, returns false and sets value to the default of TValue.</returns>
        /// <exception cref="ArgumentNullException">Thrown if key is null.</exception>
        /// VB: Public Function TryGetValue(ByVal key As TKey, ByRef value As TValue) As Boolean
        public bool TryGetValue(TKey key, out TValue value)
        {
            if (key == null)
            {
                throw new ArgumentNullException();
            }

            LinkedListNode<KeyValuePair<TKey, TValue>> node = IterateList(key);
            if (node != null)
            {
                value = node.Value.Value;
                return true;
            }
            else
            {
                value = default(TValue);
                return false;
            }
        }

        /// <summary>
        /// Determines if an object is null or not.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        private bool ObjectIsNull(object obj)
        {
            return (obj == null);
        }
    }
}
