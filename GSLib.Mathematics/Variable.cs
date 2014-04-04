using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GSLib.Mathematics
{
    public class Variable<T> : IVariable
    {
        public string Name { get; private set; }
        public T Value { get; set; }

        public bool IsConstant { get; private set; }
        public bool IsBound { get; private set; }

        /// <summary>
        /// Creates a new Variable instance.
        /// </summary>
        /// <param name="name">Name of the variable.</param>
        /// <param name="value">Type of variable.</param>
        public Variable(string name, T value)
        {
            if (name == null) throw new ArgumentNullException();
            if (name == "") throw new ArgumentException();

            Name = name;
            Value = value;
            IsConstant = false;
        }
    }

    public static class VariableExtensions
    {
        public static Variable<T> GetByName<T>(this Sets.Set<Variable<T>> set, string name)
        {
            if (name == null) throw new ArgumentNullException();
            if (name == "") throw new ArgumentException();

            foreach (Variable<T> item in set)
            {
                if (item.Name == name) return item;
            }
            return null;
        }

        public static Variable<T> GetByName<T>(this Tuples.NTuple<Variable<T>> tuple, string name)
        {
            if (name == null) throw new ArgumentNullException();
            if (name == "") throw new ArgumentException();

            foreach (Variable<T> item in tuple)
            {
                if (item.Name == name) return item;
            }
            return null;
        }
    }
}
