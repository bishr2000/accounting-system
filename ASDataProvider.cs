using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;

namespace AccountSystem
{
    class ASDataProvider
    {

        #region Constructor
        internal ASDataProvider()
        {
            if (ASSqlServer.sqlConnection.State != ConnectionState.Open)
            {
                ASSqlServer asSqlServer = new ASSqlServer();
                asSqlServer.OpenConnection();
            }
        }
        #endregion

        #region Properties
        private string errorMessage = "";
        public string ErrorMessage
        {
            get { return errorMessage; }
        }
        private string commandText = "";
        public string CommandText
        {
            get { return commandText; }
            set { commandText = value; }
        }
        private CommandType commandType = CommandType.Text;
        public CommandType CommandType
        {
            set { commandType = value; }
            get { return commandType; }
        }
        #endregion

        #region Define SqlParameter
         
        private void DefineSqlParameter(SqlCommand sqlCommand,
            string[] parameterName, object[] parameterValue)
        {
            SqlParameter sqlParameter;
            for (int i = 0; i <parameterName.Length; i++)
            {
                sqlParameter = new SqlParameter();
                sqlParameter.ParameterName =parameterName[i];
                sqlParameter.SqlValue =parameterValue[i];
                sqlCommand.Parameters.Add(sqlParameter);
            }
        }

        

        private void DefineSqlParameter(SqlCommand sqlCommand,
            string Parameter, object Value)
        {
            SqlParameter sqlParameter = new SqlParameter();
            sqlParameter.ParameterName = Parameter;
            sqlParameter.SqlValue = Value;
            sqlCommand.Parameters.Add(sqlParameter);
        }
        #endregion

        #region ExecuteNonQuery
        public int ExecuteNonQuery(
         string parameterName, string parameterValue)
        {
            int effectedRecord = 0;
            try
            {
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.CommandText = commandText;
                sqlCommand.Connection = ASSqlServer.sqlConnection;
                sqlCommand.CommandType = commandType;
                DefineSqlParameter(sqlCommand, parameterName, parameterValue);            
                effectedRecord = sqlCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                errorMessage = "Error: " + ex.Message;
            }

            return effectedRecord;
        }

        public int ExecuteNonQuery(
         string[] parameterName, object[] parameterValue)
        {
            int effectedRecord = 0;
            try
            {

                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.CommandText = commandText;
                sqlCommand.Connection = ASSqlServer.sqlConnection;
                sqlCommand.CommandType = commandType;
                DefineSqlParameter(sqlCommand, parameterName, parameterValue);            
                effectedRecord = sqlCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                errorMessage = "Error: " + ex.Message;
            }

            return effectedRecord;
        }
 

        public int ExecuteNonQuery()
        {
            int effectedRecord = 0;
            try
            {
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.CommandText = commandText;
                sqlCommand.Connection = ASSqlServer.sqlConnection;
                sqlCommand.CommandType = commandType;
             
                effectedRecord = sqlCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                errorMessage = "Error: " + ex.Message;
            }

            return effectedRecord;
        }

        public int ExecuteNonQuery(string[] parameterName, 
        object[] parameterValue, string returnParameterName)
        {
            int effectedRecord = 0;
            try
            {
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.CommandText = commandText;
                sqlCommand.Connection = ASSqlServer.sqlConnection;
                sqlCommand.CommandType = commandType;
                SqlParameter sqlParameter;
                for (int i = 0; i < parameterName.Length; i++)
                {

                    if (parameterName[i] == returnParameterName)
                    {
                        sqlParameter = new SqlParameter(parameterName[i], SqlDbType.Int);
                        sqlParameter.Direction =
                        ParameterDirection.ReturnValue;
                    }
                    else
                    {
                        sqlParameter = new SqlParameter();
                        sqlParameter.ParameterName = parameterName[i];
                        sqlParameter.SqlValue = parameterValue[i];
                    }
                    sqlCommand.Parameters.Add(sqlParameter);
                }
             
                sqlCommand.ExecuteNonQuery();
                effectedRecord = (int)sqlCommand.Parameters[returnParameterName].Value;
            }
            catch (Exception ex)
            {
                errorMessage = "Error: " + ex.Message;
            }

            return effectedRecord;
        }
        #endregion

        #region ExecuteScalar
        public object ExecuteScalar()
        {
            object objValue = null;
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.CommandText = commandText;
            sqlCommand.Connection = ASSqlServer.sqlConnection;
            sqlCommand.CommandType = commandType;
            try
            {
                objValue = sqlCommand.ExecuteScalar();
            }
            catch (Exception ex)
            {
                errorMessage = "Error: " + ex.Message;
            }

            return objValue;
        }

        public object ExecuteScalar( 
        string[] parameterName,
         object[] parameterValue)
        {
            object objValue = null;
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.CommandText = commandText;
            sqlCommand.Connection = ASSqlServer.sqlConnection;
            sqlCommand.CommandType = commandType;
            DefineSqlParameter(sqlCommand,parameterName,parameterValue);
            try
            {
                objValue = sqlCommand.ExecuteScalar();
            }
            catch (Exception ex)
            {
                errorMessage = "Error: " + ex.Message;
            }

            return objValue;
        }
        public object ExecuteScalar(
        string parameterName,
         object parameterValue)
        {
            object objValue = null;
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.CommandText = commandText;
            sqlCommand.Connection = ASSqlServer.sqlConnection;
            sqlCommand.CommandType = commandType;
            DefineSqlParameter(sqlCommand, parameterName, parameterValue);
            try
            {
                objValue = sqlCommand.ExecuteScalar();
            }
            catch (Exception ex)
            {
                errorMessage = "Error: " + ex.Message;
            }

            return objValue;
        }
        
        #endregion

     

        #region ExecuteReader and object         
        public object[] GeObjects(
        string parameterName, string parameterValue)
        {
            object[] objValue = null;
            try
            {
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.CommandText = commandText;
                sqlCommand.Connection = ASSqlServer.sqlConnection;
                sqlCommand.CommandType = commandType;
                int i = 0;
                DefineSqlParameter(sqlCommand, 
                    parameterName, parameterValue);

                SqlDataReader sqlDataReader;
                sqlDataReader = sqlCommand.ExecuteReader();
                i = sqlDataReader.FieldCount;
                if (sqlDataReader.Read())
                {
                    objValue = new object[i];
                    sqlDataReader.GetValues(objValue);
                }
                sqlDataReader.Close();
                sqlDataReader.Dispose();
            }
            catch (Exception ex)
            {
                errorMessage = "Error: " + ex.Message;

            }
            return objValue;
        }        
        #endregion

        #region DataSet
        public DataSet GetDataSet( )
        {
            DataSet dataSet = new DataSet();
            try
            {
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.CommandText = commandText;
                sqlCommand.CommandType = commandType;
                sqlCommand.Connection =
                    ASSqlServer.sqlConnection;
                SqlDataAdapter sqlDataAdapter =
                    new SqlDataAdapter(sqlCommand);
                sqlDataAdapter.Fill(dataSet);
                sqlDataAdapter.Dispose();
            }
            catch (Exception ex)
            {
                errorMessage = "Error: " + ex.Message;
            }
            return dataSet;

        }
        
        #endregion        

        #region DataTable
        public DataTable GetDataTable( )
        {
            DataTable dataTable = new DataTable();
            try
            {
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.CommandText = commandText;
                sqlCommand.CommandType = commandType;
                sqlCommand.Connection = ASSqlServer.sqlConnection;
                SqlDataAdapter sqlDataAdapter =
                    new SqlDataAdapter(sqlCommand);
                sqlDataAdapter.Fill(dataTable);
                sqlDataAdapter.Dispose();
            }
            catch (Exception ex)
            {
                errorMessage = "Error: " + ex.Message;
            }
            return dataTable;

        }

        public DataTable GetDataTable( 
            string[] parameterName, object[]parameterValue)
        {
            DataTable dataTable = new DataTable();
            try
            {
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.CommandText = commandText;
                sqlCommand.CommandType = commandType;
                sqlCommand.Connection = ASSqlServer.sqlConnection;
                DefineSqlParameter(sqlCommand, parameterName,  parameterValue);
                SqlDataAdapter sqlDataAdapter =
                    new SqlDataAdapter(sqlCommand);
                sqlDataAdapter.Fill(dataTable);
                sqlDataAdapter.Dispose();
            }
            catch (Exception ex)
            {
                errorMessage = "Error: " + ex.Message;
            }
            return dataTable;

        }
        public DataTable GetDataTable(
            string parameterName, object parameterValue)
        {
            DataTable dataTable = new DataTable();
            try
            {
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.CommandText = commandText;
                sqlCommand.CommandType = commandType;
                sqlCommand.Connection = ASSqlServer.sqlConnection;
                DefineSqlParameter(sqlCommand, parameterName, parameterValue);
                SqlDataAdapter sqlDataAdapter =
                    new SqlDataAdapter(sqlCommand);
                sqlDataAdapter.Fill(dataTable);
                sqlDataAdapter.Dispose();
            }
            catch (Exception ex)
            {
                errorMessage = "Error: " + ex.Message;
            }
            return dataTable;

        }
        #endregion

        #region GetDataRow

        public DataRow GetDataRow()
        {
            DataRow dataRow = null;
            try
            {
                DataTable dataTable = new DataTable();
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.CommandText = commandText;
                sqlCommand.CommandType = commandType;
                sqlCommand.Connection = ASSqlServer.sqlConnection;                
                SqlDataAdapter sqlDataAdapter =
                    new SqlDataAdapter(sqlCommand);
                sqlDataAdapter.Fill(dataTable);
                sqlDataAdapter.Dispose();
                if (dataTable.Rows.Count > 0)
                    dataRow = dataTable.Rows[0];
            }
            catch (Exception ex)
            {
                errorMessage = "Error: " + ex.Message;
            }
            return dataRow;

        }

        public DataRow GetDataRow(
            string parameterName, object parameterValue)
        {
            DataRow dataRow =null;
            try
            {
                DataTable dataTable = new DataTable();
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.CommandText = commandText;
                sqlCommand.CommandType = commandType;
                sqlCommand.Connection = ASSqlServer.sqlConnection;
                DefineSqlParameter(sqlCommand, parameterName, parameterValue);
                SqlDataAdapter sqlDataAdapter =
                    new SqlDataAdapter(sqlCommand);
                sqlDataAdapter.Fill(dataTable);
                sqlDataAdapter.Dispose();
                if (dataTable.Rows.Count > 0)
                    dataRow = dataTable.Rows[0];
            }
            catch (Exception ex)
            {
                errorMessage = "Error: " + ex.Message;
            }
            return dataRow;

        }
        public DataRow GetDataRow(
            string[] parameterName, object[] parameterValue)
        {
            DataRow dataRow = null;
            try
            {
                DataTable dataTable = new DataTable();
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.CommandText = commandText;
                sqlCommand.CommandType = commandType;
                sqlCommand.Connection = ASSqlServer.sqlConnection;
                DefineSqlParameter(sqlCommand, parameterName, parameterValue); //this is for adding multiple parameters for a sql command/ stored procedure
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
                sqlDataAdapter.Fill(dataTable);
                sqlDataAdapter.Dispose();
                if (dataTable.Rows.Count > 0)
                    dataRow = dataTable.Rows[0];
            }
            catch (Exception ex)
            {
                errorMessage = "Error: " + ex.Message;
            }
            return dataRow;

        }
         
        #endregion

    }
}
