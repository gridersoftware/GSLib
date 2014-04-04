using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using GSLib.Database.SQLServer;

namespace GSLib.ErrorHandling
{
    /// <summary>
    /// Represents an error handler that automatically stores errors in a properly-formated SQL Server table.
    /// </summary>
    public class SqlAutoErrorHandler : AutoErrorHandler
    {
        /// <summary>
        /// Database to store data in.
        /// </summary>
        protected new SQLWrapper database;

        /// <summary>
        /// Sets the database to null, and the schema to an empty schema. You
        /// must reinitialize with parameters to use.
        /// </summary>
        public SqlAutoErrorHandler() : base(null, new AutoErrorHandlerDbSchema())
        {
            database = null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="database"></param>
        /// <param name="tableSchema"></param>
        public SqlAutoErrorHandler(SQLWrapper database, AutoErrorHandlerDbSchema tableSchema) : 
            base(database, tableSchema)
        {
            this.database = database;
            schema = tableSchema;
        }

        public new event EventHandler<AutoErrorHandlerEventArgs> ErrorCaught;

        /// <summary>
        /// Stores an exception in the database and raises the ErrorCaught event.
        /// </summary>
        /// <param name="ex">Exception to store.</param>
        public override void CatchError(Exception ex)
        {
            CatchError(ex, "");
        }

        /// <summary>
        /// Stores an exception and user-provided information in the database and raises the ErrorCaught event.
        /// </summary>
        /// <param name="ex">Exception to store.</param>
        /// <param name="userDescription">Description of error from the user.</param>
        /// <remarks>Note that this function can itself throw exceptions if there are problems related to the database.</remarks>
        public void CatchError(Exception ex, string userDescription)
        {
            ErrorCaught(this, new AutoErrorHandlerEventArgs(ex, userDescription));

            database.AddSqlParameter("@ExceptionClass", ex.GetType().Name);
            database.AddSqlParameter("@ExceptionMessage", ex.Message);

            if (ex.InnerException != null)
            {
                database.AddSqlParameter("@InnerExceptionClass", ex.InnerException.GetType().Name);
                database.AddSqlParameter("@InnerExceptionMessage", ex.InnerException.Message);
            }
            else
            {
                database.AddSqlParameter("@InnerExceptionClass", "");
                database.AddSqlParameter("@InnerExceptionMessage", "");
            }

            database.AddSqlParameter("@CallStack", ex.StackTrace);
            database.AddSqlParameter("@UserDescription", userDescription);
            database.AddSqlParameter("@Solved", false);
            database.AddSqlParameter("@Solution", "");

            StringBuilder query = new StringBuilder();
            query.Append("INSERT INTO [");
            query.Append(schema.TableName);
            query.Append("] (");
            query.Append(schema[AutoErrorHandlerDbSchema.ColumnType.ExceptionClass] + ", ");
            query.Append(schema[AutoErrorHandlerDbSchema.ColumnType.ExceptionMessage] + ", ");
            query.Append(schema[AutoErrorHandlerDbSchema.ColumnType.InnerExceptionClass] + ", ");
            query.Append(schema[AutoErrorHandlerDbSchema.ColumnType.InnerExceptionMessage] + ", ");
            query.Append(schema[AutoErrorHandlerDbSchema.ColumnType.CallStack] + ", ");
            query.Append(schema[AutoErrorHandlerDbSchema.ColumnType.UserDescription] + ", ");
            query.Append(schema[AutoErrorHandlerDbSchema.ColumnType.Solved] + ", ");
            query.Append(schema[AutoErrorHandlerDbSchema.ColumnType.Solution]);
            query.Append(") VALUES (@ExceptionClass, @ExceptionMessage, @InnerExceptionClass, ");
            query.Append("@InnerExceptionMessage, @CallStack, @UserDescription, @Solved, @Solution");

            try
            {
                database.ExecuteNonQueryCommand(query.ToString(), true);
            }
            catch
            {
                throw;
            }
        }
    }
}
