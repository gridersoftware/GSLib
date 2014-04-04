using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GSLib.Mathematics.Common
{
    class Term
    {
        IVariable[] variables;

        public int Coefficient { get; private set; }

        /// <summary>
        /// Creates a new Term with no variables and a coefficient of 1.
        /// </summary>
        /// <remarks>When simplified, result is a double of 1.</remarks>
        public Term()
        {
            variables = new IVariable[0];
            Coefficient = 1;
        }

        /// <summary>
        /// Creates a new Term with the given variables and a coefficent of 1.
        /// </summary>
        /// <param name="variables"></param>
        public Term(IVariable[] variables)
        {
            this.variables = variables;
            Coefficient = 1;
        }

        /// <summary>
        /// Creates a new Term with the given variables and coefficient
        /// </summary>
        /// <param name="variables"></param>
        /// <param name="coefficient"></param>
        public Term(IVariable[] variables, int coefficient)
        {
            this.variables = variables;
            Coefficient = coefficient;
        }
    }
}
