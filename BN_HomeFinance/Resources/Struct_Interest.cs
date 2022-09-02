using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace BN_HomeFinance.Resources
{
    public class InterestRateConstants
    {
        #region Interest Rate Parameters
        public const string Par_ID = "@ID";
        public const string Par_TenureFrom = "@TenureFrom";
        public const string Par_TenureTo = "@TenureTo";
        public const string Par_InterestRate = "@InterestRate";
        public const string Par_Category = "@Category";
        public const string Par_Tenure = "@Tenure";
        #endregion

        #region Interest Rate Procs
        public const string Proc_CreateInterestRate = "CreateInterestRate";
        public const string Proc_DeleteInterestRateByID = "DeleteInterestRateByID";
        public const string Proc_GetAllInterestRates = "GetAllInterestRates";
        public const string Proc_UpdateInterestRateByID = "UpdateInterestRateByID";
        public const string Proc_GetInterestRateByID = "GetInterestRateByID";
        public const string Proc_GetFromToValuesForInterestRate = "GetFromToValuesForInterestRate";
        public const string Proc_GetInterestRate = "GetInterestRate";
        #endregion

        #region Interest tRate Columns
        public const string Col_TenureFrom = "TenureFrom";
        public const string Col_TenureTo = "TenureTo";
        public const string Col_InterestRate = "InterestRate";
        public const string Col_Category = "Category";
        public const string Col_Tenure = "Tenure";
        #endregion
    }

    public class InterestRate : InterestRateFactory
    {
        public string ID { get; set; }
        public string TenureFrom { get; set; }
        public string TenureTo { get; set; }
        public string Interest { get; set; }

        private PropertyTypes category = new PropertyTypes();
        public PropertyTypes Category
        {
            get { return category; }
            set { category = value; }
        }

        public int Tenure { get; set; }
    }

    public class InterestRateCollection : InterestRateFactory
    {
        private List<InterestRate> interestrate = new List<InterestRate>();
        public List<InterestRate> interestRate
        {
            get { return interestrate; }
            set { interestrate = value; }
        }
        public int Count { get; set; }
    }

    public class InterestRateFactory
    {
        public bool CreateInterestRate(InterestRate objIR)
        {
            bool b = false;
            try
            {
                objIR.ID = Convert.ToString(Helper.ExecuteSPScalarWithParameters(InterestRateConstants.Proc_CreateInterestRate, InterestRateConstants.Par_TenureFrom,
                            objIR.TenureFrom, InterestRateConstants.Par_TenureTo, objIR.TenureTo, InterestRateConstants.Par_InterestRate,
                            objIR.Interest, InterestRateConstants.Par_Category, Convert.ToString(objIR.Category.PropertyTypeID)));
            }
            catch (Exception ex)
            {

            }
            return b;
        }

        public DataTable GetAllInterestRates()
        {
            DataTable dt = new DataTable();
            try
            {
                dt = Helper.ExecuteSPWithNoParameters(InterestRateConstants.Proc_GetAllInterestRates);
            }
            catch (Exception ex)
            {

            }
            return dt;
        }

        public InterestRate GetInterestRateByID(string ID)
        {
            InterestRate objIR = new InterestRate();
            try
            {
                if (objIR != null)
                {
                    DataTable dt = Helper.ExecuteSPWithParameters(InterestRateConstants.Proc_GetInterestRateByID, InterestRateConstants.Par_ID, ID);
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            DataRow dr = dt.Rows[i];
                            objIR.Interest = dr[InterestRateConstants.Col_InterestRate].ToString();
                            objIR.TenureFrom = dr[InterestRateConstants.Col_TenureFrom].ToString();
                            objIR.TenureTo = dr[InterestRateConstants.Col_TenureTo].ToString();
                            objIR.Category.PropertyTypeID = Convert.ToInt32(dr[InterestRateConstants.Col_Category].ToString());
                            objIR.Category.PropertyType = dr[PropertyTypeConstants.col_PropertyType].ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return objIR;
        }

        public void UpdateInterestRateByID(InterestRate objIR)
        {
            if (objIR != null)
            {
                Helper.ExecuteSPScalarWithParameters(InterestRateConstants.Proc_UpdateInterestRateByID, InterestRateConstants.Par_ID, objIR.ID,
                    InterestRateConstants.Par_TenureFrom, objIR.TenureFrom, InterestRateConstants.Par_TenureTo, objIR.TenureTo,
                    InterestRateConstants.Par_InterestRate, objIR.Interest, InterestRateConstants.Par_Category, Convert.ToString(objIR.Category.PropertyTypeID)

                );
            }
        }

        public void DeleteInterestRateByID(string ID)
        {
            InterestRateConstants objIR = new InterestRateConstants();
            try
            {
                if (objIR != null)
                {
                    Helper.ExecuteSPScalarWithParameters(InterestRateConstants.Proc_DeleteInterestRateByID, InterestRateConstants.Par_ID, ID);
                }
            }
            catch
            {
                throw;
            }
        }

        public InterestRateCollection GetFromToValuesForInterestRate()
        {
            InterestRateCollection objCollection = new InterestRateCollection();

            objCollection.Count = 0;

            try
            {
                DataTable dt = Helper.ExecuteSPWithNoParameters(InterestRateConstants.Proc_GetFromToValuesForInterestRate);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    InterestRate objInterest = new InterestRate();
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        DataRow dr = dt.Rows[i];

                        objInterest.TenureFrom = dr[InterestRateConstants.Col_TenureFrom].ToString();
                        objInterest.TenureTo = dr[InterestRateConstants.Col_TenureTo].ToString();
                        objInterest.Category.PropertyTypeID = Convert.ToInt16(dr[InterestRateConstants.Col_Category]);

                        objCollection.interestRate.Add(objInterest);
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
    }
}