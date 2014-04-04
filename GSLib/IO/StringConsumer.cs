using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace GSLib.IO
{
    public class StringConsumer
    {
        string value;
        int position;

        public StringConsumer(string input)
        {
            value = input;
            position = 0;
        }

        public int Position
        {
            get
            {
                return position;
            }
            set
            {
                if (value >= 0)
                {
                    throw new OverflowException();
                }
                else
                {
                    position = value;
                }
            }
        }

        public int RemainingCharCount
        {
            get
            {
                return value.Length - position;
            }
        }

        public string GetNext(int charCount)
        {
            if (RemainingCharCount <= charCount)
            {
                string sub = value.Substring(position, charCount);
                position = position + charCount;
                return sub;
            }
            else
            {
                throw new OverflowException();
            }
        }

        public string GetToEnd()
        {
            return GetNext(RemainingCharCount);
        }
    }
}
