using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GSLib.Language
{
    public class DelimitedStringTokenizer
    {
        public static string BuildDelimetedList(string[] list, char delimeter)
        {
            StringBuilder str = new StringBuilder();
            for (int i = 0; i < list.Length; i++)
            {
                str.Append(list[i]);
                if (i < list.Length - 1) str.Append(delimeter);
            }
            return str.ToString();
        }

        

        

        /// <summary>
        /// 
        /// </summary>
        /// <param name="str"></param>
        /// <param name="delimiter"></param>
        /// <param name="breakChars"></param>
        /// <param name="excludeDelimeter"></param>
        /// <param name="throwExOnMismatch"></param>
        /// <returns></returns>
        public static DelimInfoCollection TokenizeDelimiter(string str, char delimiter, char[] breakChars, bool excludeDelimeter = true, bool throwExOnMismatch = false)
        {
            try
            {
                return TokenizeDelimiter(str, delimiter, delimiter, breakChars, excludeDelimeter, throwExOnMismatch);
            }
            catch
            {
                throw;
            }
        }

        public static DelimInfoCollection TokenizeDelimiter(string str, char openDelimiter, char closeDelimiter, char[] breakChars, bool excludeDelimeter = true, bool throwExOnMismatch = false)
        {
            //Dictionary<string, bool> result = new Dictionary<string, bool>();
            DelimInfoCollection result = new DelimInfoCollection();
            StringBuilder keyword = new StringBuilder();

            bool inDelim = false;
            foreach (char c in str)
            {
                if (!inDelim)
                {
                    if (c == openDelimiter)
                    {
                        if (!excludeDelimeter) keyword.Append(c);
                        inDelim = true;
                        if (keyword.Length > 0)
                        {
                            result.Add(keyword.ToString(), true);
                            keyword.Clear();
                        }
                    }
                    else if (breakChars.Contains(c))
                    {
                        result.Add(keyword.ToString(), true);
                        keyword.Clear();
                    }
                    else
                    {
                        keyword.Append(c);
                    }
                }
                else
                {
                    if (c == closeDelimiter)
                    {
                        if (!excludeDelimeter) keyword.Append(c);
                        inDelim = false;
                        result.Add(keyword.ToString(), false);
                        keyword.Clear();
                    }
                    else
                    {
                        keyword.Append(c);
                    }
                }
            }

            if (inDelim)
            {
                if (throwExOnMismatch) throw new DelimiterMismatchException();
                result.Add(keyword.ToString(), true);
            }

            return result;
        }

        public static DelimInfoCollection TokenizeNonDelimiter(string str, char delimiter, char[] breakChars, bool excludeDelimeter = true, bool throwExOnMismatch = false)
        {
            try
            {
                return TokenizeNonDelimiter(str, delimiter, delimiter, breakChars, excludeDelimeter, throwExOnMismatch);
            }
            catch
            {
                throw;
            }
        }

        public static DelimInfoCollection TokenizeNonDelimiter(string str, char openDelimiter, char closeDelimeter, char[] breakChars, bool excludeDelimeter = true, bool throwExOnMismatch = false)
        {
            try
            {
                DelimInfoCollection src = TokenizeDelimiter(str, openDelimiter, closeDelimeter, breakChars);
                DelimInfoCollection result = new DelimInfoCollection();

                foreach (DelimInfo kvp in src.Items)
                {
                    result.Add(kvp.Token, !kvp.Result);
                }
                return result;
            }
            catch
            {
                throw;
            }
        }


    }
}
