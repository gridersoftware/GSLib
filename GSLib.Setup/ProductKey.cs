using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GSLib.Setup
{
    /// <summary>
    /// Represents a product key or product key pattern.
    /// </summary>
    public class ProductKey
    {
        internal string[] productKey;
        bool caseSensitive;
        
        /// <summary>
        /// Gets or sets a value determining if the product key is a pattern.
        /// </summary>
        public bool IsPattern { get; set; }

        /// <summary>
        /// Gets a value determining the number of blocks in the product key.
        /// </summary>
        public int Count
        {
            get
            {
                return productKey.Length;
            }
        }

        /// <summary>
        /// Gets the block located at the given index.
        /// </summary>
        /// <param name="index">Index of block.</param>
        /// <returns>Returns the requested block.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if index is less than 0 or greater or equal to Count.</exception>
        public string this[int index]
        {
            get
            {
                if ((index >= productKey.Length) | (index < 0)) throw new ArgumentOutOfRangeException();
                return productKey[index];
            }
        }

        /// <summary>
        /// Gets a value determining if the product key is case-sensitive.
        /// </summary>
        public bool CaseSensitive
        {
            get
            {
                return caseSensitive;
            }
        }

        /// <summary>
        /// Creates a new instance of ProductKey with the given key elements.
        /// </summary>
        /// <param name="keyElements">Elements of the product key.</param>
        /// <param name="caseSensitive">Determines if the key is case-sensitive.</param>
        /// <exception cref="ArgumentNullException">Thrown if keyElements is null or keyElements contains one or more null elements.</exception>
        /// <exception cref="ArgumentException">Thrown if keyElements contains one or more empty strings.</exception>
        public ProductKey(string[] keyElements, bool caseSensitive = false)
        {
            if (keyElements == null) throw new ArgumentNullException();
            if (keyElements.Contains(null)) new ArgumentNullException();
            if (keyElements.Contains("")) throw new ArgumentException();
            
            productKey = keyElements;

            this.caseSensitive = caseSensitive;
            if (caseSensitive)
            {
                for (int i = 0; i < Count; i++)
                {
                    productKey[i] = productKey[i].ToUpper();
                }
            }
        }

        /// <summary>
        /// Creates a new instance of ProductKey with the given key.
        /// </summary>
        /// <param name="key">A product key (or key pattern) in which each block or element is separated by a hyphen.</param>
        /// <param name="caseSensitive">Determines if the key or key pattern is case-sensitive.</param>
        public ProductKey(string key, bool caseSensitive = false)
        {
            if (key == null) throw new ArgumentNullException();
            if (key == "") throw new ArgumentException();

            productKey = key.Split('-');
            
            this.caseSensitive = caseSensitive;
            if (caseSensitive)
            {
                for (int i = 0; i < Count; i++)
                {
                    productKey[i] = productKey[i].ToUpper();
                }
            }
        }

        /// <summary>
        /// Compares the ProductKey against the given pattern using the default ProductKeyComparer.
        /// </summary>
        /// <param name="pattern">Pattern to check against.</param>
        /// <returns>Returns true if the key matches the pattern. Otherwise returns false.</returns>
        public bool CheckKey(ProductKey pattern)
        {
            return CheckKey(pattern, new ProductKeyComparer());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pattern"></param>
        /// <param name="validityChecker"></param>
        /// <returns></returns>
        public bool CheckKey(ProductKey pattern, Func<ProductKey, bool> validityChecker)
        {
            return CheckKey(pattern, validityChecker, new ProductKeyComparer());
        }

        public bool CheckKey(ProductKey pattern, ProductKeyComparer comparer)
        {
            return comparer.Compare(this, pattern) == 0;
        }

        public bool CheckKey(ProductKey pattern, Func<ProductKey, bool> validityChecker, ProductKeyComparer comparer)
        {
            return ((comparer.Compare(this, pattern) == 0) && (validityChecker(this)));
        }

        public override string ToString()
        {
            StringBuilder s = new StringBuilder();
            for (int i = 0; i < productKey.Length; i++)
            {
                s.Append(productKey[i]);
                if (i < productKey.Length - 1) s.Append('-');
            }
            return s.ToString();
        }
    }
}
