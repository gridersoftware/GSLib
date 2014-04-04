using System;

namespace GSLib.Database
{
    /// <summary>
    /// Exposes a database wrapper which supports database I/O on a basic level.
    /// </summary>
    public interface IDbWrapper
    {
        /// <summary>
        /// Closes an existing database connection.
        /// </summary>
        /// <returns>Returns true on success, otherwise returns false.</returns>
        bool CloseConnection();

        /// <summary>
        /// Creates a new command and stores it.
        /// </summary>
        /// <param name="commandStr">Command string.</param>
        void CreateCommand(string commandStr);

        /// <summary>
        /// Executes the current (stored) command as a non-query against the database.
        /// </summary>
        /// <param name="useParams">Determines whether to use parameters.</param>
        /// <returns>Returns the result of the non-query.</returns>
        object ExecuteNonQuery(bool useParams = false);
        
        /// <summary>
        /// Executes a given command as a non-query against the database.
        /// </summary>
        /// <param name="commandStr">Command string.</param>
        /// <param name="useParams">Determines whether to use parameters.</param>
        /// <returns>Returns the result of the non-query.</returns>
        object ExecuteNonQueryCommand(string commandStr, bool useParams = false);

        /// <summary>
        /// Executes the current (stored) command as a scalar against the database.
        /// </summary>
        /// <param name="useParams">Determines whether to use parameters.</param>
        /// <returns>Returns the result of the scalar.</returns>
        object ExecuteScalar(bool useParams = false);

        /// <summary>
        /// Executes the given command as a scalar against the database.
        /// </summary>
        /// <param name="commandStr">Command string.</param>
        /// <param name="useParams">Determines whether to use parameters.</param>
        /// <returns>Returns the result of the scalar.</returns>
        object ExecuteScalarCommand(string commandStr, bool useParams = false);

        /// <summary>
        /// Opens a database connection.
        /// </summary>
        /// <returns>Returns true on success, otherwise return false.</returns>
        bool OpenConnection();

        /// <summary>
        /// Gets a value determining if the connection is open.
        /// </summary>
        bool ConnectionIsOpen { get; }
    }
}
