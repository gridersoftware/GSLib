using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using System.Diagnostics;

namespace GSLib.Database
{
    /// <summary>
    /// Represents a wrapper around a database connection, and provides methods
    /// to work with a database.
    /// </summary>
    /// <typeparam name="TConnection">Database connection type</typeparam>
    /// <typeparam name="TCommand">Database command type</typeparam>
    /// <typeparam name="TParameterCollection">Database parameter collection type</typeparam>
    /// <typeparam name="TParameter">Database parameter</typeparam>
    /// <typeparam name="TConnectionBuilder">Database connection string builder type</typeparam>
    /// <typeparam name="TDataReader">Database data reader type</typeparam>
    /// VB: Public Class DbWrapper(Of TConnection As DbConnection,
    ///                            TCommand As DbCommand,
    ///                            TParameterCollection As DbParameterCollection
    ///                            TParameter As DbParameter
    ///                            TConnectionBuilder As DbConnectionStringBuilder
    ///                            TDataReader As DbDataReader)
    ///             Inherits IDbWrapper
    public class DbWrapper<TConnection, TCommand, TParameterCollection, TParameter, TConnectionBuilder, TDataReader> : IDbWrapper
        where TConnection : DbConnection 
        where TCommand : DbCommand
        where TParameterCollection : DbParameterCollection
        where TParameter : DbParameter
        where TConnectionBuilder : DbConnectionStringBuilder
        where TDataReader : DbDataReader 
    {
        /***********************************************************************
         * Protected Fields
         **********************************************************************/
        /// <summary>
        /// Determines whether the connection is currently open.
        /// </summary>
        /// VB: Protected connectionOpen As Boolean
        protected bool connectionOpen;

        /// <summary>
        /// Represents a database connection
        /// </summary>
        /// VB: Protected connection As TConnection
        protected TConnection connection;

        /// <summary>
        /// Represents the current database command
        /// </summary>
        /// VB: Protected command As TCommand
        protected TCommand command;

        /// <summary>
        /// Represents the current command parameters
        /// </summary>
        /// VB: Protected parameters as TParameterCollection
        protected TParameterCollection parameters;

        /***********************************************************************
         * Constructors
         **********************************************************************/
        /// <summary>
        /// Default constructor. Sets connection to null.
        /// </summary>
        /// <remarks>When using this constructor, make sure you initialize the connection before using it.</remarks>
        /// VB: Public Sub New()
        public DbWrapper() : base()
        {
            connection = null;
        }

        /// <summary>
        /// Constructor. Initialize the connection with the given database connection.
        /// </summary>
        /// <param name="connect">Database connection to use.</param>
        /// VB: Public Sub New(ByVal connect As TConnection)
        public DbWrapper(TConnection connect) : base()
        {
            connection = connect;
        }

        
        /***********************************************************************
         * Private Methods
         **********************************************************************/ 
        /// <summary>
        /// Populate the command parameters list from the sqlParams.
        /// </summary>
        protected void PopulateParameters()
        {
            command.Parameters.Clear();

            TParameter[] paramArray = new TParameter[parameters.Count];
            parameters.CopyTo(paramArray, 0);
            command.Parameters.AddRange(paramArray);
        }

        /// <summary>
        /// Populate the command parameters list from the given parameter list.
        /// </summary>
        /// <param name="paramList"></param>
        protected void PopulateParameters(TParameter[] paramList)
        {
            command.Parameters.Clear();
            command.Parameters.AddRange(paramList);
        }
        /***********************************************************************
         * Public Properties
         **********************************************************************/
        /// <summary>
        /// Gets whether or not the connection is open.
        /// </summary>
        public bool ConnectionIsOpen
        {
            get
            {
                return connectionOpen;
            }
        }

        /// <summary>
        /// Gets the current database connection.
        /// </summary>
        public TConnection Connection
        {
            get { return connection; }
        }

        /// <summary>
        /// Gets the current command.
        /// </summary>
        /// VB: Public ReadOnly Property TypeCommandCommand As TCommand
        public TCommand Command
        {
            get
            {
                return command;
            }
        }

        /// <summary>
        /// Gets the parameter collection.
        /// </summary>
        /// VB: Public Property ParameterCollection As TParameterCollection
        public TParameterCollection ParameterCollection
        {
            get
            {
                return parameters;
            }
        }

        /***********************************************************************
         * Public Methods
         **********************************************************************/
        
        /// <summary>
        /// Opens the connection.
        /// </summary>
        /// <returns>If the connection is already open, returns false. Otherwise, returns true.</returns>
        /// VB: Public Function OpenConnection() As Boolean
        public bool OpenConnection()
        {
            try
            {
                if (!connectionOpen)
                {
                    connection.Open();
                    connectionOpen = true;
                    return true;
                }
                return false;   // the connection was already open
            }
            catch
            {
                throw;
            }
        }
        
        /// <summary>
        /// Closes the connection.
        /// </summary>
        /// <returns>If the connection is already closed, returns false. Otherwise, returns true.</returns>
        /// <exception cref="DbException"></exception>
        /// VB: publics Function CloseConnection() As Boolean
        public bool CloseConnection()
        {
            try
            {
                if (connectionOpen)
                {
                    connection.Close();
                    connectionOpen = false;
                    return true;
                }
                return false;
            }
            catch
            {
                throw;
            }
        } 

        /// <summary>
        /// Creates a new SQL command based on the given command string.
        /// </summary>
        /// <param name="commandStr">Command string to use</param>
        public abstract void CreateCommand(string commandStr);
        
        public void CreateCommand(TCommand command)
        {
            this.command = command;
        }
        
        /// <summary>
        /// Executes the current command as a reader.
        /// </summary>
        /// <param name="useParams">Determines whether or not to use the parameter list.</param>
        /// <returns>Returns a data reader containing the results of the query.</returns>
        public TDataReader ExecuteReader(bool useParams = false)
        {
            try
            {
                if (useParams)
                {
                    PopulateParameters();
                }
                return (TDataReader)command.ExecuteReader();
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Executes the current command as a reader.
        /// </summary>
        /// <param name="paramList"></param>
        /// <returns></returns>
        public TDataReader ExecuteReader(TParameter[] paramList)
        {
            Debug.Assert(command != null);

            try
            {
                PopulateParameters(paramList);
                return (TDataReader)command.ExecuteReader();
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Executes the given command as a reader.
        /// </summary>
        /// <param name="commandStr"></param>
        /// <param name="useParams"></param>
        /// <returns></returns>
        public DbDataReader ExecuteReaderCommand(string commandStr, bool useParams = false)
        {
            TDataReader reader;

            try
            {
                OpenConnection();
                CreateCommand(commandStr);
                reader = ExecuteReader(useParams);
            }
            catch
            {
                throw;
            }

            return reader;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="commandStr"></param>
        /// <param name="useParams"></param>
        /// <returns></returns>
        public object ExecuteScalarCommand(string commandStr, bool useParams = false)
        {
            object result;

            try
            {
                OpenConnection();
                CreateCommand(commandStr);
                result = ExecuteScalar(useParams);
            }
            catch
            {
                throw;
            }

            return result;
        }

        public object ExecuteScalarCommand(string commandStr, TParameter[] paramList)
        {
            object result;

            try
            {
                OpenConnection();
                CreateCommand(commandStr);
                result = ExecuteScalar(paramList);
            }
            catch
            {
                throw;
            }
            
            return result;
        }

        public object ExecuteNonQueryCommand(string commandStr, bool useParams = false)
        {
            object result;

            try
            {
                OpenConnection();
                CreateCommand(commandStr);
                result = ExecuteNonQuery(useParams);
            }
            catch
            {
                throw;
            }
            
            return result;
        }

        public object ExecuteNonQueryCommand(string commandStr, TParameter[] paramList)
        {
            object result;

            try
            {
                OpenConnection();
                CreateCommand(commandStr);
                result = ExecuteNonQuery(paramList);
            }
            catch
            {
                throw;
            }

            return result;
        }

        
        public object ExecuteScalar(bool useParams = false)
        {
            try
            {
                if (useParams) PopulateParameters();
                return command.ExecuteScalar();
            }
            catch
            {
                throw;
            }
        }


        public object ExecuteScalar(TParameter[] paramList)
        {
            try
            {
                PopulateParameters(paramList);
                return command.ExecuteScalar();
            }
            catch
            {
                throw;
            }
        }

        public object ExecuteNonQuery(bool useParams = false)
        {
            try
            {
                PopulateParameters();
                return command.ExecuteNonQuery();
            }
            catch
            {
                throw;
            }
        }

        public object ExecuteNonQuery(TParameter[] paramList)
        {
            try
            {
                PopulateParameters(paramList);
                return command.ExecuteNonQuery();
            }
            catch
            {
                throw;
            }
        }
        
        
    }

    
}
