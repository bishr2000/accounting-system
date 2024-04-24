using System;
using System.Data.SqlClient;
using System.Data;

namespace AccountSystem
{
    internal class ASSqlServer
    {

        internal ASSqlServer()
        {
             
        }
        internal static SqlConnection sqlConnection = null;

        private Exception asException = null;
        internal Exception Exception
        {
            get { return asException; }
        }
        internal bool OpenConnection()
        {
            bool connectionStatus = false;
            try
            {
                ASConfig asConfig = new ASConfig();
                string connectionString = "";

                if(ASParameters.windowsOnly)
                    connectionString = asConfig.GetConnectionString();
                else
                    connectionString = asConfig.GetConnectionString(
                    ASParameters.userName, ASParameters.password);

                sqlConnection = new SqlConnection(connectionString);
                sqlConnection.Open();
                
                connectionStatus = true;
                
            }
            catch (SqlException ex)
            {
                asException = ex;
            }
            catch (Exception ex)
            {
                asException = ex;
            }
            return connectionStatus;
        }
         
        internal void CloseConnection()
        {            
            try
            {
                if (sqlConnection.State != ConnectionState.Closed)
                {
                    sqlConnection.Close();
                    sqlConnection.Dispose();
                }
            }
            catch (Exception ex)
            {
                asException = ex;
            }
            
        }
         
    }
}
