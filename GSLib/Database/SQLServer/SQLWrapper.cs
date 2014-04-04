using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace GSLib.Database.SQLServer
{
    public class SQLWrapper : DbWrapper<SqlConnection, SqlCommand, SqlParameterCollection, 
        SqlParameter, SqlConnectionStringBuilder, SqlDataReader>
    {
        public SQLWrapper(SqlConnection connection) : base(connection) { }

        public SQLWrapper(string connectionString)
        {
            connection = new SqlConnection(connectionString);
        }

        public SQLWrapper(SqlConnectionStringBuilder builder)
        {
            connection = new SqlConnection(builder.ToString());
        }

        public override void CreateCommand(string commandStr)
        {
            try
            {
                command = new SqlCommand(commandStr, connection);
            }
            catch
            {
                throw;
            }
        }

        public new SqlDataReader ExecuteReaderCommand(string commandStr, bool useParams = false)
        {
            SqlDataReader result = null;
            try
            {
                OpenConnection();
                CreateCommand(commandStr);
                result = ExecuteReader(useParams);
            }
            catch
            {
                throw;
            }
            
            return result;
        }

        public SqlDataReader ExecuteReaderCommand(string commandStr, SqlParameter[] paramList)
        {
            SqlDataReader result = null;
            try
            {
                OpenConnection();
                CreateCommand(commandStr);
                result = ExecuteReader(paramList);
            }
            catch
            {
                throw;
            }
            
            return result;
        }

        public new object ExecuteScalarCommand(string commandStr, bool useParams = false)
        {
            object result = null;
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

        public new object ExecuteScalarCommand(string commandStr, SqlParameter[] paramList)
        {
            object result = null;
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

        public new object ExecuteNonQueryCommand(string commandStr, bool useParams = false)
        {
            object result = null;
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

        public new object ExecuteNonQueryCommand(string commandStr, SqlParameter[] paramList)
        {
            object result = null;
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

        public void AddSqlParameter(string name, object value)
        {
            parameters.Add(new SqlParameter(name, value));
        }
    }
}
