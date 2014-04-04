using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GSLib.ErrorHandling
{
    public class AutoErrorHandlerEventArgs : ErrorHandlerEventArgs 
    {
        string userData;

        public string UserData
        {
            get
            {
                return userData;
            }
        }

        public AutoErrorHandlerEventArgs(Exception ex, string userData) : base(ex)
        {
            if (userData == null) throw new ArgumentNullException();
            this.userData = userData;
        }
    }
}
