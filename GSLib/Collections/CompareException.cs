using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GSLib.Collections
{
    /// <summary>
    /// Thrown when a comparison of objects cannot occur.
    /// </summary>
    /// VB: Public Class CompareException
    ///         Inherits Exception
    public class CompareException : Exception 
    {
        /// <summary>
        /// Initializes a new instance of the CompareException class.
        /// </summary>
        /// VB: Public Sub New()
        public CompareException()
            : base("Cannot compare values.")
        {
        }

        /// <summary>
        /// Initializes a new instance of the CompareException class with the specified message.
        /// </summary>
        /// <param name="message">Message that describes the error.</param>
        /// VB: Public Sub New(ByVal message As String)
        public CompareException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the CompareException class with the specified message and inner exception.
        /// </summary>
        /// <param name="message">Message that describes the error.</param>
        /// <param name="innerException">Exception that is the cause of the error.</param>
        /// VB: Public Sub New(ByVal message As String, ByVal innerException As Exception)
        public CompareException(string message, Exception innerException) 
            : base(message, innerException)
        {
        }
    }
}
