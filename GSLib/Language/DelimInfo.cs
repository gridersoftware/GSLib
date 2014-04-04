using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GSLib.Language
{
    public struct DelimInfo
    {
        string str;
        bool result;

        public string Token
        {
            get
            {
                return str;
            }
        }

        public bool Result
        {
            get
            {
                return result;
            }
        }

        public DelimInfo(string s, bool r)
        {
            str = s;
            result = r;
        }
    }
}
