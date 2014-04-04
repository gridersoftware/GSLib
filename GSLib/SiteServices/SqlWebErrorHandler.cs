using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GSLib.Database.SQLServer;
using System.Data.SqlClient;
using System.IO;
using System.Web;

namespace GSLib.SiteServices
{
    public class SqlWebErrorHandler : ErrorHandling.GenericErrorHandler 
    {
        SQLWrapper sqlWrapper;

        public SqlWebErrorHandler() 
        {
            sqlWrapper = null;
        }

        public SqlWebErrorHandler(SQLWrapper wrapper)
        {
            sqlWrapper = wrapper;    
        }

        public SqlWebErrorHandler(string connectionString)
        {
            sqlWrapper = new SQLWrapper(connectionString);
        }

        public void CatchError(Exception ex, string tableName)
        {
            if (sqlWrapper != null)
            {
                string inner = "";
                if (ex.InnerException != null) inner = ex.InnerException.Message;
                sqlWrapper.ExecuteNonQueryCommand("INSERT INTO [" + tableName +
                        "] ([Date], [Message], [InnerMessage], [StackTrace]) " +
                        "VALUES (@date, @message, @inner, @stack)",
                    new SqlParameter[] {new SqlParameter("@date",DateTime.Now),
                                        new SqlParameter("@message", ex.Message),
                                        new SqlParameter("@inner", inner),
                                        new SqlParameter("@stack", ex.StackTrace)});
            }
            else
            {
                throw new NullReferenceException("Wrapper must be initialized!");
            }
        }

        public void CatchError(Exception ex, string tableName, ref Email mailHandler, string emailAddress)
        {
            try
            {
                CatchError(ex, tableName);
                mailHandler.SendEmail(emailAddress, emailAddress, "Site Error", ex.Message);
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

        public void CatchError(Exception ex, string tableName, string errorURL, ref Email mailHandler, string emailAddress)
        {
            try
            {
                CatchError(ex, tableName, ref mailHandler, emailAddress);
                HttpContext.Current.Response.Redirect(errorURL);
            }
            catch
            {
                throw;
            }
        }

        public void GenerateLog(string path)
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
