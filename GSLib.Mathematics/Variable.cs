using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GSLib.Mathematics
{
    public class Variable
    {
        RealNumber value;

        public string Symbol { get; private set; }
        public bool IsDefined { get; private set; }
        public bool IsConstant { get; private set; }
        public RealNumber Value
        {
            get
            {
                return IsDefined ? value : null;
            }
        } 

        /// <summary>
        /// Initializes the variable as undefined
        /// </summary>
        /// <param name="symbol"></param>
        public Variable(string symbol)
        {
            if (Symbol == null)
                throw new ArgumentNullException();
            if (Symbol == "")
                throw new ArgumentException();

            IsDefined = false;
            value = 0;
            Symbol = symbol;
        }

        /// <summary>
        /// Initializes the value with the number provided. If the number provided is null, the value is set as undefined and not constant.
        /// </summary>
        /// <param name="symbol"></param>
        /// <param name="value"></param>
        public Variable(string symbol, RealNumber value, bool isConstant = false)
            : this(symbol)
        {
            if (value == null)
            {
                IsDefined = false;
                this.value = 0;
                IsConstant = false;
            }
            else
            {
                IsDefined = true;
                this.value = value;
                IsConstant = isConstant;
            }
        }
    }
}
