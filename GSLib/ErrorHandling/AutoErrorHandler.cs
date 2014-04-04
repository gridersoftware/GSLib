using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using GSLib.Database;

namespace GSLib.ErrorHandling
{
    /// <summary>
    /// Represents an error handler that writes exception data to a database.
    /// </summary>
    public abstract class AutoErrorHandler : ErrorHandler 
    {
        /// <summary>
        /// The database that exception data will be written to.
        /// </summary>
        protected IDbWrapper database;
        /// <summary>
        /// Table schema for storing exception data.
        /// </summary>
        protected AutoErrorHandlerDbSchema schema;

        /// <summary>
        /// Initializes a new AutoErrorHandler object with the given database and table schema.
        /// </summary>
        /// <param name="database">Database to write to.</param>
        /// <param name="tableSchema">Table schema.</param>
        public AutoErrorHandler(IDbWrapper database, AutoErrorHandlerDbSchema tableSchema) : base()
        {
            this.database = database;
            schema = tableSchema;
        }

        /// <summary>
        /// Catches an error and stores the exception in the database.
        /// </summary>
        /// <param name="ex">Exception to catch.</param>
        public new abstract void CatchError(Exception ex);
    }
}
