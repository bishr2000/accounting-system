using System;
using Config = System.Configuration.ConfigurationManager;

namespace AccountSystem
{
    internal class ASConfig
    {
        // config for the server connection
        string serverName = "";
        string databaseName = "";
        string timeOut = "";
        string portNo = "";
        internal ASConfig()
        {
            serverName = Convert.ToString(Config.AppSettings["Server"]);
            databaseName = Convert.ToString(Config.AppSettings["Database"]);
            timeOut = Convert.ToString(Config.AppSettings["Timeout"]);
            portNo = Convert.ToString(Config.AppSettings["PortNo"]);
        }

        internal string GetConnectionString(string userName, string password)
        {
            string connectionString = "";


            connectionString = "server=" + serverName
                + ";database=" + databaseName;
            connectionString += ";UID=" +
                userName +
                ";PWD=" + password;
            if (timeOut != null && !timeOut.Equals(""))
                connectionString += ";Connection Timeout=" + timeOut;
            if (portNo != null && !portNo.Equals(""))
                connectionString += ";Port=" + portNo;
            return connectionString;
        }
        internal string GetConnectionString()
        {
            string connectionString = "";                         
            connectionString = "server=" + serverName 
                + ";database=" + databaseName;
            connectionString += ";Integrated Security=True";            
            if (timeOut !=null &&!timeOut.Equals(""))
                connectionString += ";Connection Timeout=" + timeOut;
            if (portNo != null && !portNo.Equals(""))
                connectionString += ";Port=" + portNo;
            return connectionString;
        }
        internal string GetReportPath()
        {
            string path = Convert.ToString(Config.AppSettings["ReportPath"]);
            return path;
        }

    }
}
