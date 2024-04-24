using System;
using System.Data;

namespace AccountSystem
{
    static class ASParameters
    {
        internal static string welcomeMessage = "Jack Johnson";
        internal static bool windowsOnly = true;
        internal static string displayUserName = "";
        internal static string userName = "";
        internal static string password = "";

        internal static string currentMonth = "Oct/2007"; // this date is invalid and overdue
        
        internal static bool autoCreateId = true;
        internal static bool showToolTipOnDataGrid = true;

        internal static string functionalCurrency = "USD";        
        internal static string dateFormat = "dd/MMM/yyyy";
        internal static string doubleNumberFormat = "#,###.00";
        internal static string integerNumberFormat = "#,###";

        internal static void GetParameters()
        {
            
            ASDataProvider asDataProvider = new ASDataProvider();
            asDataProvider.CommandText = "udsParameters";
            asDataProvider.CommandType = CommandType.StoredProcedure;
            DataRow dataRow = asDataProvider.GetDataRow();
            ASParameters.currentMonth = Convert.ToString(dataRow["CurrentMonth"]);
            ASParameters.welcomeMessage = Convert.ToString(dataRow["WelcomeMessage"]);
            ASParameters.dateFormat = Convert.ToString(dataRow["DateFormat"]);
            ASParameters.doubleNumberFormat = Convert.ToString(dataRow["DoubleNumberFormat"]);
            ASParameters.integerNumberFormat = Convert.ToString(dataRow["IntegerNumberFormat"]);
            ASParameters.autoCreateId = Convert.ToBoolean(dataRow["AutoCreateId"]);
            ASParameters.showToolTipOnDataGrid = Convert.ToBoolean(dataRow["ShowToolTipOnDataGrid"]);
            
        }
    }
}
