using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Odbc;

namespace GSLib.Database.MySQL
{
    public class MySQLWrapper : DbWrapper<OdbcConnection, OdbcCommand, OdbcParameterCollection,
        OdbcParameter, OdbcConnectionStringBuilder, OdbcDataReader>
    {
        public MySQLWrapper(OdbcConnection connection) : base(connection) { }

        public MySQLWrapper(string connectionString)
        {
            connection = new OdbcConnection(connectionString);
        }

        public MySQLWrapper(string server, string database, string uid, string password)
        {
            StringBuilder str = new StringBuilder();
            const string driver = "Driver={MySQL ODBC 3.51 Driver};";
            str.Append(driver);
            str.Append("SERVER=" + server + ";");
            str.Append("DATABASE=" + database + ";");
            str.Append("UID=" + uid + ";");
            str.Append("PWD=" + password + ";");
            connection = new OdbcConnection(str.ToString());
        }

        public OdbcDataReader ExecuteReaderCommand(string commandStr, OdbcParameter[] paramList)
        {
            OdbcDataReader result = null;
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

        public void AddParameter(string name, object value)
        {
            parameters.Add(new OdbcParameter(name, value));
        }

        public override void CreateCommand(string commandStr)
        {
            try
            {
                command = new OdbcCommand(commandStr, connection);
            }
            catch
            {
                throw;
            }
        }
    }
}
