using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using GSLib.Collections;

namespace GSLib.Setup
{
    public class ProductKeyComparer : IComparer<ProductKey>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public int Compare(ProductKey x, ProductKey y)
        {
            if (x.IsPattern == y.IsPattern) return Helpers.CompareArrays<string>(x.productKey, y.productKey);

            ProductKey pattern;
            ProductKey key;

            if (x.IsPattern)
            {
                pattern = x;
                key = y;
            }
            else
            {
                pattern = y;
                key = x;
            }

            int diff = x.Count - y.Count;
            if (diff != 0) return diff;

            for (int i = 0; i < pattern.Count; i++)
            {
                diff = diff + CompareElements(pattern[i], key[i]);
            }

            return diff;
        }

        /// <summary>
        /// Compares elements using the default key pattern system.
        /// </summary>
        /// <param name="pattern">Pattern element to check against.</param>
        /// <param name="key">Product key element to check.</param>
        /// <remarks>The standard product key pattern consists of #, $, and *. '#' represents a digit. 
        /// represents an alphabetic character. * represents an alphanumeric wildcard. Any other 
        /// alphanumeric character is interpreted as a literal. The function in its standard form
        /// only accepts alphanumeric characters as part of the key. Non-alphanumeric characters break
        /// the pattern automatically.</remarks>
        /// <exception cref="ArgumentException">Thrown if the pattern element breaks the rule of alphanumeric literals.</exception>
        /// <returns>Returns zero of the key element matches the pattern element. Otherwise, returns non-zero.</returns>
        protected virtual int CompareElements(string pattern, string key)
        {
            int diff = pattern.Length - key.Length;
            if (diff != 0) return diff;

            for (int i = 0; i < pattern.Length; i++)
            {
                char c = key[i];
                char p = pattern[i];
                if (!char.IsLetterOrDigit(c)) diff++;
                if ((!char.IsLetterOrDigit(p)) & (p != '#') & (p != '$') & (p != '*')) throw new ArgumentException();
                switch (p)
                {
                    case '#':
                        if (!char.IsDigit(c)) diff++;
                        break;

                    case '$':
                        if (char.IsDigit(c)) diff++;
                        break;

                    case '*':
                        break;

                    default:
                        if (c != p) diff++;
                        break;
                }
            }

            return diff;
        }
    }
}
