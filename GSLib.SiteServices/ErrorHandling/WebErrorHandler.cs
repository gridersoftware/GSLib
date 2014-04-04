using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net;
using System.Data.Common;
using System.Web;
using GSLib.Database;
using GSLib.SiteServices.Email;

namespace GSLib.SiteServices.ErrorHandling
{
    public abstract class WebErrorHandler<TWrapper, TParameter> : GSLib.ErrorHandling.ErrorHandler
        where TWrapper : IDbWrapper
        where TParameter : DbParameter
    {
        protected TWrapper wrapper;   

        public WebErrorHandler()
        {
            wrapper = default(TWrapper);
        }

        public WebErrorHandler(TWrapper wrapper)
        {
            this.wrapper = wrapper;
        }

        public abstract void CatchError(Exception ex, string tableName);

        public void CatchError(Exception ex, string tableName, ref Email.Email mailHandler, string emailAddress, System.Net.NetworkCredential credentials)
        {
            try
            {
                CatchError(ex, tableName);
                //mailHandler.SendEmail(emailAddress, emailAddress, "Web Error", ex.Message + "\n" + ex.InnerException + "\n\n" + ex.StackTrace, credentials);
            }
            catch
            {
                throw;
            }
        }

        public void CatchError(Exception ex, string tableName, string errorURL)
        {
            try
            {
                CatchError(ex, tableName);
                HttpContext.Current.Response.Redirect(errorURL);
            }
            catch
            {
                throw;
            }
        }

        public void CatchError(Exception ex, string tableName, string errorURL, ref WebMail mailHandler, string emailAddress, System.Net.NetworkCredential credentials, bool useOldMail = false)
        {
            try
            {
                //CatchError(ex, tableName, ref mailHandler, emailAddress, credentials, useOldMail);
                HttpContext.Current.Response.Redirect(errorURL);
            }
            catch
            {
                throw;
            }
        }

        public new void GenerateLog(string path)
        {
            try
            {
                File.WriteAllLines(path, notes);
            }
            catch
            {
                throw;
            }
        }
    }
}
