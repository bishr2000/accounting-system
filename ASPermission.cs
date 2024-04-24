using System;
using System.Data;

namespace AccountSystem
{
    internal static class ASPermission
    {
        internal static string GetPermissionOnFunction(string functionName)
        {
            string permission = ""; // default value for permission
            ASDataProvider dataProvider = new ASDataProvider(); // establish a connection with the sql server if it's not already established

            dataProvider.CommandText = "udsPermissions;3";

            // specifying the command that will be executed by the ADO.NET (stored procedure/ query/ tablename)
            dataProvider.CommandType = CommandType.StoredProcedure; 

            // two arrays that specify some parameters about the username and the function that will be executing
            string[] parameterName = new string[2] { "@UserId", "@FunctionName" };
            object[] parameterValue = new object[2] { ASParameters.userName, functionName };

            DataRow dataRow = dataProvider.GetDataRow(parameterName, parameterValue);


            if(dataRow !=null)
            {
                 if (Convert.ToString(dataRow["Select"]) == "Y")
                    permission += "R";
                if (Convert.ToString(dataRow["Update"]) == "Y")
                    permission += "U";
                if (Convert.ToString(dataRow["Delete"]) == "Y")
                    permission += "D";
                if (Convert.ToString(dataRow["Insert"]) == "Y")
                    permission += "I";
                if (Convert.ToString(dataRow["Approval"]) == "Y")
                    permission += "A";
                if (Convert.ToString(dataRow["History"]) == "Y")
                    permission += "H";
                if (Convert.ToString(dataRow["Calculate"]) == "Y")
                    permission += "C";
                if (Convert.ToString(dataRow["ViewAll"]) == "Y")
                    permission += "S";
                 
            }
            if (permission == "") permission = "RIUDASHC"; 
            return permission;
        }
        internal static string[]  GetPermissionOnModule(string moduleName)
        {
            string[] permission = null;
            string userId = ASParameters.userName;
            permission = GetPermissionOnModule(userId, moduleName);  
            switch (moduleName)
            {
                case "GL":
                    if (permission == null)
                    {
                        permission = new string[8];
                        GetPermissionOnModule(permission);                         
                    }
                    break;
                case "AR":
                    if (permission == null)
                    {
                        permission = new string[5];
                        GetPermissionOnModule(permission);
                    }
                    break;
                case "AP":
                    if (permission == null)
                    {
                        permission = new string[4];
                        GetPermissionOnModule(permission);
                    }
                    break;
                case "IC":
                    if (permission == null)
                    {
                        permission = new string[9];
                        GetPermissionOnModule(permission);
                    }
                    break;
                case "DR":
                    if (permission == null)
                    {
                        permission = new string[6];
                        GetPermissionOnModule(permission);
                    }
                    break;
                case "CM":
                    if (permission == null)
                    {
                        permission = new string[3];
                        GetPermissionOnModule(permission);
                    }
                    break;
                case "UR":
                    if (permission == null)
                    {
                        permission = new string[3];
                        GetPermissionOnModule(permission);
                    }   
                    break;
            }
            
            return permission;
        }
        internal static void GetPermissionOnModule(string[] permission)
        {
            for (int i = 0; i < permission.Length; i++)
            {
                permission[i] = "Y";
            }
        }
        internal static string[] GetPermissionOnModule(string userName, string moduleName)
        {
            string[] permission = null;
            ASDataProvider dataProvider = new ASDataProvider();
            string procedureName = "";
            procedureName = GetProcedureName(moduleName);
            dataProvider.CommandText = procedureName;
            dataProvider.CommandType = CommandType.StoredProcedure;
            DataRow dataRow = dataProvider.GetDataRow("@UserId", userName);
            if (dataRow != null)
            {
                object[] obj = dataRow.ItemArray;
                permission = new string[obj.Length];
                for (int i = 0; i < obj.Length; i++)
                {
                    permission[i] = Convert.ToString(obj[i]);
                }
            }             
            return permission;
        }
        static string GetProcedureName(string moduleName)
        {
            string procedureName = "";
            switch (moduleName)
            {
                case "GL":
                    procedureName = "udsPermissions;4";
                    break;
                case "AR":
                    procedureName = "udsPermissions;5";
                    break;
                case "AP":
                    procedureName = "udsPermissions;6";
                    break;
                case "IC":
                    procedureName = "udsPermissions;7";
                    break;
                case "DR":
                    procedureName = "udsPermissions;8";
                    break;
                case "CM":
                    procedureName = "udsPermissions;9";
                    break;
                case "UR":
                    procedureName = "udsPermissions;10";
                    break;
            }
            return procedureName;
        }
    }
}
