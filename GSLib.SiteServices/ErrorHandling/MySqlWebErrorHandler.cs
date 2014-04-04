using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GSLib.Database.MySQL;
using System.Data.Odbc;
using System.IO;
using System.Web;

namespace GSLib.SiteServices.ErrorHandling
{
    public class MySqlWebErrorHandler : WebErrorHandler<MySQLWrapper, OdbcParameter>
    {
        public MySqlWebErrorHandler()
        {
            wrapper = null;
        }

        public MySqlWebErrorHandler(MySQLWrapper wrapper)
        {
            this.wrapper = wrapper;
        }

        public MySqlWebErrorHandler(string connectionString)
        {
            wrapper = new MySQLWrapper(connectionString);
        }

        public override void CatchError(Exception ex, string tableName)
        {
            try
            {
                if (wrapper != null)
                {
                    string inner = "";
                    if (ex.InnerException != null) inner = ex.InnerException.Message;
                    wrapper.ExecuteNonQueryCommand("INSERT INTO [" + tableName +
                            "] ([Date], [Message], [InnerMessage], [StackTrace]) " +
                            "VALUES (@date, @message, @inner, @stack)",
                        new OdbcParameter[] {new OdbcParameter("@date",DateTime.Now),
                                        new OdbcParameter("@message", ex.Message),
                                        new OdbcParameter("@inner", inner),
                                        new OdbcParameter("@stack", ex.StackTrace)});
                }
                else
                {
                    throw new NullReferenceException("Wrapper must be initialized!");
                }
            }
            catch
            {
                throw;
            }
        }
    }
}
