using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace BN_HomeFinance.Resources
{
    public class GovernorateConstants
    {
        public const string col_GovernorateID = "GovernorateID";
        public const string col_Governorate = "Governorate";

        public const string par_GovernorateID = "@GovernorateID";
        public const string par_Governorate = "@Governorate";

        public const string Proc_GetGovernorate = "GET_GOVERNORATE";
        public const string Proc_ReadGovernorateByID = "ReadGovernorateByID";
        public const string Proc_CreateGovernorate = "CreateGovernorate";
        public const string Proc_DeleteGovernorateByID = "DeleteGovernorateByID";
        public const string Proc_UpdateGovernorateByID = "UpdateGovernorateByID";
    }

    public class Governorates : GovernorateFactory
    {
        public int GovernorateID { get; set; }
        public string Governorate { get; set; }
    }
    public class GovernorateCollection : GovernorateFactory
    {
        private List<Governorates> governorate = new List<Governorates>();
        public List<Governorates> Governorate
        {
            get { return governorate; }
            set { governorate = value; }
        }
        public int Count { get; set; }
    }
    public class GovernorateFactory
    {
        public GovernorateCollection GetAllGovernorates()
        {
            GovernorateCollection objCollection = new GovernorateCollection();
            objCollection.Count = 0;
            try
            {
                DataTable dt = Helper.ExecuteSPWithNoParameters(GovernorateConstants.Proc_GetGovernorate);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Governorates objGovernorate = new Governorates();
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        DataRow dr = dt.Rows[i];
                        objGovernorate.GovernorateID = Convert.ToInt32(dr[GovernorateConstants.col_GovernorateID]);
                        objGovernorate.Governorate = dr[GovernorateConstants.col_Governorate].ToString();

                        objCollection.Governorate.Add(objGovernorate);
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

        public bool CreateGovernorate(Governorates objGov)
        {
            bool b = false;
            try
            {
                objGov.GovernorateID = Convert.ToInt32(Helper.ExecuteSPScalarWithParameters(GovernorateConstants.Proc_CreateGovernorate, GovernorateConstants.par_Governorate,
                            objGov.Governorate));
            }
            catch (Exception ex)
            {

            }
            return b;
        }
        public DataTable ReadAllGovernorates()
        {
            DataTable dt = new DataTable();
            try
            {
                dt = Helper.ExecuteSPWithNoParameters(GovernorateConstants.Proc_GetGovernorate);
            }
            catch (Exception ex)
            {

            }
            return dt;
        }

        public Governorates ReadGovernorateByID(string ID)
        {
            Governorates objGov = new Governorates();
            try
            {
                if (objGov != null)
                {
                    DataTable dt = Helper.ExecuteSPWithParameters(GovernorateConstants.Proc_ReadGovernorateByID, GovernorateConstants.par_GovernorateID, ID);
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            DataRow dr = dt.Rows[i];
                            objGov.Governorate = dr[GovernorateConstants.col_Governorate].ToString();

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return objGov;
        }

        public void UpdateGovernorateByID(Governorates objGov)
        {
            if (objGov != null)
            {
                Helper.ExecuteSPScalarWithParameters(GovernorateConstants.Proc_UpdateGovernorateByID, GovernorateConstants.par_GovernorateID, Convert.ToString(objGov.GovernorateID),
                    GovernorateConstants.par_Governorate, objGov.Governorate
                );
            }
        }

        public void DeleteGovernorateByID(string ID)
        {
            Governorates objGov = new Governorates();
            try
            {
                if (objGov != null)
                {
                    Helper.ExecuteSPScalarWithParameters(GovernorateConstants.Proc_DeleteGovernorateByID, GovernorateConstants.par_GovernorateID, ID);
                }
            }
            catch
            {
                throw;
            }
        }
    }
}