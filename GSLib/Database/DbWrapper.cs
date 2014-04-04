using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using System.Diagnostics;

namespace GSLib.Database
{
    /// <summary>
    /// Represents a database wrapper
    /// </summary>
    /// Public MustInherit Class DbWrapper
    public abstract class DbWrapper
    {
        /***********************************************************************
         * Private Fields
         **********************************************************************/
        /// <summary>
        /// Represents a database connection
        /// </summary>
        /// VB: Protected connection As DbConnection
        protected DbConnection connection;

        /// <summary>
        /// Represents the current database command
        /// </summary>
        /// VB: Protected command As DbCommand
        protected DbCommand command;

        /// <summary>
        /// Determines if a connection is currently opened or closed
        /// </summary>
        /// VB: Protected connectionOpen as Boolean
        protected bool connectionOpen;

        /// <summary>
        /// Represents the current command parameters
        /// </summary>
        /// VB: Protected parameters as DbParameterCollection
        protected DbParameterCollection parameters;

        /***********************************************************************
         * Constructors
         **********************************************************************/
        /// <summary>
        /// Default constructor. Sets the connection to null. 
        /// </summary>
        /// VB: Public Sub New()
        public DbWrapper()
        {
            connection = null;
        }

        /***********************************************************************
         * Private Methods
         **********************************************************************/
        /// <summary>
        /// Populate the command parameters list from the sqlParams.
        /// </summary>
        /// <exception cref="NullReferenceException">Thrown if command is null.</exception>
        /// VB: Protected MustOverride Sub PopulateParameters()
        protected abstract void PopulateParameters();

        /// <summary>
        /// Populate the command parameters list from the given parameter list.
        /// </summary>
        /// <param name="paramList">Parameters to use.</param>
        /// <exception cref="ArgumentNullException">Thrown if paramList is null.</exception>
        /// <exception cref="NullReferenceException">Thrown if command is null.</exception>
        /// VB: Protected MustOverride Sub PopulateParameters(ByVal paramList() As DbParameter)
        protected abstract void PopulateParameters(DbParameter[] paramList);

        /***********************************************************************
         * Public Properties
         **********************************************************************/
        /// <summary>
        /// Gets a value determining if the database connection is open.
        /// </summary>
        /// VB: Public ReadOnly Property ConnectionIsOpen As Boolean
        public bool ConnectionIsOpen
        {
            get
            {
                return connectionOpen;
            }
        }

        /// <summary>
        /// Gets the current database command
        /// </summary>
        /// VB: Public Overridable ReadOnly Property Command As DbCommand
        public virtual DbCommand Command
        {
            get
            {
                return command;
            }
        }

        /// <summary>
        /// Gets the current database parameters
        /// </summary>
        /// VB: Public Overridable ReadOnly Property ParameterCollection As DbParameterCollection
        public virtual DbParameterCollection ParameterCollection
        {
            get
            {
                return parameters;
            }
        }

        /// <summary>
        /// Gets the current database connection.
        /// </summary>
        /// VB: Public MustOverride ReadOnly Property Connection As DbConnection
        public abstract DbConnection Connection { get; }

        /***********************************************************************
         * Public Methods
         **********************************************************************/
        /// <summary>
        /// Opens the connection.
        /// </summary>
        /// <returns>If the connection is already open, returns false. Otherwise, returns true.</returns>
        /// VB: Public MustOverride Function OpenConnection() As Boolean
        public abstract bool OpenConnection();

        /// <summary>
        /// Closes the connection.
        /// </summary>
        /// <returns>If the connection is already closed, returns false. Otherwise, returns true.</returns>
        /// <exception cref="DbException">Thrown if there was a problem closing the database connection.</exception>
        /// VB: Public Overridable Function CloseConnection() As Boolean
        public virtual bool CloseConnection()
        {
            try
            {
                if (connectionOpen)
                {
                    connection.Close();
                    connectionOpen = false;
                    return true;
                }
                return false;   // connection was already closed
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Creates a new command based on the given command string.
        /// </summary>
        /// <param name="commandStr">Command to use.</param>
        /// VB: Public MustOverride Sub CreateCommand(ByVal commandStr As String)
        public abstract void CreateCommand(string commandStr);

        /// <summary>
        /// Executes the current Command against the current Connection and returns a DbDataReader.
        /// </summary>
        /// <!-- alt summary: Executes the current [[GSLib.Database.DbWrapper.Command Property|Command]]
        /// against the current [[GSLib.Database.DbWrapper.Connection Property|Connection]] and returns
        /// a DbDataReader.-->
        /// <param name="useParams">Determines whether to use parameters added to the DbWrapper.</param>
        /// <returns>Returns</returns>
        /// VB: Public Overridable Function ExecuteReader(Optional ByVal useParams As Boolean = False) As DbDataReader
        public virtual DbDataReader ExecuteReader(bool useParams = false)
        {
            try
            {
                if (useParams)
                {
                    PopulateParameters();
                }
                return command.ExecuteReader();
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Executes the current Command against the current Connection and returns a DbDataReader.
        /// </summary>
        /// <!-- alt summary: Executes the current [[GSLib.Database.DbWrapper.Command Property|Command]]
        /// against the current [[GSLib.Database.DbWrapper.Connection Property|Connection]] and returns
        /// a DbDataReader.-->
        /// <param name="paramList">Parameters to use.</param>
        /// <returns>Returns a DbDataReader containing the records.</returns>
        /// <exception cref="ArgumentNullException">Thrown if paramList is null.</exception>
        /// VB: Public Overridable Function ExecuteReader(ByVal paramList() As DbParameter) As DbDataReader
        public virtual DbDataReader ExecuteReader(DbParameter[] paramList)
        {
            if (paramList == null) throw new ArgumentNullException();

            try
            {
                PopulateParameters(paramList);
                return command.ExecuteReader();
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Executes the current Command against the current Connection and returns the first column of the first row of the result set returned by the query. All other columns and rows are ignored.
        /// </summary>
        /// <!-- alt summary: Executes the current [[GSLib.Database.DbWrapper.Command Property|Command]]
        /// against the current [[GSLib.Database.DbWrapper.Connection Property|Connection]] and returns
        /// the first column of the first row of the result set returned by the query. All other columns and rows are ignored.-->
        /// <param name="useParams">Determines whether to use parameters contained in the DbWrapper.</param>
        /// <returns>Returns an object</returns>
        /// VB: Public Overridable Function ExecuteScalar(Optional ByVal useParams As Boolean = False) As Object
        public virtual object ExecuteScalar(bool useParams = false)
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

        /// <summary>
        /// Executes the current Command against the current Connection and returns the first column of the first row of the result set returned by the query. All other columns and rows are ignored.
        /// </summary>
        /// <!-- alt summary: Executes the current [[GSLib.Database.DbWrapper.Command Property|Command]]
        /// against the current [[GSLib.Database.DbWrapper.Connection Property|Connection]] and returns
        /// the first column of the first row of the result set returned by the query. All other columns and rows are ignored.-->
        /// <param name="paramList">Parameter list to use.</param>
        /// <returns>Returns an object representing the first column of the first row of the result.</returns>
        /// <exception cref="ArgumentNullException">Thrown if paramList is null.</exception>
        /// VB: Public Overridable Function ExecuteScalar(Optional ByVal paramList() As DbParameter) As Object
        public virtual object ExecuteScalar(DbParameter[] paramList)
        {
            if (paramList == null) throw new ArgumentNullException();
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

        /// <summary>
        /// Executes the current Command against the current Connection.
        /// </summary>
        /// <!-- alt summary: Executes the current [[GSLib.Database.DbWrapper.Command Property|Command]]
        /// against the current [[GSLib.Database.DbWrapper.Connection Property|Connection]].-->
        /// <param name="useParams">Determines whether to use parameters contained in the DbWrapper</param>
        /// <returns>Returns then number of rows affected.</returns>
        /// VB: Public Overridable Function ExecuteNonQuery(Optional ByVal useParams As Boolean = False) As Object
        public virtual int ExecuteNonQuery(bool useParams = false)
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

        /// <summary>
        /// Executes the current Command against the current Connection.
        /// </summary>
        /// <!-- alt summary: Executes the current [[GSLib.Database.DbWrapper.Command Property|Command]]
        /// against the current [[GSLib.Database.DbWrapper.Connection Property|Connection]].-->
        /// <param name="paramList">Parameter list to use.</param>
        /// <returns>Returns the number of rows affected.</returns>
        /// <exception cref="ArgumentNullException">Thrown if paramList is null.</exception>
        /// VB: Public Overridable Function ExecuteNonQuery(ByVal paramList() As DbParameter) As Object
        public virtual int ExecuteNonQuery(DbParameter[] paramList)
        {
            if (paramList == null) throw new ArgumentNullException();
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

        /// <summary>
        /// Executes the given command against the current Connection and returns a DbDataReader.
        /// </summary>
        /// <!-- alt summary: Executes the given command against the current 
        /// [[GSLib.Database.DbWrapper.Connection Property|Connection]] and returns a DbDataReader.-->
        /// <param name="commandStr">Command string to execute.</param>
        /// <param name="useParams">Determines whether to use the current parameters.</param>
        /// <returns>Returns a DbDataReader.</returns>
        /// <exception cref="ArgumentNullException">Thrown if commandStr is null.</exception>
        /// <exception cref="DbException">Thrown if a database error occurs.</exception>
        /// <remarks>When implementing this method, it may be a good idea to implement it as a 
        /// call to CreateCommand() followed by ExecuteReader(bool), both wrapped in a Try-Catch
        /// statement. If it is implemented this way, remember to document that the method changes
        /// the Command property.</remarks>
        /// VB: Public MustOverride Function ExecuteReaderCommand(ByVal commandStr As String, Optional ByVal useParams As Boolean = False) As DbDataReader
        public abstract DbDataReader ExecuteReaderCommand(string commandStr, bool useParams = false);

        /// <summary>
        /// Executes the current command against the current Connection and returns a DbDataReader.
        /// </summary>
        /// <!-- alt summary: Executes the given command against the current 
        /// [[GSLib.Database.DbWrapper.Connection Property|Connection]] and returns a DbDataReader.-->
        /// <param name="commandStr">Command string to execute.</param>
        /// <param name="paramList">Parameter list to use.</param>
        /// <returns>Returns a DbDataReader.</returns>
        /// <exception cref="ArgumentNullException">Thrown if one or both arguments are null.</exception>
        /// <exception cref="DbException">Thrown if a database error occurs.</exception>
        /// <remarks>When implementing this method, it may be a good idea to implement it as a 
        /// call to CreateCommand() followed by ExecuteReader(bool), both wrapped in a Try-Catch
        /// statement. If it is implemented this way, remember to document that the method changes
        /// the Command property.</remarks>
        /// VB: Public MustOverride Function ExecuteReaderCommand(ByVal commandStr As String, Optional ByVal paramList() As DbParameter) As DbDataReader
        public abstract DbDataReader ExecuteReaderCommand(string commandStr, DbParameter[] paramList);

        /// <summary>
        /// Executes the given command against the current Connection and returns 
        /// the first column of the first row of the result set returned by the query.
        /// All other columns and rows are ignored.
        /// </summary>
        /// /// <!-- alt summary: Executes the given command against the current 
        /// [[GSLib.Database.DbWrapper.Connection Property|Connection]] and the first 
        /// column of the first row of the result set returned by the query. All other 
        /// columns and rows are ignored.-->
        /// <param name="commandStr">Command to execute.</param>
        /// <param name="useParams">Determines whether to use </param>
        /// <returns></returns>
        /// VB: Public MustOverride Function ExecuteScalarCommand(ByVal commandStr As String, Optional ByVal useParams As Boolean = False) As Object
        public abstract object ExecuteScalarCommand(string commandStr, bool useParams = false);

        /// <summary>
        /// Executes the given command and returns the first column of the first row of the result set returned by the query. All other columns and rows are ignored.
        /// </summary>
        /// <param name="commandStr"></param>
        /// <param name="paramList"></param>
        /// <returns></returns>
        /// VB: Public MustOverride Function ExecuteScalarCommand(ByVal commandStr As String, Optional ByVal paramList() As DbParameter) As Object
        public abstract object ExecuteScalarCommand(string commandStr, DbParameter[] paramList);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="commandStr"></param>
        /// <param name="useParams"></param>
        /// <returns></returns>
        /// VB: Public MustOverride Function ExecuteNonQueryCommand(ByVal commandStr As String, Optional ByVal useParams As Boolean = False) As Object
        public abstract object ExecuteNonQueryCommand(string commandStr, bool useParams = false);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="commandStr"></param>
        /// <param name="paramList"></param>
        /// <returns></returns>
        /// VB: Public MustOverride Function ExecuteNonQueryCommand(ByVal commandStr As String, Optional ByVal paramList() As DbParameter) As Object
        public abstract object ExecuteNonQueryCommand(string commandStr, DbParameter[] paramList);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="param"></param>
        /// VB: Public Sub AddParameter(ByVal param As DbParameter)
        public void AddParameter(DbParameter param)
        {
            parameters.Add(param);
        }

        /// <summary>
        /// 
        /// </summary>
        /// VB: Public Sub ClearParameters()
        public void ClearParameters()
        {
            parameters.Clear();
        }
    }
}
