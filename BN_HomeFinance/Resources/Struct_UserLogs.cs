using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace BN_HomeFinance.Resources
{
    public class UserLogs : UserLogs_Factory
    {
        public string UserID { get; set; }

        public string UserName { get; set; }

        public System.DateTime CreatedOn { get; set; }
        public Nullable<System.DateTime> LastLoggedIn  { get; set; }
    }

    public class UserLogs_Constants
    {
        #region Procedures
        public const string Proc_AddUserLogs = "AddUserLogs";
        public const string Proc_UpdateUserLogs = "UpdateUserLogs";
        public const string Proc_GetDormantUsers = "GetDormantUsers";
        public const string Proc_RemoveInactiveUsersFromUserLog = "RemoveInactiveUsersFromUserLog";
        public const string Proc_RemoveInactiveUsersFromUserData = "RemoveInactiveUsersFromUserData";
        #endregion

        #region Parameters
        public const string Par_userID = "@userid";
        public const string Par_username = "@username";
        public const string Par_createdon = "@createdon";
        public const string Par_lastloggedin = "@lastloggedin";
        #endregion

        #region Columns
        public const string Col_UserID = "UserID";
        public const string Col_UserName = "UserName";
        public const string Col_CreatedOn = "CreatedOn";
        public const string Col_LastLoggedIn = "LastLoggedIn";
        #endregion
    }

    public class UserLogs_Collection : UserLogs_Factory
    {
        private List<UserLogs> userlogs = new List<UserLogs>();
        public List<UserLogs> UserLogs
        {
            get { return userlogs; }
            set { userlogs = value; }
        }
        public int Count { get; set; }
    }

    public class UserLogs_Factory
    {
        public bool CreateUserLogs(UserLogs uObj)
        {
            bool b = false;

            if (uObj != null)
            {
                Helper.ExecuteSPScalarWithParameters(UserLogs_Constants.Proc_AddUserLogs, UserLogs_Constants.Par_userID,
                     uObj.UserID,UserLogs_Constants.Par_username, uObj.UserName);
            }
            return b;
        }

        public bool UpdateUserLogs(UserLogs uObj)
        {
            bool b = false;

            if (uObj != null)
            {
                Helper.ExecuteSPScalarWithParameters(UserLogs_Constants.Proc_UpdateUserLogs, UserLogs_Constants.Par_username,
                     uObj.UserName);
            }
            return b;
        }

        public UserLogs_Collection GetDormantUsers()
        {
            UserLogs_Collection objCollection = new UserLogs_Collection();
            objCollection.Count = 0;
            try
            {
                DataTable dt = Helper.ExecuteSPWithNoParameters(UserLogs_Constants.Proc_GetDormantUsers);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    UserLogs objUserLogs = new UserLogs();
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        DataRow dr = dt.Rows[i];
                        objUserLogs.UserID = dr[UserLogs_Constants.Col_UserID].ToString();
                        objUserLogs.UserName = dr[UserLogs_Constants.Col_UserName].ToString();
                        objUserLogs.CreatedOn = Convert.ToDateTime(dr[UserLogs_Constants.Col_CreatedOn].ToString());

                        if (string.IsNullOrEmpty(dr[UserLogs_Constants.Col_LastLoggedIn].ToString()))
                        {
                            objUserLogs.LastLoggedIn = null;
                        }
                        else 
                        {
                            objUserLogs.LastLoggedIn = Convert.ToDateTime(dr[UserLogs_Constants.Col_LastLoggedIn].ToString());
                        }
                        
                        
                        objCollection.UserLogs.Add(objUserLogs);
                        objCollection.Count++;
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }

            return objCollection;
        }

        public bool RemoveInactiveUsersFromUserLog()
        {
            bool b = false;

            try
            {
                Helper.ExecuteSPScalarWithoutParameters(UserLogs_Constants.Proc_RemoveInactiveUsersFromUserLog);
                b = true;
            }
            catch
            {
                throw;
            }

            return b;
        }
    }

}