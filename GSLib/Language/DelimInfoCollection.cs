using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GSLib.Language
{
    public class DelimInfoCollection : IEnumerable<DelimInfo>
    {
        List<DelimInfo> items;

        public DelimInfoCollection()
        {
            items = new List<DelimInfo>();
        }

        internal void Add(string s, bool r)
        {
            items.Add(new DelimInfo(s, r));
        }

        internal void AddCollection(DelimInfoCollection collection)
        {
            items.AddRange(collection.Items);
        }

        public DelimInfo[] Items
        {
            get
            {
                return items.ToArray();
            }
        }

        public DelimInfo this[int index]
        {
            get
            {
                return items[index];
            }
        }

        public int Count
        {
            get
            {
                return items.Count;
            }
        }

        public Dictionary<string, bool> ToDictionary()
        {
            Dictionary<string, bool> result = new Dictionary<string, bool>();
            foreach (DelimInfo d in items)
            {
                result.Add(d.Token, d.Result);
            }
            return result;
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return items.GetEnumerator();
        }

        IEnumerator<DelimInfo> IEnumerable<DelimInfo>.GetEnumerator()
        {
            return items.GetEnumerator();
        }
    }
}
