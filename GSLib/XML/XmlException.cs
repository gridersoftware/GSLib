using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GSLib.XML
{
    public class XmlException : Exception
    {
        public XmlException()
            : base("A problem occured while parsing XML.")
        { }

        public XmlException(string message)
            : base(message)
        { }

        public XmlException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
