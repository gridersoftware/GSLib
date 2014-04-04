using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GSLib.Mathematics
{
    public class Constant<T> : IVariable
    {
        public string Name { get; private set; }
        public bool IsConstant { get; private set; }
        public T Value { get; private set; }

        public Constant(string name, T value)
        {
            Name = name;
            Value = value;
            IsConstant = true;
        }
    }

    public static class ConstantExtensions
    {
        public static Constant<T> GetByName<T>(this Sets.Set<Constant<T>> set, string name)
        {
            if (name == null) throw new ArgumentNullException();
            if (name == "") throw new ArgumentException();

            foreach (Constant<T> item in set)
            {
                if (item.Name == name) return item;
            }
            return null;
        }

        public static Constant<T> GetByName<T>(this Tuples.NTuple<Constant<T>> tuple, string name)
        {
            if (name == null) throw new ArgumentNullException();
            if (name == "") throw new ArgumentException();

            foreach (Constant<T> item in tuple)
            {
                if (item.Name == name) return item;
            }
            return null;
        }
    }
}
