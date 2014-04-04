using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GSLib.Collections
{
    public class Bitfield2 : IList<bool>
    {
        #region Private Fields
        List<bool> bits;
        Endianness endianness;

        #endregion 

        #region Enumerations

        public enum Endianness
        {
            LittleEndian,
            BigEndian
        }

        #endregion

        #region Constructors

        public Bitfield2(Endianness endianness = Endianness.LittleEndian)
        {
            this.endianness = endianness;
            bits = new List<bool>();
        }

        public Bitfield2(int bitCount, Endianness endianness = Endianness.LittleEndian)
            : this(endianness)
        {
            if (bitCount < 0) throw new ArgumentOutOfRangeException();

            for (int i = 0; i < bitCount; i++)
            {
                bits.Add(false);
            }
        }

        public Bitfield2(bool[] bits, Endianness endianness = Endianness.LittleEndian)
            : this(endianness)
        {
            if (bits == null) throw new ArgumentNullException();

            for (int i = 0; i < bits.Length; i++)
            {
                this.bits.Add(bits[i]);
            }
        }

        public Bitfield2(Bitfield2 bitfield, Endianness endianness = Endianness.LittleEndian)
            : this(endianness)
        {
            if (bitfield == null) throw new ArgumentNullException();

            bits.AddRange(bitfield.Bits);
        }

        #endregion

        #region Public Properties

        public int Count
        {
            get { return bits.Count; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }
        
        public bool this[int index]
        {
            get
            {
                if ((index < 0) | (index >= Count)) throw new IndexOutOfRangeException();
                return bits[index];
            }
            set
            {
                if ((index < 0) | (index >= Count)) throw new IndexOutOfRangeException();
                bits[index] = value;
            }
        }

        public bool[] Bits
        {
            get
            {
                return bits.ToArray();
            }
        }

        public Endianness Endian
        {
            get
            {
                return endianness;
            }
        }

        public bool ByteDivisible
        {
            get
            {
                return Count % 8 == 0;
            }
        }

        public int ByteCount
        {
            get
            {
                return Count / 8;
            }
        }

        #endregion 

        #region Public Instance Methods

        public void Pad(int count, bool bit = false)
        {
            Bitfield2 b = new Bitfield2();
            for (int i = 0; i < count; i++)
            {
                b.Add(bit);
            }

            if (Endian == Endianness.LittleEndian)
                Append(b);
            else
                bits.InsertRange(0, b);
        }

        public void PadToByte(bool bit = false)
        {
            if (!ByteDivisible) Pad(Count % 8, bit);
        }

        public int IndexOf(bool item)
        {
            return bits.IndexOf(item);
        }

        public bool Remove(bool item)
        {
            return bits.Remove(item);
        }

        public void Insert(int index, bool item)
        {
            bits.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            bits.RemoveAt(index);
        }

        public void Add(bool item)
        {
            bits.Add(item);
        }

        public void Append(bool[] bitArray)
        {
            if (bitArray == null) throw new ArgumentNullException();

            for (int i = 0; i < bitArray.Length; i++)
            {
                bits.Add(bitArray[i]);
            }
        }

        public void Append(Bitfield2 bitfield)
        {
            if (bitfield.endianness != endianness)
                bitfield.Reverse();

            Append(bitfield.Bits);
        }

        public void Flip(int index)
        {
            if ((index < 0) | (index >= Count)) throw new ArgumentOutOfRangeException();

            this[index] = !this[index];
        }

        public void Reverse()
        {
            if (endianness == Endianness.LittleEndian)
                endianness = Endianness.BigEndian;
            else
                endianness = Endianness.LittleEndian;

            bits.Reverse();
        }

        public void Clear()
        {
            bits.Clear();
        }

        public bool Contains(bool item)
        {
            return bits.Contains(item);
        }

        public void CopyTo(bool[] array, int arrayIndex)
        {
            bits.CopyTo(array, arrayIndex);
        }

        public IEnumerator<bool> GetEnumerator()
        {
            return bits.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion 
    }
}
