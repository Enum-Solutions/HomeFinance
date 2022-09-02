using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace BN_HomeFinance.Resources
{
    public class TenureConstants
    {
        #region Parameters

        public const string Par_Value = "@Value";
        public const string Par_Tenure = "@Tenure";
        public const string Par_ID = "@ID";

        #endregion

        #region Procedure
        public const string Proc_GetTenureValue = "GET_TENURE_VALUE";
        public const string Proc_CreateTenure = "CreateTenure";
        public const string Proc_ReadAllTenure = "ReadAllTenure";
        public const string Proc_ReadTenureByTenureID = "ReadTenureByTenureID";
        public const string Proc_UpdateTenureByTenureID = "UpdateTenureByTenureID";
        public const string Proc_DeleteTenureByTenureID = "DeleteTenureByTenureID";
        #endregion

        #region Columns
        public const string Col_Value = "TenureValue";
        public const string Col_Tenure = "Tenure";
        #endregion
    }

    public class Tenure : TenureFactory
    {
        public string ID { get; set; }
        public int Value { get; set; }
        public string tenure { get; set; }
    }

    public class TenureFactory
    {
        public bool CreateTenure(Tenure objTenure)
        {
            bool b = false;
            try
            {
                objTenure.ID = Convert.ToString(Helper.ExecuteSPScalarWithParameters(TenureConstants.Proc_CreateTenure, TenureConstants.Par_Tenure,
                            objTenure.tenure));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return b;
        }

        public DataTable ReadAllTenure()
        {
            DataTable dt = new DataTable();
            try
            {
                dt = Helper.ExecuteSPWithNoParameters(TenureConstants.Proc_ReadAllTenure);
            }
            catch (Exception ex)
            {

            }
            return dt;
        }

        public Tenure ReadTenureByID(string ID)
        {
            Tenure objTenure = new Tenure();
            try
            {
                if (objTenure != null)
                {
                    DataTable dt = Helper.ExecuteSPWithParameters(TenureConstants.Proc_ReadTenureByTenureID, TenureConstants.Par_ID, ID);
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            DataRow dr = dt.Rows[i];
                            objTenure.tenure = dr[TenureConstants.Col_Tenure].ToString();

                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return objTenure;
        }

        public void UpdateTenureByID(Tenure objTenure)
        {
            if (objTenure != null)
            {
                Helper.ExecuteSPScalarWithParameters(TenureConstants.Proc_UpdateTenureByTenureID, TenureConstants.Par_ID, objTenure.ID,
                    TenureConstants.Par_Tenure, objTenure.tenure
                );
            }
        }

        public void DeleteTenureByID(string ID)
        {
            TenureConstants objTenure = new TenureConstants();
            try
            {
                if (objTenure != null)
                {
                    Helper.ExecuteSPScalarWithParameters(TenureConstants.Proc_DeleteTenureByTenureID, TenureConstants.Par_ID, ID);
                }
            }
            catch
            {
                throw;
            }
        }

        public Tenure GetLOV(string val)
        {
            DataTable dt = Helper.ExecuteSPWithParameters(TenureConstants.Proc_GetTenureValue, TenureConstants.Par_Value, val);
            Tenure objTenure = new Tenure();
            if (dt != null)
            {
                objTenure.Value = Convert.ToInt32(dt.Rows[0][TenureConstants.Col_Value]);
            }
            return objTenure;
        }
    }
}