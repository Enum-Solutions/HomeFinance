using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace BN_HomeFinance.Resources
{
    [Serializable]
    public class CurrentUser : User_Factory
    {
        public string UserID { get; set; }

        public string FullName { get; set; }

        public string LoginName { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }

        private UserTypes userType = new UserTypes();
        public UserTypes UserType
        {
            get { return userType; }
            set { userType = value; }
        }

        public bool IsAdmin { get; set; }

        public bool IsExists { get; set; }

        public string Status { get; set; }

        public string Address { get; set; }

        public string Contact { get; set; }

        public string Pin { get; set; }

        public string City { get; set; }

        public string Country { get; set; }

        public string Description { get; set; }

        public byte[] Picture { get; set; }
    }

    [Serializable]
    public class BNUser : User_Factory
    {
        public string UserID { get; set; }

        public string FullName { get; set; }

        public string LoginName { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }

        private UserTypes userType = new UserTypes();
        public UserTypes UserType
        {
            get { return userType; }
            set { userType = value; }
        }

        public bool IsAdmin { get; set; }

        public bool IsExists { get; set; }

        public string Description { get; set; }

        public string Status { get; set; }

        public bool HasUser { get; set; }

        public string Address { get; set; }

        public string Contact { get; set; }

        public string Pin { get; set; }

        public string City { get; set; }

        public string Country { get; set; }

        public string About { get; set; }

        private BNImage picture = new BNImage();
        public BNImage Picture
        {
            get { return picture; }
            set { picture = value; }
        }

        private BNImage bgpicture = new BNImage();
        public BNImage BGPicture
        {
            get { return bgpicture; }
            set { bgpicture = value; }
        }

        public BNUser()
        {
        }

        public BNUser(string UserID)
        {
            try
            {
                GetUserWithUserID(UserID);
            }
            catch
            {
                throw;
            }
        }
    }

    [Serializable]
    public class UserTypes : User_Factory
    {
        public string UserTypeID { get; set; }

        public string UserType { get; set; }
    }

    public class BNUserCollection : User_Factory
    {
        private List<BNUser> bnUser = new List<BNUser>();
        public List<BNUser> BNUser
        {
            get { return bnUser; }
            set { bnUser = value; }
        }

        public int Count { get; set; }

    }

    public class User_Constants
    {
        #region Procedures

        public const string Proc_CreateUser = "CreateUser";
        public const string Proc_GetUserWithLogin = "GetUserWithLogin";
        public const string Proc_GetUserWithUserID = "GetUserWithUserID";
        public const string Proc_ValidateCredentials = "ValidateCredentials";
        public const string Proc_GetConsumerTypeID = "GetConsumerTypeID";
        public const string Proc_DeleteUser = "DeleteUser";
        public const string Proc_CreateKey = "CreateKey";
        public const string Proc_DeleteKey = "DeleteKey";
        public const string Proc_ValidateEmail = "ValidateEmail";
        public const string Proc_ValidateUserWithEmail = "ValidateUserWithEmail";
        public const string Proc_UpdatePassword = "UpdatePassword";
        public const string Proc_GetUserTypes = "GET_USER_TYPES";
        public const string Proc_GetUsersByUserTypeID = "GetUsersByUserTypeID";
        public const string Proc_GetAllUserData = "GetAllUserData";
        public const string Proc_GetTopUsers = "GetTopUsers";
        public const string Proc_CheckUserExists = "CheckUserExists";
        public const string Proc_CheckEmailExist = "CheckEmailExist";
        public const string Proc_GetActiveVendorBuilders = "GetActiveVendorBuilders";
        public const string Proc_UpdateUserStatus = "UpdateUserStatus";
        public const string Proc_GetUserWithEmail = "GetUserWithEmail";

        #endregion

        #region Enums

        public enum UserStatus
        {
            Active,
            InActive,
            Locked
        }

        public enum UserType
        {
            Admin,
            Consumer,
            Builder,
            Vendor
        }

        #endregion

        #region Columns

        public const string Col_UserTypeID = "UserTypeID";
        public const string Col_UserType = "UserType";
        public const string Col_UserID = "UserID";
        public const string Col_LoginName = "LoginName";
        public const string Col_FullName = "FullName";
        public const string Col_Password = "Password";
        public const string Col_About = "About";
        public const string Col_Email = "Email";
        public const string Col_Status = "Status";
        public const string Col_Address = "Address";
        public const string Col_Contact = "Contact";
        public const string Col_Pin = "Pin";
        public const string Col_City = "City";
        public const string Col_Country = "Country";
        public const string Col_SpecialKey = "SpecialKey";

        #endregion

        #region Parameters

        public const string Par_UserTypeID = "@UserTypeID";
        public const string Par_UserType = "@UserType";
        public const string Par_UserID = "@UserID";
        public const string Par_LoginName = "@LoginName";
        public const string Par_FullName = "@FullName";
        public const string Par_About = "@About";
        public const string Par_Password = "@Password";
        public const string Par_Email = "@Email";
        public const string Par_Status = "@Status";
        public const string Par_Address = "@Address";
        public const string Par_Contact = "@Contact";
        public const string Par_Pin = "@Pin";
        public const string Par_City = "@City";
        public const string Par_Country = "@Country";
        public const string Par_Key = "@Key";

        #endregion
    }

    [Serializable]
    public class User_Factory
    {
        public BNUser GetUser(string Login)
        {
            BNUser user = new BNUser();

            try
            {
                DataTable dt = Helper.ExecuteSPWithParameters(User_Constants.Proc_GetUserWithLogin, User_Constants.Par_LoginName, Login);

                if (dt != null && dt.Rows.Count > 0)
                {
                    user.HasUser = true;
                    user.UserID = dt.Rows[0][User_Constants.Col_UserID].ToString();
                    user.FullName = dt.Rows[0][User_Constants.Col_FullName].ToString();
                    user.LoginName = dt.Rows[0][User_Constants.Col_LoginName].ToString();
                    user.Password = Helper.EncryptDecrypt(Helper.Encryption.Decrypt.ToString(), dt.Rows[0][User_Constants.Col_Password].ToString());
                    user.Email = dt.Rows[0][User_Constants.Col_Email].ToString();
                    user.UserType.UserTypeID = dt.Rows[0][User_Constants.Col_UserTypeID].ToString();
                    user.UserType.UserType = dt.Rows[0][User_Constants.Col_UserType].ToString();
                    user.Address = dt.Rows[0][User_Constants.Col_Address].ToString();
                    user.Contact = dt.Rows[0][User_Constants.Col_Contact].ToString();
                    user.Pin = dt.Rows[0][User_Constants.Col_Pin].ToString();
                    user.City = dt.Rows[0][User_Constants.Col_City].ToString();
                    user.Country = dt.Rows[0][User_Constants.Col_Country].ToString();
                    user.Status = dt.Rows[0][User_Constants.Col_Status].ToString();
                    user.About = dt.Rows[0][User_Constants.Col_About].ToString();

                    if (user.UserType.UserType == User_Constants.UserType.Admin.ToString())
                        user.IsAdmin = true;
                    else
                        user.IsAdmin = false;
                    try
                    {
                        user.Picture.ImageID = dt.Rows[0][BNImage_Constants.Col_ImageID].ToString();
                        user.Picture.ImageName = dt.Rows[0][BNImage_Constants.Col_ImageName].ToString();
                        user.Picture.ImageData = (byte[])dt.Rows[0][BNImage_Constants.Col_ImageData];
                    }
                    catch
                    {
                        user.Picture.ImageName = null;
                    }

                    if (user.Picture.ImageData != null)
                    {
                        string rarBase64Data = Convert.ToBase64String(user.Picture.ImageData);
                        user.Picture.ImageURL = string.Format("data:image/png;base64,{0}", rarBase64Data);
                    }
                    else
                    {
                        user.Picture.ImageURL = Helper.pc_NoUser;
                    }

                    try
                    {
                        user.BGPicture.ImageID = dt.Rows[0][BNImage_Constants.Col_CoverImageID].ToString();
                        user.BGPicture.ImageName = dt.Rows[0][BNImage_Constants.Col_CoverImageName].ToString();
                        user.BGPicture.ImageData = (byte[])dt.Rows[0][BNImage_Constants.Col_CoverImageData];
                    }
                    catch
                    {
                        user.BGPicture.ImageName = null;
                    }

                    if (user.BGPicture.ImageData != null)
                    {
                        string rarBase64Data = Convert.ToBase64String(user.BGPicture.ImageData);
                        user.BGPicture.ImageURL = string.Format("data:image/png;base64,{0}", rarBase64Data);
                    }
                    else
                    {
                        //user.BGPicture.ImageURL = Helper.pc_Background_User;
                    }

                    user.IsExists = true;
                }
            }
            catch
            {
                throw;
            }

            return user;
        }

        public BNUser GetUserWithUserID(string UserID)
        {
            BNUser user = new BNUser();

            try
            {
                DataTable dt = Helper.ExecuteSPWithParameters(User_Constants.Proc_GetUserWithUserID, User_Constants.Par_UserID, UserID);

                if (dt != null && dt.Rows.Count > 0)
                {
                    user.HasUser = true;
                    user.UserID = dt.Rows[0][User_Constants.Col_UserID].ToString();
                    user.FullName = dt.Rows[0][User_Constants.Col_FullName].ToString();
                    user.LoginName = dt.Rows[0][User_Constants.Col_LoginName].ToString();
                    user.Password = Helper.EncryptDecrypt(Helper.Encryption.Decrypt.ToString(), dt.Rows[0][User_Constants.Col_Password].ToString());
                    user.Email = dt.Rows[0][User_Constants.Col_Email].ToString();
                    user.UserType.UserTypeID = dt.Rows[0][User_Constants.Col_UserTypeID].ToString();
                    user.UserType.UserType = dt.Rows[0][User_Constants.Col_UserType].ToString();
                    user.Address = dt.Rows[0][User_Constants.Col_Address].ToString();
                    user.Contact = dt.Rows[0][User_Constants.Col_Contact].ToString();
                    user.Pin = dt.Rows[0][User_Constants.Col_Pin].ToString();
                    user.City = dt.Rows[0][User_Constants.Col_City].ToString();
                    user.Country = dt.Rows[0][User_Constants.Col_Country].ToString();
                    user.Status = dt.Rows[0][User_Constants.Col_Status].ToString();
                    user.About = dt.Rows[0][User_Constants.Col_About].ToString();

                    if (user.UserType.UserType == User_Constants.UserType.Admin.ToString())
                        user.IsAdmin = true;
                    else
                        user.IsAdmin = false;

                    try
                    {
                        user.Picture.ImageID = dt.Rows[0][BNImage_Constants.Col_ImageID].ToString();
                        user.Picture.ImageName = dt.Rows[0][BNImage_Constants.Col_ImageName].ToString();
                        user.Picture.ImageData = (byte[])dt.Rows[0][BNImage_Constants.Col_ImageData];
                    }
                    catch
                    {
                        user.Picture.ImageName = null;
                    }

                    if (user.Picture.ImageData != null)
                    {
                        string rarBase64Data = Convert.ToBase64String(user.Picture.ImageData);
                        user.Picture.ImageURL = string.Format("data:image/png;base64,{0}", rarBase64Data);
                    }
                    else
                    {
                        user.Picture.ImageURL = Helper.pc_NoUser;
                    }

                    try
                    {
                        user.BGPicture.ImageID = dt.Rows[0][BNImage_Constants.Col_CoverImageID].ToString();
                        user.BGPicture.ImageName = dt.Rows[0][BNImage_Constants.Col_CoverImageName].ToString();
                        user.BGPicture.ImageData = (byte[])dt.Rows[0][BNImage_Constants.Col_CoverImageData];
                    }
                    catch
                    {
                        user.BGPicture.ImageName = null;
                    }

                    if (user.BGPicture.ImageData != null)
                    {
                        string rarBase64Data = Convert.ToBase64String(user.BGPicture.ImageData);
                        user.BGPicture.ImageURL = string.Format("data:image/png;base64,{0}", rarBase64Data);
                    }
                    else
                    {
                        user.BGPicture.ImageURL = Helper.pc_Background_User;
                    }

                    user.IsExists = true;
                }
            }
            catch
            {
                throw;
            }

            return user;
        }

        public BNUser GetUserWithUserLogin(string Login)
        {
            BNUser user = new BNUser();

            try
            {
                DataTable dt = Helper.ExecuteSPWithParameters(User_Constants.Proc_GetUserWithLogin, User_Constants.Par_LoginName, Login);

                if (dt != null && dt.Rows.Count > 0)
                {
                    user.HasUser = true;
                    user.UserID = dt.Rows[0][User_Constants.Col_UserID].ToString();
                    user.FullName = dt.Rows[0][User_Constants.Col_FullName].ToString();
                    user.LoginName = dt.Rows[0][User_Constants.Col_LoginName].ToString();
                    user.Password = Helper.EncryptDecrypt(Helper.Encryption.Decrypt.ToString(), dt.Rows[0][User_Constants.Col_Password].ToString());
                    user.Email = dt.Rows[0][User_Constants.Col_Email].ToString();
                    user.UserType.UserTypeID = dt.Rows[0][User_Constants.Col_UserTypeID].ToString();
                    user.UserType.UserType = dt.Rows[0][User_Constants.Col_UserType].ToString();
                    user.Address = dt.Rows[0][User_Constants.Col_Address].ToString();
                    user.Contact = dt.Rows[0][User_Constants.Col_Contact].ToString();
                    user.Pin = dt.Rows[0][User_Constants.Col_Pin].ToString();
                    user.City = dt.Rows[0][User_Constants.Col_City].ToString();
                    user.Country = dt.Rows[0][User_Constants.Col_Country].ToString();
                    user.Status = dt.Rows[0][User_Constants.Col_Status].ToString();
                    user.About = dt.Rows[0][User_Constants.Col_About].ToString();

                    if (user.UserType.UserType == User_Constants.UserType.Admin.ToString())
                        user.IsAdmin = true;
                    else
                        user.IsAdmin = false;

                    try
                    {
                        user.Picture.ImageID = dt.Rows[0][BNImage_Constants.Col_ImageID].ToString();
                        user.Picture.ImageName = dt.Rows[0][BNImage_Constants.Col_ImageName].ToString();
                        user.Picture.ImageData = (byte[])dt.Rows[0][BNImage_Constants.Col_ImageData];
                    }
                    catch
                    {
                        user.Picture.ImageName = null;
                    }

                    if (user.Picture.ImageData != null)
                    {
                        string rarBase64Data = Convert.ToBase64String(user.Picture.ImageData);
                        user.Picture.ImageURL = string.Format("data:image/png;base64,{0}", rarBase64Data);
                    }
                    else
                    {
                        user.Picture.ImageURL = Helper.pc_NoUser;
                    }

                    try
                    {
                        user.BGPicture.ImageID = dt.Rows[0][BNImage_Constants.Col_CoverImageID].ToString();
                        user.BGPicture.ImageName = dt.Rows[0][BNImage_Constants.Col_CoverImageName].ToString();
                        user.BGPicture.ImageData = (byte[])dt.Rows[0][BNImage_Constants.Col_CoverImageData];
                    }
                    catch
                    {
                        user.BGPicture.ImageName = null;
                    }

                    if (user.BGPicture.ImageData != null)
                    {
                        string rarBase64Data = Convert.ToBase64String(user.BGPicture.ImageData);
                        user.BGPicture.ImageURL = string.Format("data:image/png;base64,{0}", rarBase64Data);
                    }
                    else
                    {
                        user.BGPicture.ImageURL = Helper.pc_Background_User;
                    }

                    user.IsExists = true;
                }
            }
            catch
            {
                throw;
            }

            return user;
        }


        public BNUser GetUserWithEmail(string Email)
        {
            BNUser user = new BNUser();

            try
            {
                DataTable dt = Helper.ExecuteSPWithParameters(User_Constants.Proc_GetUserWithEmail, User_Constants.Par_Email, Email);

                if (dt != null && dt.Rows.Count > 0)
                {
                    user.HasUser = true;
                    user.UserID = dt.Rows[0][User_Constants.Col_UserID].ToString();
                    user.FullName = dt.Rows[0][User_Constants.Col_FullName].ToString();
                    user.LoginName = dt.Rows[0][User_Constants.Col_LoginName].ToString();
                    user.Password = Helper.EncryptDecrypt(Helper.Encryption.Decrypt.ToString(), dt.Rows[0][User_Constants.Col_Password].ToString());
                    user.Email = dt.Rows[0][User_Constants.Col_Email].ToString();
                    user.UserType.UserTypeID = dt.Rows[0][User_Constants.Col_UserTypeID].ToString();
                    user.UserType.UserType = dt.Rows[0][User_Constants.Col_UserType].ToString();
                    user.Address = dt.Rows[0][User_Constants.Col_Address].ToString();
                    user.Contact = dt.Rows[0][User_Constants.Col_Contact].ToString();
                    user.Pin = dt.Rows[0][User_Constants.Col_Pin].ToString();
                    user.City = dt.Rows[0][User_Constants.Col_City].ToString();
                    user.Country = dt.Rows[0][User_Constants.Col_Country].ToString();
                    user.Status = dt.Rows[0][User_Constants.Col_Status].ToString();
                    user.About = dt.Rows[0][User_Constants.Col_About].ToString();

                    if (user.UserType.UserType == User_Constants.UserType.Admin.ToString())
                        user.IsAdmin = true;
                    else
                        user.IsAdmin = false;

                    try
                    {
                        user.Picture.ImageID = dt.Rows[0][BNImage_Constants.Col_ImageID].ToString();
                        user.Picture.ImageName = dt.Rows[0][BNImage_Constants.Col_ImageName].ToString();
                        user.Picture.ImageData = (byte[])dt.Rows[0][BNImage_Constants.Col_ImageData];
                    }
                    catch
                    {
                        user.Picture.ImageName = null;
                    }

                    if (user.Picture.ImageData != null)
                    {
                        string rarBase64Data = Convert.ToBase64String(user.Picture.ImageData);
                        user.Picture.ImageURL = string.Format("data:image/png;base64,{0}", rarBase64Data);
                    }
                    else
                    {
                        user.Picture.ImageURL = Helper.pc_NoUser;
                    }

                    try
                    {
                        user.BGPicture.ImageID = dt.Rows[0][BNImage_Constants.Col_CoverImageID].ToString();
                        user.BGPicture.ImageName = dt.Rows[0][BNImage_Constants.Col_CoverImageName].ToString();
                        user.BGPicture.ImageData = (byte[])dt.Rows[0][BNImage_Constants.Col_CoverImageData];
                    }
                    catch
                    {
                        user.BGPicture.ImageName = null;
                    }

                    if (user.BGPicture.ImageData != null)
                    {
                        string rarBase64Data = Convert.ToBase64String(user.BGPicture.ImageData);
                        user.BGPicture.ImageURL = string.Format("data:image/png;base64,{0}", rarBase64Data);
                    }
                    else
                    {
                        user.BGPicture.ImageURL = Helper.pc_Background_User;
                    }

                    user.IsExists = true;
                }
            }
            catch
            {
                throw;
            }

            return user;
        }


        public bool CreateUser(BNUser user)
        {
            bool b = false;

            try
            {
                if (user.Picture.ImageData != null)
                    user.Picture.ImageID = (user.Picture.SaveImage(user.Picture)).ImageID;
                else
                    user.Picture.ImageID = "0";

                if (user.BGPicture.ImageData != null)
                    user.BGPicture.ImageID = (user.BGPicture.SaveImage(user.BGPicture)).ImageID;
                else
                    user.BGPicture.ImageID = "0";

                string UserID = Helper.ExecuteSPScalarWithParameters(User_Constants.Proc_CreateUser, User_Constants.Par_UserID, new Guid(user.UserID),
                    User_Constants.Par_FullName, user.FullName, User_Constants.Par_LoginName, user.LoginName,
                    User_Constants.Par_Password, Helper.EncryptDecrypt(Helper.Encryption.Encrypt.ToString(), user.Password), User_Constants.Par_Email, user.Email,
                    User_Constants.Par_UserTypeID, user.UserType.UserTypeID, User_Constants.Par_Status, user.Status,
                    User_Constants.Par_Address, user.Address, User_Constants.Par_Contact, user.Contact, User_Constants.Par_Pin, user.Pin,
                    User_Constants.Par_City, user.City, User_Constants.Par_Country, user.Country, BNImage_Constants.Par_ImageID, user.Picture.ImageID.ToString(),
                    BNImage_Constants.Par_CoverImageID, user.BGPicture.ImageID.ToString(), User_Constants.Par_About, user.About);

                if (UserID != null && UserID != "")
                {
                    b = true;
                }
            }
            catch
            {
                throw;
            }

            return b;
        }

        public bool ValidateCredentials(string UserName, string Password)
        {
            bool b = false;

            try
            {
                DataTable dt = Helper.ExecuteSPWithParameters(User_Constants.Proc_ValidateCredentials, User_Constants.Par_LoginName, UserName,
                    User_Constants.Par_Password, Helper.EncryptDecrypt(Helper.Encryption.Encrypt.ToString(), Password));

                if (dt != null && dt.Rows.Count > 0)
                {
                    b = true;
                }
            }
            catch
            {
                throw;
            }

            return b;
        }

        public bool DeleteUser(string UserID)
        {
            bool b = false;

            try
            {
                Helper.ExecuteSPScalarWithParameters(User_Constants.Proc_DeleteUser,
                    User_Constants.Par_UserID, UserID);

                if (UserID != null && UserID != "")
                {
                    b = true;
                }
            }
            catch
            {
                throw;
            }

            return b;
        }

        public bool CheckLoginExists(string login)
        {
            bool b = false;

            try
            {
                DataTable dt = Helper.ExecuteSPWithParameters(User_Constants.Proc_CheckUserExists,
                   User_Constants.Par_LoginName, login);

                if (dt != null && dt.Rows.Count > 0)
                {
                    b = true;
                }
            }
            catch
            {
                throw;
            }

            return b;
        }

        public bool CheckEmailExists(string Email)
        {
            bool b = false;

            try
            {
                DataTable dt = Helper.ExecuteSPWithParameters(User_Constants.Proc_CheckEmailExist,
                   User_Constants.Par_Email, Email);

                if (dt != null && dt.Rows.Count > 0)
                {
                    b = true;
                }
            }
            catch
            {
                throw;
            }

            return b;
        }

        public bool ResetPasswordApply(string Email)
        {
            bool b = false;

            try
            {
                DataTable dt = Helper.ExecuteSPWithParameters(User_Constants.Proc_ValidateUserWithEmail, User_Constants.Par_Email, Email);

                if (dt != null && dt.Rows.Count > 0)
                {
                    string Gui = Helper.ExecuteSPScalarWithParameters(User_Constants.Proc_CreateKey, User_Constants.Par_Email, Email, User_Constants.Par_Key, Guid.NewGuid().ToString());

                    Helper.SendEmail();
                    b = true;
                }
                else
                {

                }
            }
            catch
            {
                throw;
            }

            return b;
        }

        public bool ValidateKey(string Email, string Key)
        {
            bool b = false;

            try
            {
                DataTable dt = Helper.ExecuteSPWithParameters(User_Constants.Proc_ValidateEmail, User_Constants.Par_Email, Email);

                if (dt != null && dt.Rows.Count > 0)
                {
                    if (Key == dt.Rows[0][User_Constants.Col_SpecialKey].ToString())
                    {
                        b = true;
                    }
                }
            }
            catch (Exception ex)
            {

            }

            return b;
        }

        public DataTable GetAllUserTypesDataTable()
        {
            DataTable dt = new DataTable();

            try
            {
                dt = Helper.ExecuteSPWithNoParameters(User_Constants.Proc_GetUserTypes);
            }
            catch
            {
                throw;
            }
            return dt;
        }

        public DataTable GetAllUsersDatatable()
        {
            DataTable dt = new DataTable();

            try
            {
                dt = Helper.ExecuteSPWithNoParameters(User_Constants.Proc_GetAllUserData);
            }
            catch
            {
                throw;
            }
            return dt;
        }

        public BNUserCollection GetUsersByUserTypeID(string UserTypeID)
        {
            BNUserCollection objCollection = new BNUserCollection();

            objCollection.Count = 0;

            try
            {
                DataTable dt = Helper.ExecuteSPWithParameters(User_Constants.Proc_GetUsersByUserTypeID, User_Constants.Par_UserTypeID, UserTypeID);

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    BNUser objUser = new BNUser();

                    if (dt != null && dt.Rows.Count > 0)
                    {
                        DataRow dr = dt.Rows[i];
                        objUser.UserID = dr[User_Constants.Col_UserID].ToString();
                        objUser.FullName = dr[User_Constants.Col_FullName].ToString();
                        objUser.Email = dr[User_Constants.Col_Email].ToString();
                        objUser.Status = dr[User_Constants.Col_Status].ToString();
                        objUser.Contact = dr[User_Constants.Col_Contact].ToString();
                        objUser.Address = dr[User_Constants.Col_Address].ToString();
                        //objUser.Description = dr[User_Constants.Col_About].ToString();
                        objUser.City = dr[User_Constants.Col_City].ToString();
                        objUser.Country = dr[User_Constants.Col_Country].ToString();
                        objUser.Picture.ImageID = Convert.ToString(dr[BNImage_Constants.Col_ImageID]);
                        objUser.UserType.UserTypeID = dr[User_Constants.Col_UserTypeID].ToString();
                        objUser.UserType.UserType = dr[User_Constants.Col_UserType].ToString();

                        objCollection.BNUser.Add(objUser);
                        objCollection.Count++;
                    }
                }
            }
            catch
            {
                throw;
            }

            return objCollection;
        }

        public BNUserCollection GetTopUser(string Type)
        {
            BNUserCollection collection = new BNUserCollection();
            collection.Count = 0;

            try
            {
                DataTable dt = Helper.ExecuteSPWithParameters(User_Constants.Proc_GetTopUsers, User_Constants.Par_UserType, Type);

                if (dt != null && dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {

                        BNUser user = new BNUser();
                        user.HasUser = true;
                        user.UserID = dt.Rows[i][User_Constants.Col_UserID].ToString();
                        user.FullName = dt.Rows[i][User_Constants.Col_FullName].ToString();
                        user.Contact = dt.Rows[i][User_Constants.Col_Contact].ToString();
                        user.Email = dt.Rows[i][User_Constants.Col_Email].ToString();
                        user.City = dt.Rows[i][User_Constants.Col_City].ToString();
                        user.Country = dt.Rows[i][User_Constants.Col_Country].ToString();
                        user.UserType.UserTypeID = dt.Rows[i][User_Constants.Col_UserTypeID].ToString();

                        try
                        {
                            user.Picture.ImageID = dt.Rows[i][BNImage_Constants.Col_ImageID].ToString();
                            user.Picture.ImageName = dt.Rows[i][BNImage_Constants.Col_ImageName].ToString();
                            user.Picture.ImageData = (byte[])dt.Rows[i][BNImage_Constants.Col_ImageData];
                        }
                        catch
                        {
                            user.Picture.ImageName = null;
                        }

                        if (user.Picture.ImageData != null)
                        {
                            string rarBase64Data = Convert.ToBase64String(user.Picture.ImageData);
                            user.Picture.ImageURL = string.Format("data:image/png;base64,{0}", rarBase64Data);
                        }
                        else
                        {
                            user.Picture.ImageURL = Helper.pc_NoUser;
                        }

                        user.IsExists = true;

                        collection.BNUser.Add(user);
                        collection.Count++;
                    }
                }
            }
            catch
            {
                throw;
            }
            return collection;
        }

        public bool UpdateUserStatus(string LoginName, string Status)
        {
            bool b = false;

            try
            {
                Helper.ExecuteSPScalarWithParameters(User_Constants.Proc_UpdateUserStatus, User_Constants.Par_LoginName, LoginName, User_Constants.Par_Status, Status);
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