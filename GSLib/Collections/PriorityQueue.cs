using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace GSLib.Collections
{
    /// <summary>
    /// Represents a priority queue, which orders things depending on a given weight.
    /// </summary>
    /// <typeparam name="TValue">Value type.</typeparam>
    /// <typeparam name="TWeight">Weight type. The weight type must be an IComparable(Of TWeight).</typeparam>
    /// <remarks>Note: The PriorityQueue class is not an ICollection like the System.Collections.Generic.Queue(of T) class is.
    /// Therefore, PriorityQueue objects cannot be passed as ICollection objects.</remarks>
    public class PriorityQueue<TValue, TWeight> : IEnumerable<TValue>, IEnumerable 
        where TWeight : IComparable<TWeight>
    {
        struct QueueItem
        {
            public TValue value;
            public TWeight weight;
        }

        LinkedList<QueueItem> items;

        bool priorityDescending;

        public PriorityQueue(bool priorityDescending = true)
        {
            this.priorityDescending = priorityDescending;
            items = new LinkedList<QueueItem>();
        }

        public void Enqueue(TValue itemValue, TWeight itemWeight)
        {
            QueueItem item = new QueueItem() { value = itemValue, weight = itemWeight };
            if (items.Count == 0)
            {
                items.AddFirst(item);
            }
            else
            {
                LinkedListNode<QueueItem> current = items.First;
                if (priorityDescending)
                {
                    while ((current != null) && (itemWeight.CompareTo(current.Value.weight) < 0))
                    {
                        current = current.Next;
                    }
                }
                else
                {
                    while ((current != null) && (itemWeight.CompareTo(current.Value.weight) > 0))
                    {
                        current = current.Next;
                    }
                }

                if (current == null)
                    items.AddLast(item);
                else
                    items.AddBefore(current, item);
            }
        }

        public bool Contains(TValue value)
        {
            return (GetNodeByValue(value) != null);
        }

        public TWeight this[TValue val]
        {
            get
            {
                if (val == null) throw new ArgumentNullException();
                LinkedListNode<QueueItem> item = GetNodeByValue(val);
                if (item != null) return item.Value.weight;
                else return default(TWeight);
            }
            set
            {
                if (val == null) throw new ArgumentNullException();
                LinkedListNode<QueueItem> item = GetNodeByValue(val);
                if (item != null)
                {
                    Enqueue(item.Value.value, value);
                    items.Remove(item);
                }
                else
                {
                    throw new KeyNotFoundException("The value does not exist in the PriorityQueue.");
                }
            }
        }

        private LinkedListNode<QueueItem> GetNodeByValue(TValue value)
        {
            LinkedListNode<QueueItem> current = items.First;
            while ((current != null) && (!current.Value.value.Equals(value)))
            {
                current = current.Next;
            }
            return current;
        }

        public TValue Dequeue(out TWeight weight)
        {
            if (items.Count > 0)
            {
                QueueItem item = items.First.Value;
                items.RemoveFirst();
                weight = item.weight;
                return item.value;
            }
            else
            {
                throw new InvalidOperationException("The queue is empty.");
            }
        }

        public TValue Dequeue()
        {
            if (items.Count > 0)
            {
                TWeight temp;
                return Dequeue(out temp);
            }
            else
            {
                throw new InvalidOperationException("The queue is empty.");
            }
        }

        public TValue Peek()
        {
            if (items.Count > 0)
            {
                TWeight temp;
                return Peek(out temp);
            }
            else
            {
                throw new InvalidOperationException("The queue is empty.");
            }
        }

        public TValue Peek(out TWeight weight)
        {
            if (items.Count > 0)
            {
                QueueItem item = items.First.Value;
                weight = item.weight;
                return item.value;
            }
            else
            {
                throw new InvalidOperationException("The queue is empty.");
            }
        }

        public int Count
        {
            get
            {
                return items.Count;
            }
        }

        public IEnumerator<TValue> GetEnumerator()
        {
            LinkedListNode<QueueItem> current = items.First;
            while (current.Next != null)
            {
                yield return current.Value.value;
                current = current.Next;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
