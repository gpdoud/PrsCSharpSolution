using System;

using Microsoft.Data.SqlClient;

namespace PrsDbLib {

    public class PrsConnection {

        public SqlConnection sqlConnection { get; set; } = null;

        public void Connect() {
            sqlConnection.Open();
            if(sqlConnection.State != System.Data.ConnectionState.Open) {
                throw new Exception("SqlConnection failed to open!");
            }
            System.Diagnostics.Debug.WriteLine("SqlConnection opened");
            return;
        }

        public void Disconnect() {
            if(sqlConnection == null) {
                return;
            }
            sqlConnection.Close();
            sqlConnection = null;
            System.Diagnostics.Debug.WriteLine("SqlConnection closed.");
        }

        public PrsConnection(string server, string database) {
            var connStr = $"server={server}\\sqlexpress;database={database};trusted_connection=true;";
            sqlConnection = new SqlConnection(connStr);
        }
    }
}
