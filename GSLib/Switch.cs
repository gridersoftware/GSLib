using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GSLib
{
    /// <summary>
    /// Represents an object-oriented version of a switch statement.
    /// </summary>
    /// <typeparam name="T">Type of value to compare to.</typeparam>
    /// <remarks>Switch class has no public constructor. Use the static Create() function instead.</remarks>
    public class Switch<T>
    {
        Dictionary<T, Action> cases;    // list of cases
        Action defaultCase;             // default case, if any
        T value;                        // value to compare to

        /// <summary>
        /// Sets the default action.
        /// </summary>
        public Action Default
        {
            set
            {
                defaultCase = value;
            }
        }

        /// <summary>
        /// Private constructor.
        /// </summary>
        private Switch()
        {
            defaultCase = null;
            cases = new Dictionary<T, Action>();
        }

        /// <summary>
        /// Creates and returns a new Switch object with the given value.
        /// </summary>
        /// <param name="srcValue">Value to compare against.</param>
        /// <returns>Returns a new Switch object.</returns>
        public static Switch<T> Create(T srcValue)
        {
            Switch<T> s = new Switch<T>();
            s.value = srcValue;
            return s;
        }

        /// <summary>
        /// Adds a case to evaluate when the given value is equal to the Switch value.
        /// </summary>
        /// <param name="value">Value to compare against the Switch value.</param>
        /// <param name="action">Action to carry out.</param>
        public void AddCase(T value, Action action)
        {
            cases.Add(value, action);
        }

        /// <summary>
        /// Executes the switch.
        /// </summary>
        public void Execute()
        {
            if (cases.ContainsKey(value))
            {
                cases[value].Invoke();
            }
            else if (defaultCase != null)
            {
                defaultCase.Invoke();
            }
            else
            {
                throw new Exception("Default case required.");
            }
        }
    }
}
