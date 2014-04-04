using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GSLib.ErrorHandling
{
    /// <summary>
    /// Contains database table information for the AutoErrorHandler class.
    /// </summary>
    /// <remarks>
    /// This structure is used to interface your database table with the AutoErrorHandler class. Such a
    /// table should have the following columns, whose data types are listed in parentheses:
    /// an auto-incrementing indexer, exception class name (string), exception message (string), 
    /// inner exception class (string), inner exception name (string), call stack (string), 
    /// user description (string), solved (boolean), solution (string).
    /// 
    /// When calling the constructor of this type, you will enter the table name, and the names of all
    /// columns EXCEPT the indexer.
    /// </remarks>
    public struct AutoErrorHandlerDbSchema
    {
        string[] columns;
        string tableName;

        public AutoErrorHandlerDbSchema(string tableName,
                                        string exceptionClass,
                                        string exceptionMessage,
                                        string innerExceptionClass,
                                        string innerExceptionMessage,
                                        string callStack,
                                        string userDescription,
                                        string solved,
                                        string solution)
        {
            columns = new string[(int)ColumnType.Solution + 1];
            this.tableName = tableName;
            this[ColumnType.ExceptionClass] = exceptionClass;
            this[ColumnType.ExceptionMessage] = exceptionMessage;
            this[ColumnType.InnerExceptionClass] = innerExceptionClass;
            this[ColumnType.InnerExceptionMessage] = innerExceptionMessage;
            this[ColumnType.CallStack] = callStack;
            this[ColumnType.UserDescription] = userDescription;
            this[ColumnType.Solved] = solved;
            this[ColumnType.Solution] = solution;
        }

        public enum ColumnType
        {
            ExceptionClass,
            ExceptionMessage,
            InnerExceptionClass,
            InnerExceptionMessage,
            CallStack,
            UserDescription,
            Solved,
            Solution
        }

        public string TableName
        {
            get
            {
                return tableName;
            }
        }

        /// <summary>
        /// Gets or sets the column name of the given ColumnType.
        /// </summary>
        /// <param name="column">Type of column to get or set.</param>
        /// <returns></returns>
        public string this[ColumnType column]
        {
            get
            {
                return columns[(int)column];
            }
            set
            {
                columns[(int)column] = value;
            }
        }

        /// <summary>
        /// Gets the name of the given column.
        /// </summary>
        /// <param name="column">Column to get the name of.</param>
        /// <returns>Returns the name of the column.</returns>
        public string GetColumnName(ColumnType column)
        {
            return this[column];
        }
    }
}
