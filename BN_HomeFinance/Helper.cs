using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BN_HomeFinance.Resources;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Security.Cryptography;
using System.Configuration;
using System.Net;
using System.IO;
using System.Web.Script.Serialization;

namespace BN_HomeFinance
{
    public class Helper
    {
        #region Google Authentication

        public class Tokenclass
        {
            public string access_token
            {
                get;
                set;
            }
            public string token_type
            {
                get;
                set;
            }
            public int expires_in
            {
                get;
                set;
            }
            public string refresh_token
            {
                get;
                set;
            }
        }

        public class Userclass
        {
            public string id
            {
                get;
                set;
            }
            public string name
            {
                get;
                set;
            }
            public string given_name
            {
                get;
                set;
            }
            public string family_name
            {
                get;
                set;
            }
            public string link
            {
                get;
                set;
            }
            public string picture
            {
                get;
                set;
            }
            public string gender
            {
                get;
                set;
            }
            public string locale
            {
                get;
                set;
            }
        }

        public static BNUser GetToken(string code)
        {
            string clientID = ConfigurationManager.AppSettings[Keys.ClientID.ToString()].ToString();
            string clientSecret = ConfigurationManager.AppSettings[Keys.ClientSecret.ToString()].ToString();
            string url = "https://accounts.google.com/o/oauth2/token";
            string poststring = "grant_type=authorization_code&code=" + HttpUtility.UrlEncode(code) + "&client_id=" + clientID + "&client_secret=" + clientSecret + "&redirect_uri=" + HttpUtility.UrlEncode(ConfigurationManager.AppSettings[Helper.Keys.SiteURL.ToString()].ToString());

            var request = (HttpWebRequest)WebRequest.Create(url + "?" + poststring);
            request.ContentType = "application/x-www-form-urlencoded";
            request.Method = "POST";
            UTF8Encoding utfenc = new UTF8Encoding();
            byte[] bytes = utfenc.GetBytes(poststring);
            Stream outputstream = null;

            try
            {
                request.ContentLength = bytes.Length;
                outputstream = request.GetRequestStream();
                outputstream.Write(bytes, 0, bytes.Length);
            }
            catch { }
            var response = (HttpWebResponse)request.GetResponse();
            var streamReader = new StreamReader(response.GetResponseStream());
            string responseFromServer = streamReader.ReadToEnd();
            JavaScriptSerializer js = new JavaScriptSerializer();
            Tokenclass obj = js.Deserialize<Tokenclass>(responseFromServer);
            return GetuserProfile(obj.access_token);
        }

        public static BNUser GetuserProfile(string accesstoken)
        {
            string url = "https://www.googleapis.com/oauth2/v1/userinfo?alt=json&access_token=" + accesstoken + "";
            WebRequest request = WebRequest.Create(url);
            request.Credentials = CredentialCache.DefaultCredentials;
            WebResponse response = request.GetResponse();
            Stream dataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(dataStream);
            string responseFromServer = reader.ReadToEnd();
            reader.Close();
            response.Close();
            JavaScriptSerializer js = new JavaScriptSerializer();
            Userclass userinfo = js.Deserialize<Userclass>(responseFromServer);

            BNUser user = new BNUser();
            
            if (userinfo.id != null && userinfo.id != "")
            {
                user.LoginName = userinfo.id;
                user.FullName = userinfo.name;
            }
            return user;
        }

        #endregion
        #region Methods

        //No Parameters Procedures

        public static DataTable ExecuteSPWithNoParameters(string Procedure)
        {
            DataTable dt = new DataTable();

            try
            {
                using (SqlConnection con = new SqlConnection(ConStr))
                {
                    using (SqlCommand cmd = new SqlCommand(Procedure, con))
                    {
                        SqlDataAdapter da = new SqlDataAdapter(cmd);

                        if (con.State == ConnectionState.Closed)
                            con.Open();

                        da.Fill(dt);

                        con.Close();
                    }
                }
            }
            catch
            {
                throw;
            }

            return dt;
        }

        public static DataSet ExecuteSPWithNoParametersDataSet(string Procedure)
        {
            DataSet ds = new DataSet();

            try
            {
                using (SqlConnection con = new SqlConnection(ConStr))
                {
                    using (SqlCommand cmd = new SqlCommand(Procedure, con))
                    {
                        SqlDataAdapter da = new SqlDataAdapter(cmd);

                        if (con.State == ConnectionState.Closed)
                            con.Open();

                        da.Fill(ds);

                        con.Close();
                    }
                }
            }
            catch
            {
                throw;
            }

            return ds;
        }

        #region Procedure with Parameters

        public static DataTable ExecuteSPWithParameters(string Procedure, string Par_1, string Val_1)
        {
            DataTable dt = new DataTable();

            try
            {
                using (SqlConnection con = new SqlConnection(ConStr))
                {
                    using (SqlCommand cmd = new SqlCommand(Procedure, con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue(Par_1, Val_1);

                        SqlDataAdapter da = new SqlDataAdapter(cmd);

                        if (con.State == ConnectionState.Closed)
                            con.Open();

                        da.Fill(dt);

                        con.Close();
                    }
                }
            }
            catch
            {
                throw;
            }

            return dt;
        }

        public static DataSet ExecuteSPWithParametersWithDataSet(string Procedure, string Par_1, string Val_1)
        {
            DataSet ds = new DataSet();

            try
            {
                using (SqlConnection con = new SqlConnection(ConStr))
                {
                    using (SqlCommand cmd = new SqlCommand(Procedure, con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        SqlDataAdapter da = new SqlDataAdapter(cmd);

                        cmd.Parameters.AddWithValue(Par_1, Val_1);

                        if (con.State == ConnectionState.Closed)
                            con.Open();

                        da.Fill(ds);

                        con.Close();
                    }
                }
            }
            catch
            {
                throw;
            }

            return ds;
        }

        public static DataTable ExecuteSPWithParameters(string Procedure, string Par_1, string Val_1, string Par_2, string Val_2)
        {
            DataTable dt = new DataTable();

            try
            {
                using (SqlConnection con = new SqlConnection(ConStr))
                {
                    using (SqlCommand cmd = new SqlCommand(Procedure, con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        SqlDataAdapter da = new SqlDataAdapter(cmd);

                        cmd.Parameters.AddWithValue(Par_1, Val_1);
                        cmd.Parameters.AddWithValue(Par_2, Val_2);

                        if (con.State == ConnectionState.Closed)
                            con.Open();

                        da.Fill(dt);

                        con.Close();
                    }
                }
            }
            catch
            {
                throw;
            }

            return dt;
        }

        public static DataTable ExecuteSPWithParameters(string Procedure, string Par_1, string Val_1, string Par_2, string Val_2, string Par_3, string Val_3)
        {
            DataTable dt = new DataTable();

            try
            {
                using (SqlConnection con = new SqlConnection(ConStr))
                {
                    using (SqlCommand cmd = new SqlCommand(Procedure, con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        SqlDataAdapter da = new SqlDataAdapter(cmd);

                        cmd.Parameters.AddWithValue(Par_1, Val_1);
                        cmd.Parameters.AddWithValue(Par_2, Val_2);
                        cmd.Parameters.AddWithValue(Par_3, Val_3);

                        if (con.State == ConnectionState.Closed)
                            con.Open();

                        da.Fill(dt);

                        con.Close();
                    }
                }
            }
            catch
            {
                throw;
            }

            return dt;
        }

        public static DataTable ExecuteSPWithParameters(string Procedure, string Par_1, string Val_1, string Par_2, string Val_2, string Par_3, string Val_3, string Par_4, string Val_4)
        {
            DataTable dt = new DataTable();

            try
            {
                using (SqlConnection con = new SqlConnection(ConStr))
                {
                    using (SqlCommand cmd = new SqlCommand(Procedure, con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        SqlDataAdapter da = new SqlDataAdapter(cmd);

                        cmd.Parameters.AddWithValue(Par_1, Val_1);
                        cmd.Parameters.AddWithValue(Par_2, Val_2);
                        cmd.Parameters.AddWithValue(Par_3, Val_3);
                        cmd.Parameters.AddWithValue(Par_4, Val_4);

                        if (con.State == ConnectionState.Closed)
                            con.Open();

                        da.Fill(dt);

                        con.Close();
                    }
                }
            }
            catch
            {
                throw;
            }

            return dt;
        }

        public static DataTable ExecuteSPWithParameters(string Procedure, string Par_1, string Val_1, string Par_2, string Val_2,
            string Par_3, string Val_3, string Par_4, string Val_4, string Par_5, string Val_5, string Par_6, string Val_6,
            string Par_7, string Val_7, string Par_8, string Val_8)
        {
            DataTable dt = new DataTable();

            try
            {
                using (SqlConnection con = new SqlConnection(ConStr))
                {
                    using (SqlCommand cmd = new SqlCommand(Procedure, con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        SqlDataAdapter da = new SqlDataAdapter(cmd);

                        cmd.Parameters.AddWithValue(Par_1, Val_1);
                        cmd.Parameters.AddWithValue(Par_2, Val_2);
                        cmd.Parameters.AddWithValue(Par_3, Val_3);
                        cmd.Parameters.AddWithValue(Par_4, Val_4);
                        cmd.Parameters.AddWithValue(Par_5, Val_5);
                        cmd.Parameters.AddWithValue(Par_6, Val_6);
                        cmd.Parameters.AddWithValue(Par_7, Val_7);
                        cmd.Parameters.AddWithValue(Par_8, Val_8);

                        if (con.State == ConnectionState.Closed)
                            con.Open();

                        da.Fill(dt);

                        con.Close();
                    }
                }
            }
            catch
            {
                throw;
            }

            return dt;
        }
        #endregion

        #region Procedure Scalar

        public static string ExecuteSPScalarWithoutParameters(string Procedure)
        {
            string returnVal = string.Empty;

            try
            {
                using (SqlConnection con = new SqlConnection(ConStr))
                {
                    using (SqlCommand cmd = new SqlCommand(Procedure, con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        if (con.State == ConnectionState.Closed)
                            con.Open();

                        cmd.ExecuteNonQuery();

                        con.Close();
                    }
                }
            }
            catch
            {
                throw;
            }

            return returnVal;
        }

        public static string ExecuteSPScalarWithParameters(string Procedure, string Par_1, string Val_1)
        {
            string returnVal = string.Empty;

            try
            {
                using (SqlConnection con = new SqlConnection(ConStr))
                {
                    using (SqlCommand cmd = new SqlCommand(Procedure, con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue(Par_1, Val_1);

                        if (con.State == ConnectionState.Closed)
                            con.Open();

                        cmd.ExecuteNonQuery();

                        con.Close();
                    }
                }
            }
            catch
            {
                throw;
            }

            return returnVal;
        }

        public static string ExecuteSPScalarWithParameters(string Procedure, string Par_1, string Val_1, string Par_2, string Val_2)
        {
            string returnVal = string.Empty;

            try
            {
                using (SqlConnection con = new SqlConnection(ConStr))
                {
                    using (SqlCommand cmd = new SqlCommand(Procedure, con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        SqlDataAdapter da = new SqlDataAdapter(cmd);

                        cmd.Parameters.AddWithValue(Par_1, Val_1);
                        cmd.Parameters.AddWithValue(Par_2, Val_2);

                        if (con.State == ConnectionState.Closed)
                            con.Open();

                        returnVal = (string)cmd.ExecuteScalar();

                        con.Close();
                    }
                }
            }
            catch
            {
                throw;
            }

            return returnVal;
        }

        public static int ExecuteSPScalarWithParametersInt(string Procedure, string Par_1, string Val_1, string Par_2, string Val_2)
        {
            int returnVal = 0;

            try
            {
                using (SqlConnection con = new SqlConnection(ConStr))
                {
                    using (SqlCommand cmd = new SqlCommand(Procedure, con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        SqlDataAdapter da = new SqlDataAdapter(cmd);

                        cmd.Parameters.AddWithValue(Par_1, Val_1);
                        cmd.Parameters.AddWithValue(Par_2, Val_2);

                        if (con.State == ConnectionState.Closed)
                            con.Open();

                        returnVal = (int)cmd.ExecuteScalar();

                        con.Close();
                    }
                }
            }
            catch
            {
                throw;
            }

            return returnVal;
        }

        public static int ExecuteSPScalarWithParametersIntChat(string Procedure, string Par_1, string Val_1, string Par_2, Guid Val_2)
        {
            int returnVal = 0;

            try
            {
                using (SqlConnection con = new SqlConnection(ConStr))
                {
                    using (SqlCommand cmd = new SqlCommand(Procedure, con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        SqlDataAdapter da = new SqlDataAdapter(cmd);

                        cmd.Parameters.AddWithValue(Par_1, Val_1);
                        cmd.Parameters.AddWithValue(Par_2, Val_2);

                        if (con.State == ConnectionState.Closed)
                            con.Open();

                        returnVal = (int)cmd.ExecuteScalar();

                        con.Close();
                    }
                }
            }
            catch
            {
                throw;
            }

            return returnVal;
        }

        public static string ExecuteSPScalarWithParameters(string Procedure, string Par_1, string Val_1, string Par_2, string Val_2, string Par_3, Guid Val_3)
        {
            string returnVal = string.Empty;

            try
            {
                using (SqlConnection con = new SqlConnection(ConStr))
                {
                    using (SqlCommand cmd = new SqlCommand(Procedure, con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        SqlDataAdapter da = new SqlDataAdapter(cmd);

                        cmd.Parameters.AddWithValue(Par_1, Val_1);
                        cmd.Parameters.AddWithValue(Par_2, Val_2);
                        cmd.Parameters.AddWithValue(Par_3, Val_3);

                        if (con.State == ConnectionState.Closed)
                            con.Open();

                        int val = ((int)cmd.ExecuteScalar());

                        returnVal = val.ToString();
                        con.Close();
                    }
                }
            }
            catch
            {
                throw;
            }

            return returnVal;
        }

        public static int ExecuteSPScalarWithParameters(string Procedure, string Par_1, string Val_1, string Par_2, byte[] Val_2)
        {
            int returnVal;

            try
            {
                using (SqlConnection con = new SqlConnection(ConStr))
                {
                    using (SqlCommand cmd = new SqlCommand(Procedure, con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        SqlDataAdapter da = new SqlDataAdapter(cmd);

                        cmd.Parameters.AddWithValue(Par_1, Val_1);
                        cmd.Parameters.AddWithValue(Par_2, Val_2);

                        if (con.State == ConnectionState.Closed)
                            con.Open();

                        returnVal = (int)cmd.ExecuteScalar();

                        con.Close();
                    }
                }
            }
            catch
            {
                throw;
            }

            return returnVal;
        }

        public static string ExecuteSPScalarWithParameters(string Procedure, string Par_1, string Val_1, string Par_2, string Val_2, string Par_3, string Val_3)
        {
            string returnVal = string.Empty;

            try
            {
                using (SqlConnection con = new SqlConnection(ConStr))
                {
                    using (SqlCommand cmd = new SqlCommand(Procedure, con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        SqlDataAdapter da = new SqlDataAdapter(cmd);

                        cmd.Parameters.AddWithValue(Par_1, Val_1);
                        cmd.Parameters.AddWithValue(Par_2, Val_2);
                        cmd.Parameters.AddWithValue(Par_3, Val_3);

                        if (con.State == ConnectionState.Closed)
                            con.Open();

                        int val = ((int)cmd.ExecuteScalar());

                        returnVal = val.ToString();
                        con.Close();
                    }
                }
            }
            catch
            {
                throw;
            }

            return returnVal;
        }

        public static string ExecuteSPScalarWithParameters(string Procedure, string Par_1, string Val_1, string Par_2, string Val_2, string Par_3, string Val_3, string Par_4, string Val_4)
        {
            string returnVal = string.Empty;

            try
            {
                using (SqlConnection con = new SqlConnection(ConStr))
                {
                    using (SqlCommand cmd = new SqlCommand(Procedure, con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        SqlDataAdapter da = new SqlDataAdapter(cmd);

                        cmd.Parameters.AddWithValue(Par_1, Val_1);
                        cmd.Parameters.AddWithValue(Par_2, Val_2);
                        cmd.Parameters.AddWithValue(Par_3, Val_3);
                        cmd.Parameters.AddWithValue(Par_4, Val_4);

                        if (con.State == ConnectionState.Closed)
                            con.Open();

                        cmd.ExecuteNonQuery();

                        con.Close();
                    }
                }
            }
            catch
            {
                throw;
            }

            return returnVal;
        }

        public static string ExecuteSPScalarWithParameters(string Procedure, string Par_1, string Val_1, string Par_2, string Val_2,
            string Par_3, string Val_3, string Par_4, string Val_4, string Par_5, string Val_5)
        {
            string returnVal = string.Empty;

            try
            {
                using (SqlConnection con = new SqlConnection(ConStr))
                {
                    using (SqlCommand cmd = new SqlCommand(Procedure, con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        SqlDataAdapter da = new SqlDataAdapter(cmd);

                        cmd.Parameters.AddWithValue(Par_1, Val_1);
                        cmd.Parameters.AddWithValue(Par_2, Val_2);
                        cmd.Parameters.AddWithValue(Par_3, Val_3);
                        cmd.Parameters.AddWithValue(Par_4, Val_4);
                        cmd.Parameters.AddWithValue(Par_5, Val_5);

                        if (con.State == ConnectionState.Closed)
                            con.Open();

                        cmd.ExecuteNonQuery();

                        con.Close();
                    }
                }
            }
            catch
            {
                throw;
            }

            return returnVal;
        }

        public static string ExecuteSPScalarWithParameters(string Procedure, string Par_1, string Val_1, string Par_2, string Val_2,
            string Par_3, string Val_3, string Par_4, string Val_4, string Par_5, string Val_5, string Par_6, string Val_6)
        {
            string returnVal;

            try
            {
                using (SqlConnection con = new SqlConnection(ConStr))
                {
                    using (SqlCommand cmd = new SqlCommand(Procedure, con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue(Par_1, Val_1);
                        cmd.Parameters.AddWithValue(Par_2, Val_2);
                        cmd.Parameters.AddWithValue(Par_3, Val_3);
                        cmd.Parameters.AddWithValue(Par_4, Val_4);
                        cmd.Parameters.AddWithValue(Par_5, Val_5);
                        cmd.Parameters.AddWithValue(Par_6, Val_6);

                        if (con.State == ConnectionState.Closed)
                            con.Open();

                        int temp = (int)cmd.ExecuteScalar();

                        returnVal = temp.ToString();

                        con.Close();
                    }
                }
            }
            catch
            {
                throw;
            }

            return returnVal.ToString();
        }


        public static string ExecuteSPScalarWithParameters(string Procedure, string Par_1, string Val_1, string Par_2, string Val_2,
            string Par_3, string Val_3, string Par_4, string Val_4, string Par_5, string Val_5, string Par_6, string Val_6, string Par_7, string Val_7)
        {
            string returnVal;

            try
            {
                using (SqlConnection con = new SqlConnection(ConStr))
                {
                    using (SqlCommand cmd = new SqlCommand(Procedure, con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue(Par_1, Val_1);
                        cmd.Parameters.AddWithValue(Par_2, Val_2);
                        cmd.Parameters.AddWithValue(Par_3, Val_3);
                        cmd.Parameters.AddWithValue(Par_4, Val_4);
                        cmd.Parameters.AddWithValue(Par_5, Val_5);
                        cmd.Parameters.AddWithValue(Par_6, Val_6);
                        cmd.Parameters.AddWithValue(Par_7, Val_7);

                        if (con.State == ConnectionState.Closed)
                            con.Open();

                        int temp = (int)cmd.ExecuteScalar();

                        returnVal = temp.ToString();

                        con.Close();
                    }
                }
            }
            catch
            {
                throw;
            }

            return returnVal.ToString();
        }


        public static string ExecuteSPScalarWithParameters(string Procedure, string Par_1, string Val_1, string Par_2, string Val_2,
            string Par_3, string Val_3, string Par_4, string Val_4, string Par_5, string Val_5, string Par_6, string Val_6,
            string Par_7, string Val_7, string Par_8, string Val_8, string Par_9, string Val_9, string Par_10, string Val_10,
             string Par_11, string Val_11)
        {
            int returnVal;

            try
            {
                using (SqlConnection con = new SqlConnection(ConStr))
                {
                    using (SqlCommand cmd = new SqlCommand(Procedure, con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        SqlDataAdapter da = new SqlDataAdapter(cmd);

                        cmd.Parameters.AddWithValue(Par_1, Val_1);
                        cmd.Parameters.AddWithValue(Par_2, Val_2);
                        cmd.Parameters.AddWithValue(Par_3, Val_3);
                        cmd.Parameters.AddWithValue(Par_4, Val_4);
                        cmd.Parameters.AddWithValue(Par_5, Val_5);
                        cmd.Parameters.AddWithValue(Par_6, Val_6);
                        cmd.Parameters.AddWithValue(Par_7, Val_7);
                        cmd.Parameters.AddWithValue(Par_8, Val_8);
                        cmd.Parameters.AddWithValue(Par_9, Val_9);
                        cmd.Parameters.AddWithValue(Par_10, Val_10);
                        cmd.Parameters.AddWithValue(Par_11, Val_11);

                        if (con.State == ConnectionState.Closed)
                            con.Open();

                        returnVal = (int)cmd.ExecuteScalar();

                        con.Close();
                    }
                }
            }
            catch
            {
                throw;
            }

            return returnVal.ToString();
        }

        public static string ExecuteSPScalarWithParameters(string Procedure, string Par_1, string Val_1, string Par_2, string Val_2,
            string Par_3, string Val_3, string Par_4, string Val_4, string Par_5, string Val_5, string Par_6, string Val_6,
            string Par_7, string Val_7, string Par_8, string Val_8, string Par_9, string Val_9, string Par_10, string Val_10,
             string Par_11, string Val_11, string Par_12, string Val_12)
        {
            int returnVal;

            try
            {
                using (SqlConnection con = new SqlConnection(ConStr))
                {
                    using (SqlCommand cmd = new SqlCommand(Procedure, con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        SqlDataAdapter da = new SqlDataAdapter(cmd);

                        cmd.Parameters.AddWithValue(Par_1, Val_1);
                        cmd.Parameters.AddWithValue(Par_2, Val_2);
                        cmd.Parameters.AddWithValue(Par_3, Val_3);
                        cmd.Parameters.AddWithValue(Par_4, Val_4);
                        cmd.Parameters.AddWithValue(Par_5, Val_5);
                        cmd.Parameters.AddWithValue(Par_6, Val_6);
                        cmd.Parameters.AddWithValue(Par_7, Val_7);
                        cmd.Parameters.AddWithValue(Par_8, Val_8);
                        cmd.Parameters.AddWithValue(Par_9, Val_9);
                        cmd.Parameters.AddWithValue(Par_10, Val_10);
                        cmd.Parameters.AddWithValue(Par_11, Val_11);
                        cmd.Parameters.AddWithValue(Par_12, Val_12);

                        if (con.State == ConnectionState.Closed)
                            con.Open();

                        returnVal = (int)cmd.ExecuteScalar();

                        con.Close();
                    }
                }
            }
            catch
            {
                throw;
            }

            return returnVal.ToString();
        }
        public static string ExecuteSPScalarWithParameters(string Procedure, string Par_1, string Val_1, string Par_2, string Val_2,
           string Par_3, string Val_3, string Par_4, string Val_4, string Par_5, string Val_5, string Par_6, string Val_6,
           string Par_7, string Val_7, string Par_8, string Val_8, string Par_9, string Val_9, string Par_10, string Val_10,
            string Par_11, string Val_11, string Par_12, string Val_12, string Par_13, string Val_13, string Par_14, string Val_14)
        {
            Guid returnVal;

            try
            {
                using (SqlConnection con = new SqlConnection(ConStr))
                {
                    using (SqlCommand cmd = new SqlCommand(Procedure, con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        SqlDataAdapter da = new SqlDataAdapter(cmd);

                        cmd.Parameters.AddWithValue(Par_1, Val_1);
                        cmd.Parameters.AddWithValue(Par_2, Val_2);
                        cmd.Parameters.AddWithValue(Par_3, Val_3);
                        cmd.Parameters.AddWithValue(Par_4, Val_4);
                        cmd.Parameters.AddWithValue(Par_5, Val_5);
                        cmd.Parameters.AddWithValue(Par_6, Val_6);
                        cmd.Parameters.AddWithValue(Par_7, Val_7);
                        cmd.Parameters.AddWithValue(Par_8, Val_8);
                        cmd.Parameters.AddWithValue(Par_9, Val_9);
                        cmd.Parameters.AddWithValue(Par_10, Val_10);
                        cmd.Parameters.AddWithValue(Par_11, Val_11);
                        cmd.Parameters.AddWithValue(Par_12, Val_12);
                        cmd.Parameters.AddWithValue(Par_13, Val_13);
                        cmd.Parameters.AddWithValue(Par_14, Val_14);

                        if (con.State == ConnectionState.Closed)
                            con.Open();

                        returnVal = (Guid)cmd.ExecuteScalar();

                        con.Close();
                    }
                }
            }
            catch
            {
                throw;
            }

            return returnVal.ToString();
        }

        #endregion

        public static string EncryptDecrypt(string Action, string Text)
        {
            string Final = "";

            try
            {
                if (Action == Encryption.Encrypt.ToString())
                {
                    byte[] Data = UTF8Encoding.UTF8.GetBytes(Text);

                    using (MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider())
                    {
                        byte[] Keys = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes("P@$$w0rd"));

                        using (TripleDESCryptoServiceProvider tripDes = new TripleDESCryptoServiceProvider() { Key = Keys, Mode = CipherMode.ECB, Padding = PaddingMode.PKCS7 })
                        {
                            ICryptoTransform transformer = tripDes.CreateEncryptor();
                            byte[] result = transformer.TransformFinalBlock(Data, 0, Data.Length);

                            Final = Convert.ToBase64String(result, 0, result.Length);
                        }
                    }
                }
                else
                {
                    byte[] Data = Convert.FromBase64String(Text);

                    using (MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider())
                    {
                        byte[] Keys = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes("P@$$w0rd"));

                        using (TripleDESCryptoServiceProvider tripDes = new TripleDESCryptoServiceProvider() { Key = Keys, Mode = CipherMode.ECB, Padding = PaddingMode.PKCS7 })
                        {
                            ICryptoTransform transformer = tripDes.CreateDecryptor();
                            byte[] result = transformer.TransformFinalBlock(Data, 0, Data.Length);

                            Final = UTF8Encoding.UTF8.GetString(result, 0, result.Length);
                        }
                    }
                }
            }
            catch
            {
                throw;
            }

            return Final;
        }

        public static bool SendEmail()
        {
            return true;
        }

        public static BNUser GetCurrentUser()
        {
            BNUser user = new BNUser();

            try
            {

            }
            catch
            {
                throw;
            }

            return user;
        }

        public static string ConvertUrlFromByteArray(byte[] array)
        {
            string uri = "";

            try
            {
                string rarBase64Data = Convert.ToBase64String(array);
                uri = string.Format("data:image/png;base64,{0}", rarBase64Data);
            }
            catch
            {
                throw;
            }
            return uri;
        }

        public static DataSet GetData(string spName, SqlParameter spParameter)
        {
            DataSet ds = new DataSet();

            try
            {
                SqlConnection con = new SqlConnection(ConStr);
                SqlDataAdapter da = new SqlDataAdapter(spName, con);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                if (spParameter != null)
                {
                    da.SelectCommand.Parameters.Add(spParameter);
                }

                da.Fill(ds);
            }
            catch
            {
                throw;
            }
            return ds;
        }

        public virtual DataTable ExecuteSPWithParameteres(string spname, string param1, string val1, string param2, string val2, string param3, string val3, string param4, string val4, string param5, string val5)
        {
            DataTable dt = new DataTable();
            return dt;
        }

        public virtual DataTable ExecuteSPWithParameteres(string spname, string param1, string val1, string param2, string val2, string param3, string val3, string param4, string val4, string param5, string val5, string param6, string val6)
        {
            DataTable dt = new DataTable();
            return dt;
        }

        public virtual DataTable ExecuteSPWithParameteres(string spname, string param1, string val1, string param2, string val2, string param3, string val3, string param4, string val4, string param5, string val5, string param6, string val6, string param7, string val7)
        {
            DataTable dt = new DataTable();
            return dt;
        }

        public virtual DataTable ExecuteSPWithParameteres(string spname, string param1, string val1, string param2, string val2, string param3, string val3, string param4, string val4, string param5, string val5, string param6, string val6, string param7, string val7, string param8, string val8)
        {
            DataTable dt = new DataTable();
            return dt;
        }

        public virtual DataTable ExecuteSPWithParameteres(string spname, string param1, string val1, string param2, string val2, string param3, string val3, string param4, string val4, string param5, string val5, string param6, string val6, string param7, string val7, string param8, string val8, string param9, string val9)
        {
            DataTable dt = new DataTable();
            return dt;
        }

        public virtual DataTable ExecuteSPWithParameteres(string spname, string param1, string val1, string param2, string val2, string param3, string val3, string param4, string val4, string param5, string val5, string param6, string val6, string param7, string val7, string param8, string val8, string param9, string val9, string param10, string val10)
        {
            DataTable dt = new DataTable();
            return dt;
        }

        public static DataTable ExecuteSPWithReturnValue(string Procedure, string Par_1, string Val_1)
        {
            DataTable dt = new DataTable();

            try
            {
                using (SqlConnection con = new SqlConnection())
                {
                    using (SqlCommand cmd = new SqlCommand(Procedure))
                    {
                        SqlDataAdapter da = new SqlDataAdapter(cmd);

                        cmd.Parameters.AddWithValue(Par_1, Val_1);


                        if (con.State == ConnectionState.Closed)
                            con.Open();

                        da.Fill(dt);

                        con.Close();
                    }
                }
            }
            catch
            {
                throw;
            }

            return dt;

        }

        public virtual DataTable ExecuteSPWithReturnValue(string Procedure, string Par_1, string Val_1, string Par_2, string Val_2)
        {
            DataTable dt = new DataTable();

            try
            {
                using (SqlConnection con = new SqlConnection())
                {
                    using (SqlCommand cmd = new SqlCommand(Procedure))
                    {
                        SqlDataAdapter da = new SqlDataAdapter(cmd);

                        cmd.Parameters.AddWithValue(Par_1, Val_1);
                        cmd.Parameters.AddWithValue(Par_2, Val_2);


                        if (con.State == ConnectionState.Closed)
                            con.Open();

                        da.Fill(dt);

                        con.Close();
                    }
                }
            }
            catch
            {
                throw;
            }

            return dt;

        }

        public static DataTable ExecuteSPWithReturnValue(string Procedure, string Par_1, DataTable Par_3)
        {
            DataTable dt = new DataTable();

            try
            {
                using (SqlConnection con = new SqlConnection())
                {
                    using (SqlCommand cmd = new SqlCommand(Procedure))
                    {
                        SqlDataAdapter da = new SqlDataAdapter(cmd);



                        if (con.State == ConnectionState.Closed)
                            con.Open();

                        da.Fill(dt);

                        con.Close();
                    }
                }
            }
            catch
            {
                throw;
            }

            return dt;
        }

        public virtual DataTable ExecuteSPWithReturnValue(string Procedure, string Par_1, string Val_1, string Par_2, string Val_2, string Par_3, string Val_3)
        {
            DataTable dt = new DataTable();

            try
            {
                using (SqlConnection con = new SqlConnection())
                {
                    using (SqlCommand cmd = new SqlCommand(Procedure))
                    {
                        SqlDataAdapter da = new SqlDataAdapter(cmd);

                        cmd.Parameters.AddWithValue(Par_1, Val_1);
                        cmd.Parameters.AddWithValue(Par_2, Val_2);
                        cmd.Parameters.AddWithValue(Par_3, Val_3);

                        if (con.State == ConnectionState.Closed)
                            con.Open();

                        da.Fill(dt);

                        con.Close();
                    }
                }
            }
            catch
            {
                throw;
            }

            return dt;
        }

        public virtual DataTable ExecuteSPWithReturnValue(string Procedure, string Par_1, string Val_1, string Par_2, string Val_2, string Par_3, string Val_3, string Par_4, string Val_4)
        {
            DataTable dt = new DataTable();

            try
            {
                using (SqlConnection con = new SqlConnection())
                {
                    using (SqlCommand cmd = new SqlCommand(Procedure))
                    {
                        SqlDataAdapter da = new SqlDataAdapter(cmd);

                        cmd.Parameters.AddWithValue(Par_1, Val_1);
                        cmd.Parameters.AddWithValue(Par_2, Val_2);
                        cmd.Parameters.AddWithValue(Par_3, Val_3);
                        cmd.Parameters.AddWithValue(Par_4, Val_4);

                        if (con.State == ConnectionState.Closed)
                            con.Open();

                        da.Fill(dt);

                        con.Close();
                    }
                }
            }
            catch
            {
                throw;
            }

            return dt;

        }

        public virtual DataTable ExecuteSPWithReturnValue(string Procedure, string Par_1, string Val_1, string Par_2, string Val_2, string Par_3, string Val_3, string Par_4, string Val_4, string Par_5, string Val_5, string Par_6, string Val_6, string Par_7, string Val_7, string Par_8, string Val_8, string Par_9, string Val_9, string Par_10, string Val_10, string Par_11, string Val_11, string Par_12, string Val_12, string Par_13, string Val_13, string Par_14, string Val_14, string Par_15, string Val_15)
        {
            DataTable dt = new DataTable();

            try
            {
                using (SqlConnection con = new SqlConnection())
                {
                    using (SqlCommand cmd = new SqlCommand(Procedure))
                    {
                        SqlDataAdapter da = new SqlDataAdapter(cmd);

                        cmd.Parameters.AddWithValue(Par_1, Val_1);
                        cmd.Parameters.AddWithValue(Par_2, Val_2);
                        cmd.Parameters.AddWithValue(Par_3, Val_3);
                        cmd.Parameters.AddWithValue(Par_4, Val_4);
                        cmd.Parameters.AddWithValue(Par_5, Val_5);
                        cmd.Parameters.AddWithValue(Par_6, Val_6);
                        cmd.Parameters.AddWithValue(Par_7, Val_7);
                        cmd.Parameters.AddWithValue(Par_8, Val_8);
                        cmd.Parameters.AddWithValue(Par_9, Val_9);
                        cmd.Parameters.AddWithValue(Par_10, Val_10);
                        cmd.Parameters.AddWithValue(Par_11, Val_11);
                        cmd.Parameters.AddWithValue(Par_12, Val_12);
                        cmd.Parameters.AddWithValue(Par_13, Val_13);
                        cmd.Parameters.AddWithValue(Par_14, Val_14);
                        cmd.Parameters.AddWithValue(Par_15, Val_15);

                        if (con.State == ConnectionState.Closed)
                            con.Open();

                        da.Fill(dt);

                        con.Close();
                    }
                }
            }
            catch
            {
                throw;
            }

            return dt;

        }

        public static int ExecuteSPWithReturnValue(string Procedure, string Par_1, string Val_1, string Par_2, string Val_2, string Par_3, string Val_3, string Par_4, string Val_4, string Par_5, string Val_5, string Par_6, string Val_6, string Par_7, string Val_7, string Par_8, string Val_8, string Par_9, string Val_9, string Par_10, string Val_10, string Par_11, string Val_11, string Par_12, string Val_12, string Par_13, string Val_13, string Par_14, string Val_14, string Par_15, string Val_15, string Par_16, string Val_16, string Par_17, string Val_17)
        {
            int a = 10;
            DataTable dt = new DataTable();

            try
            {
                using (SqlConnection con = new SqlConnection())
                {
                    using (SqlCommand cmd = new SqlCommand(Procedure))
                    {
                        SqlDataAdapter da = new SqlDataAdapter(cmd);

                        cmd.Parameters.AddWithValue(Par_1, Val_1);
                        cmd.Parameters.AddWithValue(Par_2, Val_2);
                        cmd.Parameters.AddWithValue(Par_3, Val_3);
                        cmd.Parameters.AddWithValue(Par_4, Val_4);
                        cmd.Parameters.AddWithValue(Par_5, Val_5);
                        cmd.Parameters.AddWithValue(Par_6, Val_6);
                        cmd.Parameters.AddWithValue(Par_7, Val_7);
                        cmd.Parameters.AddWithValue(Par_8, Val_8);
                        cmd.Parameters.AddWithValue(Par_9, Val_9);
                        cmd.Parameters.AddWithValue(Par_10, Val_10);
                        cmd.Parameters.AddWithValue(Par_11, Val_11);
                        cmd.Parameters.AddWithValue(Par_12, Val_12);
                        cmd.Parameters.AddWithValue(Par_13, Val_13);
                        cmd.Parameters.AddWithValue(Par_14, Val_14);
                        cmd.Parameters.AddWithValue(Par_15, Val_15);
                        cmd.Parameters.AddWithValue(Par_16, Val_16);
                        cmd.Parameters.AddWithValue(Par_17, Val_17);



                        if (con.State == ConnectionState.Closed)
                            con.Open();

                        da.Fill(dt);

                        con.Close();
                    }
                }
            }
            catch
            {
                throw;
            }

            return a;

        }

        public virtual DataTable ExecuteSPWithReturnValue(string Procedure, string Par_1, string Val_1, string Par_2, string Val_2, string Par_3, string Val_3, string Par_4, string Val_4, string Par_5, string Val_5, string Par_6, string Val_6, string Par_7, string Val_7, string Par_8, string Val_8, string Par_9, string Val_9, string Par_10, string Val_10, string Par_11, string Val_11, string Par_12, string Val_12, string Par_13, string Val_13, string Par_14, string Val_14, string Par_15, string Val_15, string Par_16, string Val_16, string Par_17, string Val_17, string Par_18, string Val_18)
        {
            DataTable dt = new DataTable();

            try
            {
                using (SqlConnection con = new SqlConnection())
                {
                    using (SqlCommand cmd = new SqlCommand(Procedure))
                    {
                        SqlDataAdapter da = new SqlDataAdapter(cmd);

                        cmd.Parameters.AddWithValue(Par_1, Val_1);
                        cmd.Parameters.AddWithValue(Par_2, Val_2);
                        cmd.Parameters.AddWithValue(Par_3, Val_3);
                        cmd.Parameters.AddWithValue(Par_4, Val_4);
                        cmd.Parameters.AddWithValue(Par_5, Val_5);
                        cmd.Parameters.AddWithValue(Par_6, Val_6);
                        cmd.Parameters.AddWithValue(Par_7, Val_7);
                        cmd.Parameters.AddWithValue(Par_8, Val_8);
                        cmd.Parameters.AddWithValue(Par_9, Val_9);
                        cmd.Parameters.AddWithValue(Par_10, Val_10);
                        cmd.Parameters.AddWithValue(Par_11, Val_11);
                        cmd.Parameters.AddWithValue(Par_12, Val_12);
                        cmd.Parameters.AddWithValue(Par_13, Val_13);
                        cmd.Parameters.AddWithValue(Par_14, Val_14);
                        cmd.Parameters.AddWithValue(Par_15, Val_15);
                        cmd.Parameters.AddWithValue(Par_16, Val_16);
                        cmd.Parameters.AddWithValue(Par_17, Val_17);
                        cmd.Parameters.AddWithValue(Par_18, Val_18);


                        if (con.State == ConnectionState.Closed)
                            con.Open();

                        da.Fill(dt);

                        con.Close();
                    }
                }
            }
            catch
            {
                throw;
            }

            return dt;

        }

        public virtual DataTable ExecuteSPWithReturnValue(string Procedure, string Par_1, string Val_1, string Par_2, string Val_2, string Par_3, string Val_3, string Par_4, string Val_4, string Par_5, string Val_5, string Par_6, string Val_6, string Par_7, string Val_7, string Par_8, string Val_8, string Par_9, string Val_9, string Par_10, string Val_10, string Par_11, string Val_11, string Par_12, string Val_12, string Par_13, string Val_13, string Par_14, string Val_14, string Par_15, string Val_15, string Par_16, string Val_16, string Par_17, string Val_17, string Par_18, string Val_18, string Par_19, string Val_19)
        {
            DataTable dt = new DataTable();

            try
            {
                using (SqlConnection con = new SqlConnection())
                {
                    using (SqlCommand cmd = new SqlCommand(Procedure))
                    {
                        SqlDataAdapter da = new SqlDataAdapter(cmd);

                        cmd.Parameters.AddWithValue(Par_1, Val_1);
                        cmd.Parameters.AddWithValue(Par_2, Val_2);
                        cmd.Parameters.AddWithValue(Par_3, Val_3);
                        cmd.Parameters.AddWithValue(Par_4, Val_4);
                        cmd.Parameters.AddWithValue(Par_5, Val_5);
                        cmd.Parameters.AddWithValue(Par_6, Val_6);
                        cmd.Parameters.AddWithValue(Par_7, Val_7);
                        cmd.Parameters.AddWithValue(Par_8, Val_8);
                        cmd.Parameters.AddWithValue(Par_9, Val_9);
                        cmd.Parameters.AddWithValue(Par_10, Val_10);
                        cmd.Parameters.AddWithValue(Par_11, Val_11);
                        cmd.Parameters.AddWithValue(Par_12, Val_12);
                        cmd.Parameters.AddWithValue(Par_13, Val_13);
                        cmd.Parameters.AddWithValue(Par_14, Val_14);
                        cmd.Parameters.AddWithValue(Par_15, Val_15);
                        cmd.Parameters.AddWithValue(Par_16, Val_16);
                        cmd.Parameters.AddWithValue(Par_17, Val_17);
                        cmd.Parameters.AddWithValue(Par_18, Val_18);
                        cmd.Parameters.AddWithValue(Par_19, Val_19);

                        if (con.State == ConnectionState.Closed)
                            con.Open();

                        da.Fill(dt);

                        con.Close();
                    }
                }
            }
            catch
            {
                throw;
            }

            return dt;

        }

        public static string ExecuteSPScalarWithParameters(string Procedure, string Par_1, string Val_1, string Par_2,
            string Val_2, string Par_3, string Val_3, string Par_4, string Val_4, string Par_5, string Val_5, string Par_6,
            string Val_6, string Par_7, string Val_7, string Par_8, string Val_8, string Par_9, string Val_9, string Par_10,
            string Val_10, string Par_11, string Val_11, string Par_12, string Val_12, string Par_13, string Val_13, string Par_14,
            string Val_14, string Par_15, string Val_15, string Par_16, string Val_16, string Par_17, string Val_17)
        {
            string returnVal = string.Empty;

            try
            {
                using (SqlConnection con = new SqlConnection(ConStr))
                {
                    using (SqlCommand cmd = new SqlCommand(Procedure))
                    {
                        SqlDataAdapter da = new SqlDataAdapter(cmd);

                        cmd.Parameters.AddWithValue(Par_1, Val_1);
                        cmd.Parameters.AddWithValue(Par_2, Val_2);
                        cmd.Parameters.AddWithValue(Par_3, Val_3);
                        cmd.Parameters.AddWithValue(Par_4, Val_4);
                        cmd.Parameters.AddWithValue(Par_5, Val_5);
                        cmd.Parameters.AddWithValue(Par_6, Val_6);
                        cmd.Parameters.AddWithValue(Par_7, Val_7);
                        cmd.Parameters.AddWithValue(Par_8, Val_8);
                        cmd.Parameters.AddWithValue(Par_9, Val_9);
                        cmd.Parameters.AddWithValue(Par_10, Val_10);
                        cmd.Parameters.AddWithValue(Par_11, Val_11);
                        cmd.Parameters.AddWithValue(Par_12, Val_12);
                        cmd.Parameters.AddWithValue(Par_13, Val_13);
                        cmd.Parameters.AddWithValue(Par_14, Val_14);
                        cmd.Parameters.AddWithValue(Par_15, Val_15);
                        cmd.Parameters.AddWithValue(Par_16, Val_16);
                        cmd.Parameters.AddWithValue(Par_17, Val_17);

                        if (con.State == ConnectionState.Closed)
                            con.Open();

                        int temp = (int)cmd.ExecuteScalar();

                        returnVal = temp.ToString();

                        con.Close();
                    }
                }
            }
            catch
            {
                throw;
            }

            return returnVal;

        }

        public static string ExecuteSPScalarWithParameters(string Procedure, string Par_1, string Val_1, string Par_2,
            string Val_2, string Par_3, string Val_3, string Par_4, string Val_4, string Par_5, string Val_5, string Par_6,
            string Val_6, string Par_7, string Val_7, string Par_8, string Val_8, string Par_9, string Val_9, string Par_10,
            string Val_10, string Par_11, string Val_11, string Par_12, string Val_12, string Par_13, string Val_13, string Par_14,
            string Val_14, string Par_15, string Val_15)
        {
            string returnVal = string.Empty;

            try
            {
                using (SqlConnection con = new SqlConnection(ConStr))
                {
                    using (SqlCommand cmd = new SqlCommand(Procedure, con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue(Par_1, Val_1);
                        cmd.Parameters.AddWithValue(Par_2, Val_2);
                        cmd.Parameters.AddWithValue(Par_3, Val_3);
                        cmd.Parameters.AddWithValue(Par_4, Val_4);
                        cmd.Parameters.AddWithValue(Par_5, Val_5);
                        cmd.Parameters.AddWithValue(Par_6, Val_6);
                        cmd.Parameters.AddWithValue(Par_7, Val_7);
                        cmd.Parameters.AddWithValue(Par_8, Val_8);
                        cmd.Parameters.AddWithValue(Par_9, Val_9);
                        cmd.Parameters.AddWithValue(Par_10, Val_10);
                        cmd.Parameters.AddWithValue(Par_11, Val_11);
                        cmd.Parameters.AddWithValue(Par_12, Val_12);
                        cmd.Parameters.AddWithValue(Par_13, Val_13);
                        cmd.Parameters.AddWithValue(Par_14, Val_14);
                        cmd.Parameters.AddWithValue(Par_15, Val_15);

                        if (con.State == ConnectionState.Closed)
                            con.Open();

                        int temp = (int)cmd.ExecuteScalar();

                        returnVal = temp.ToString();

                        con.Close();
                    }
                }
            }
            catch
            {
                throw;
            }

            return returnVal;
        }

        public static string ExecuteSPScalarWithParameters(string Procedure, string Par_1, Guid Val_1, string Par_2,
            string Val_2, string Par_3, string Val_3, string Par_4, string Val_4, string Par_5, string Val_5, string Par_6,
            string Val_6, string Par_7, string Val_7, string Par_8, string Val_8, string Par_9, string Val_9, string Par_10,
            string Val_10, string Par_11, string Val_11, string Par_12, string Val_12, string Par_13, string Val_13, string Par_14,
            string Val_14, string Par_15, string Val_15)
        {
            string returnVal = string.Empty;

            try
            {
                using (SqlConnection con = new SqlConnection(ConStr))
                {
                    using (SqlCommand cmd = new SqlCommand(Procedure, con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue(Par_1, Val_1);
                        cmd.Parameters.AddWithValue(Par_2, Val_2);
                        cmd.Parameters.AddWithValue(Par_3, Val_3);
                        cmd.Parameters.AddWithValue(Par_4, Val_4);
                        cmd.Parameters.AddWithValue(Par_5, Val_5);
                        cmd.Parameters.AddWithValue(Par_6, Val_6);
                        cmd.Parameters.AddWithValue(Par_7, Val_7);
                        cmd.Parameters.AddWithValue(Par_8, Val_8);
                        cmd.Parameters.AddWithValue(Par_9, Val_9);
                        cmd.Parameters.AddWithValue(Par_10, Val_10);
                        cmd.Parameters.AddWithValue(Par_11, Val_11);
                        cmd.Parameters.AddWithValue(Par_12, Val_12);
                        cmd.Parameters.AddWithValue(Par_13, Val_13);
                        cmd.Parameters.AddWithValue(Par_14, Val_14);
                        cmd.Parameters.AddWithValue(Par_15, Val_15);

                        if (con.State == ConnectionState.Closed)
                            con.Open();

                        Guid g = (Guid)cmd.ExecuteScalar();

                        returnVal = g.ToString();

                        con.Close();
                    }
                }
            }
            catch
            {
                throw;
            }

            return returnVal;
        }

        public static string ExecuteSPScalarWithParameters(string Procedure, string Par_1, string Val_1, string Par_2,
            string Val_2, string Par_3, string Val_3, string Par_4, string Val_4, string Par_5, string Val_5, string Par_6,
            string Val_6, string Par_7, string Val_7, string Par_8, string Val_8, string Par_9, string Val_9, string Par_10,
            string Val_10, string Par_11, string Val_11, string Par_12, string Val_12, string Par_13, string Val_13, string Par_14,
            string Val_14, string Par_15, string Val_15, string Par_16, string Val_16)
        {
            string returnVal = string.Empty;

            try
            {
                using (SqlConnection con = new SqlConnection(ConStr))
                {
                    using (SqlCommand cmd = new SqlCommand(Procedure, con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue(Par_1, Val_1);
                        cmd.Parameters.AddWithValue(Par_2, Val_2);
                        cmd.Parameters.AddWithValue(Par_3, Val_3);
                        cmd.Parameters.AddWithValue(Par_4, Val_4);
                        cmd.Parameters.AddWithValue(Par_5, Val_5);
                        cmd.Parameters.AddWithValue(Par_6, Val_6);
                        cmd.Parameters.AddWithValue(Par_7, Val_7);
                        cmd.Parameters.AddWithValue(Par_8, Val_8);
                        cmd.Parameters.AddWithValue(Par_9, Val_9);
                        cmd.Parameters.AddWithValue(Par_10, Val_10);
                        cmd.Parameters.AddWithValue(Par_11, Val_11);
                        cmd.Parameters.AddWithValue(Par_12, Val_12);
                        cmd.Parameters.AddWithValue(Par_13, Val_13);
                        cmd.Parameters.AddWithValue(Par_14, Val_14);
                        cmd.Parameters.AddWithValue(Par_15, Val_15);
                        cmd.Parameters.AddWithValue(Par_16, Val_16);

                        if (con.State == ConnectionState.Closed)
                            con.Open();

                        int temp = (int)cmd.ExecuteScalar();

                        returnVal = temp.ToString();

                        con.Close();
                    }
                }
            }
            catch
            {
                throw;
            }

            return returnVal;

        }


        public static string ExecuteSPScalarWithParameters(string Procedure, string Par_1, string Val_1, string Par_2, string Val_2,
            string Par_3, string Val_3, string Par_4, string Val_4, string Par_5, string Val_5, string Par_6, string Val_6,
            string Par_7, string Val_7, string Par_8, string Val_8, string Par_9, string Val_9, string Par_10, string Val_10,
            string Par_11, string Val_11, string Par_12, string Val_12, string Par_13, string Val_13, string Par_14, string Val_14, string Par_15, string Val_15,
            string Par_16, string Val_16, string Par_17, string Val_17, string Par_18, string Val_18, string Par_19, string Val_19)
        {
            int returnVal;

            try
            {
                using (SqlConnection con = new SqlConnection(ConStr))
                {
                    using (SqlCommand cmd = new SqlCommand(Procedure, con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        SqlDataAdapter da = new SqlDataAdapter(cmd);

                        cmd.Parameters.AddWithValue(Par_1, Val_1);
                        cmd.Parameters.AddWithValue(Par_2, Val_2);
                        cmd.Parameters.AddWithValue(Par_3, Val_3);
                        cmd.Parameters.AddWithValue(Par_4, Val_4);
                        cmd.Parameters.AddWithValue(Par_5, Val_5);
                        cmd.Parameters.AddWithValue(Par_6, Val_6);
                        cmd.Parameters.AddWithValue(Par_7, Val_7);
                        cmd.Parameters.AddWithValue(Par_8, Val_8);
                        cmd.Parameters.AddWithValue(Par_9, Val_9);
                        cmd.Parameters.AddWithValue(Par_10, Val_10);
                        cmd.Parameters.AddWithValue(Par_11, Val_11);
                        cmd.Parameters.AddWithValue(Par_12, Val_12);
                        cmd.Parameters.AddWithValue(Par_13, Val_13);
                        cmd.Parameters.AddWithValue(Par_14, Val_14);
                        cmd.Parameters.AddWithValue(Par_15, Val_15);
                        cmd.Parameters.AddWithValue(Par_16, Val_16);
                        cmd.Parameters.AddWithValue(Par_17, Val_17);
                        cmd.Parameters.AddWithValue(Par_18, Val_18);
                        cmd.Parameters.AddWithValue(Par_19, Val_19);

                        if (con.State == ConnectionState.Closed)
                            con.Open();

                        returnVal = (int)cmd.ExecuteScalar();

                        con.Close();
                    }
                }
            }
            catch
            {
                throw;
            }

            return returnVal.ToString();
        }


        public static void ExecuteSPScalarWithParameters(string Procedure, string Par_1, string Val_1, string Par_2, string Val_2,
            string Par_3, string Val_3, string Par_4, string Val_4, string Par_5, string Val_5, string Par_6, string Val_6,
            string Par_7, string Val_7, string Par_8, string Val_8, string Par_9, string Val_9, string Par_10, string Val_10,
            string Par_11, string Val_11, string Par_12, string Val_12, string Par_13, string Val_13, string Par_14, string Val_14, string Par_15, string Val_15,
            string Par_16, string Val_16, string Par_17, string Val_17, string Par_18, string Val_18, string Par_19, string Val_19, string Par_20, string Val_20)
        {


            try
            {
                using (SqlConnection con = new SqlConnection(ConStr))
                {
                    using (SqlCommand cmd = new SqlCommand(Procedure, con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        SqlDataAdapter da = new SqlDataAdapter(cmd);

                        cmd.Parameters.AddWithValue(Par_1, Val_1);
                        cmd.Parameters.AddWithValue(Par_2, Val_2);
                        cmd.Parameters.AddWithValue(Par_3, Val_3);
                        cmd.Parameters.AddWithValue(Par_4, Val_4);
                        cmd.Parameters.AddWithValue(Par_5, Val_5);
                        cmd.Parameters.AddWithValue(Par_6, Val_6);
                        cmd.Parameters.AddWithValue(Par_7, Val_7);
                        cmd.Parameters.AddWithValue(Par_8, Val_8);
                        cmd.Parameters.AddWithValue(Par_9, Val_9);
                        cmd.Parameters.AddWithValue(Par_10, Val_10);
                        cmd.Parameters.AddWithValue(Par_11, Val_11);
                        cmd.Parameters.AddWithValue(Par_12, Val_12);
                        cmd.Parameters.AddWithValue(Par_13, Val_13);
                        cmd.Parameters.AddWithValue(Par_14, Val_14);
                        cmd.Parameters.AddWithValue(Par_15, Val_15);
                        cmd.Parameters.AddWithValue(Par_16, Val_16);
                        cmd.Parameters.AddWithValue(Par_17, Val_17);
                        cmd.Parameters.AddWithValue(Par_18, Val_18);
                        cmd.Parameters.AddWithValue(Par_19, Val_19);
                        cmd.Parameters.AddWithValue(Par_20, Val_20);

                        if (con.State == ConnectionState.Closed)
                            con.Open();

                        cmd.ExecuteNonQuery();

                        con.Close();
                    }
                }
            }
            catch
            {
                throw;
            }

        }
        #endregion

        #region Enums

        public enum Encryption
        {
            Encrypt,
            Decrypt
        }

        public enum QueryStrings
        {
            Key,
            EditMode,
            Email,
            AnnouncementID,
            UserID,
            ChatID,
            ProductID,
            MessageID,
            PromotionID,
            StoryID,
            PropertyID,
            UserTypeID,
            IsAdmin,
            InterestRateID,
            TenureID,
            DownPaymentID,
            GovernorateID,
            WilayatID,
            PropertyTypeID,
            AmenityID,
            PopupID,
            ProductCategoryID,
            PreviewMode,
            Header,
            Description,
            HeaderColor,
            DescriptionColor,
            code
        }

        public enum ViewStates
        {
            ConsumerID,
            Announcement,
            Image,
            CoverImage,
            User,
            Property,
            Attempts
        }

        public enum Sessions
        {
            User,
            Source,
            Properties,
            Products,
            PreviewImageVendor,
            PreviewImageBuilder,
            PreviewImageHome
        }

        public enum Module
        {
            Announcements,
            Messages,
            Promotions,
            Popup,
            Stories,
            VendorAnnouncements,
            BuilderAnnouncements
        }

        public enum Keys
        {
            ClientID,
            ClientSecret,
            SiteURL
        }

        #endregion

        #region URLS

        public const string pg_Login = "Login.aspx";
        public const string pg_Dashboard = "Dashboard.aspx";
        public const string pg_Logout = "Logout.aspx";
        public const string pg_NoPermissions = "NoPermissions.aspx";
        public const string pg_Index = "Index.aspx";
        public const string pg_ForgotPassword = "ForgotPassword.aspx";
        public const string pg_Signup = "Signup.aspx";
        public const string pg_ResetPassword = "ResetPassword.aspx";
        public const string pg_AnnouncementTray = "AnnouncementsTray.aspx";
        public const string pg_AllPopups = "AllPopups.aspx";
        public const string pg_Popup = "Popup.aspx";
        public const string pg_Announcements = "Announcements.aspx";
        public const string pg_CreateStakeHolder = "CreateStakeHolder.aspx";
        public const string pg_ChatPage = "ChatPage.aspx";
        public const string pg_MessageTray = "MessageTray.aspx";
        public const string pg_NewMessage = "Message.aspx";
        public const string pg_Message = "MessageBoard.aspx";
        public const string pg_CreateProperty = "CreateProperty.aspx";
        public const string pg_AllUsers = "AllUsers.aspx";
        public const string pg_StakeHolderList = "StakeHolderList.aspx";
        public const string pg_AdminResetPassword = "AdminResetPassword.aspx";
        public const string pg_CreatePromotion = "CreatePromotion.aspx";
        public const string pg_PromotionsTray = "MyPromotions.aspx";
        public const string pg_CreateStories = "CreateStory.aspx";
        public const string pg_StoriesTray = "MyStories.aspx";
        public const string pg_AllStories = "AllStories.aspx";
        public const string pg_AllPromotions = "AllPromotions.aspx";
        public const string pg_AllBuilderAnnouncements = "AllBuilderAnnouncements.aspx";
        public const string pg_BuilderAnnouncement = "BuilderAnnouncement.aspx";
        public const string pg_AllVendorAnnouncements = "AllVendorAnnouncements.aspx";
        public const string pg_VendorAnnouncement = "VendorAnnouncement.aspx";
        public const string pg_MyProperties = "MyProperties.aspx";
        public const string pg_MyProducts = "MyProducts.aspx";
        public const string pg_MyPromotions = "MyPromotions.aspx";
        public const string pg_CreateProduct = "CreateProduct.aspx";
        public const string pg_AllProperties = "AlProperties.aspx";
        public const string pg_AllPropertiesAdmin = "AllProperties.aspx";
        public const string pg_PropertyDetails = "PropertyDetails.aspx";
        public const string pg_ProductDetails = "ProductDetails.aspx";
        public const string pg_AllProducts = "AllProducts.aspx";
        public const string pg_UserProfile = "UserProfile.aspx";
        public const string pg_ChatList = "ChatList.aspx";
        public const string pg_SearchProperties = "SearchedProperties.aspx";
        public const string pg_SearchedProducts = "SearchedProducts.aspx";
        public const string pg_AdminLogin = "AdminLogin.aspx";
        public const string pg_Tenure = "Tenure.aspx";
        public const string pg_TenureTray = "TenureTray.aspx";
        public const string pg_DownPayment = "DownPayment.aspx";
        public const string pg_Governorate = "Governorate.aspx";
        public const string pg_GovernorateTray = "GovernorateTray.aspx";
        public const string pg_Wilayat = "Wilayat.aspx";
        public const string pg_WilayatTray = "WilayatTray.aspx";
        public const string pg_PropertyType = "PropertyType.aspx";
        public const string pg_PropertyTypeTray = "PropertyTypeTray.aspx";
        public const string pg_Amenity = "Amenity.aspx";
        public const string pg_AmenityTray = "AmenityTray.aspx";
        public const string pg_ProductCategory = "ProductCategory.aspx";
        public const string pg_ProductCategoryTray = "ProductCategoryTray.aspx";
        public const string pg_InterestTray = "InterestRateTray.aspx";
        public const string pg_InterestRate = "InterestRate.aspx";

        public const string pc_NoProperty = "Assets/img/unnamed.jpg";
        public const string pc_NoUser = "Assets/img/default-image.jpg";
        public const string pc_Background = "Assets/img/background-default.png";
        public const string pc_Background_User = "data:image/svg+xml,%3Csvg xmlns = \"http://www.w3.org/2000/svg\" width=\"100\" height=\"100\" viewBox=\"0 0 100 100\"%3E%3Cg fill-rule=\"evenodd\"%3E%3Cg fill = \"%23ffffff\" fill-opacity=\"0.12\"%3E%3Cpath opacity = \".5\" d=\"M96 95h4v1h-4v4h-1v-4h-9v4h-1v-4h-9v4h-1v-4h-9v4h-1v-4h-9v4h-1v-4h-9v4h-1v-4h-9v4h-1v-4h-9v4h-1v-4h-9v4h-1v-4H0v-1h15v-9H0v-1h15v-9H0v-1h15v-9H0v-1h15v-9H0v-1h15v-9H0v-1h15v-9H0v-1h15v-9H0v-1h15v-9H0v-1h15V0h1v15h9V0h1v15h9V0h1v15h9V0h1v15h9V0h1v15h9V0h1v15h9V0h1v15h9V0h1v15h9V0h1v15h4v1h-4v9h4v1h-4v9h4v1h-4v9h4v1h-4v9h4v1h-4v9h4v1h-4v9h4v1h-4v9h4v1h-4v9zm-1 0v-9h-9v9h9zm-10 0v-9h-9v9h9zm-10 0v-9h-9v9h9zm-10 0v-9h-9v9h9zm-10 0v-9h-9v9h9zm-10 0v-9h-9v9h9zm-10 0v-9h-9v9h9zm-10 0v-9h-9v9h9zm-9-10h9v-9h-9v9zm10 0h9v-9h-9v9zm10 0h9v-9h-9v9zm10 0h9v-9h-9v9zm10 0h9v-9h-9v9zm10 0h9v-9h-9v9zm10 0h9v-9h-9v9zm10 0h9v-9h-9v9zm9-10v-9h-9v9h9zm-10 0v-9h-9v9h9zm-10 0v-9h-9v9h9zm-10 0v-9h-9v9h9zm-10 0v-9h-9v9h9zm-10 0v-9h-9v9h9zm-10 0v-9h-9v9h9zm-10 0v-9h-9v9h9zm-9-10h9v-9h-9v9zm10 0h9v-9h-9v9zm10 0h9v-9h-9v9zm10 0h9v-9h-9v9zm10 0h9v-9h-9v9zm10 0h9v-9h-9v9zm10 0h9v-9h-9v9zm10 0h9v-9h-9v9zm9-10v-9h-9v9h9zm-10 0v-9h-9v9h9zm-10 0v-9h-9v9h9zm-10 0v-9h-9v9h9zm-10 0v-9h-9v9h9zm-10 0v-9h-9v9h9zm-10 0v-9h-9v9h9zm-10 0v-9h-9v9h9zm-9-10h9v-9h-9v9zm10 0h9v-9h-9v9zm10 0h9v-9h-9v9zm10 0h9v-9h-9v9zm10 0h9v-9h-9v9zm10 0h9v-9h-9v9zm10 0h9v-9h-9v9zm10 0h9v-9h-9v9zm9-10v-9h-9v9h9zm-10 0v-9h-9v9h9zm-10 0v-9h-9v9h9zm-10 0v-9h-9v9h9zm-10 0v-9h-9v9h9zm-10 0v-9h-9v9h9zm-10 0v-9h-9v9h9zm-10 0v-9h-9v9h9zm-9-10h9v-9h-9v9zm10 0h9v-9h-9v9zm10 0h9v-9h-9v9zm10 0h9v-9h-9v9zm10 0h9v-9h-9v9zm10 0h9v-9h-9v9zm10 0h9v-9h-9v9zm10 0h9v-9h-9v9z\"/%3E%3Cpath d = \"M6 5V0H5v5H0v1h5v94h1V6h94V5H6z\" /% 3E % 3C/g%3E%3C/g%3E%3C/svg%3E";

        #endregion

        #region GlobalConstants

        public const string par_ErrorType = "";
        public const string par_ErrorModule = "";
        public const string par_ErrorMessage = "";
        public const string par_ErrorBy = "";

        #endregion

        public static string ConStr = ConfigurationManager.ConnectionStrings["Connection"].ConnectionString;
    }
}