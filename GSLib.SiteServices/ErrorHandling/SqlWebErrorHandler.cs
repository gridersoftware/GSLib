using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GSLib.Database.SQLServer;
using System.Data.SqlClient;
using System.IO;
using System.Web;

namespace GSLib.SiteServices.ErrorHandling
{
    public class SqlWebErrorHandler : WebErrorHandler<SQLWrapper, SqlParameter>
    {
        public SqlWebErrorHandler() 
        {
            wrapper = null;
        }

        public SqlWebErrorHandler(SQLWrapper wrapper)
        {
            this.wrapper = wrapper;
        }

        public SqlWebErrorHandler(string connectionString)
        {
            wrapper = new SQLWrapper(connectionString);
        }

        public override void CatchError(Exception ex, string tableName)
        {
            if (wrapper != null)
            {
                string inner = "";
                if (ex.InnerException != null) inner = ex.InnerException.Message;
                wrapper.ExecuteNonQueryCommand("INSERT INTO [" + tableName +
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
    }
}
