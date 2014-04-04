using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GSLib.ErrorHandling
{
    public class ErrorHandlerEventArgs : EventArgs
    {
        Exception e;

        public Exception ex
        {
            get
            {
                return e;
            }
        }

        public ErrorHandlerEventArgs(Exception ex) : base()
        {
            if (ex == null) throw new ArgumentNullException();
            e = ex;
        }
    }
}
