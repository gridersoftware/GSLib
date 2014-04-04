using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GSLib.Collections
{
    // Please note: the main file for this class is SortableDictionary.cs.
    // Also, do not document this class with the "partial" keyword in the public docs.
    public partial class SortableDictionary<TKey, TValue> : IDictionary<TKey, TValue>
    {
        /// <summary>
        /// Sorts the items in the SortableDictionary by the given specification.
        /// </summary>
        /// <param name="byKey">Indicates whether to sort by key or by value.</param>
        /// <param name="sortAscending">Indicates whether to order ascending, or descending.</param>
        /// VB: Public Sub Sort(ByVal byKey As Boolean, ByVal ascending As Boolean)
        public void Sort(bool byKey, bool sortAscending)
        {
            sortByKey = byKey;
            orderAscending = sortAscending;

            SlowSort();
        }

        /// <summary>
        /// Sorts by making a copy of the underlying LinkedList, clear the LinkedList, then re-add all items.
        /// </summary>
        private void SlowSort()
        {
            // make a copy of the current list
            LinkedList<KeyValuePair<TKey, TValue>> copy = Helpers.DeepCloneLinkedList<KeyValuePair<TKey, TValue>>(list);

            list.Clear(); // clear the list
            LinkedListNode<KeyValuePair<TKey, TValue>> current = copy.First;
            while (current != null)
            {
                AddItem(current.Value.Key, current.Value.Value);
                current = current.Next;
            }
        }

        private void AddItem(TKey key, TValue value)
        {
            LinkedListNode<KeyValuePair<TKey, TValue>> current = list.First;
            KeyValuePair<TKey, TValue> item = new KeyValuePair<TKey, TValue>(key, value);
            LinkedListNode<KeyValuePair<TKey,TValue>> node = new LinkedListNode<KeyValuePair<TKey,TValue>>(item);

            if (list.Count == 0)
            {
                list.AddFirst(node);
            }

            while (current != null)
            {
                int result;
                if (SortByKey)
                    result = Helpers.Compare<TKey>(key, current.Value.Key, keyComparer);
                else
                    result = Helpers.Compare<TValue>(value, current.Value.Value, valueComparer);

                if (!orderAscending) result = result * -1;
                if (result < 0)
                {
                    list.AddBefore(current, node);
                    current = null;
                }
                else
                {
                    if (list.Count == 1)
                    {
                        list.AddAfter(current, node);
                        current = null;
                    }
                    else
                    {
                        if (current.Next == null)
                        {
                            list.AddLast(node);
                            current = null;
                        }
                        else
                        {
                            current = current.Next;
                        }
                    }
                }
            }
        }
    }
}
