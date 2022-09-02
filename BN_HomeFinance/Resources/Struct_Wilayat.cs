using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace BN_HomeFinance.Resources
{
    public class WilayatConstants
    {
        public const string col_WilayatID = "WilayatID";
        public const string col_Wilayat = "Wilayat";

        public const string par_WilayatID = "@WilayatID";
        public const string par_Wilayat = "@Wilayat";

        public const string proc_GetWilayatByGovernorate = "GET_WILAYAT_BY_GOVERNORATE";
        public const string proc_GetAllWilayats = "GetAllWilayats";
        public const string Proc_ReadWilayatByID = "ReadWilayatByID";
        public const string Proc_ReadAllWilayat = "ReadAllWilayat";
        public const string Proc_CreateWilayat = "CreateWilayat";
        public const string Proc_DeleteWilayatByID = "DeleteWilayatByID";
        public const string Proc_UpdateWilayatByID = "UpdateWilayatByID";
    }

    public class Wilayats : WilayatFactory
    {
        public int WilayatID { get; set; }

        public string WilayatIDs { get; set; }

        public string Wilayat { get; set; }

        private Governorates governorate = new Governorates();
        public Governorates Governorate
        {
            get { return governorate; }
            set { governorate = value; }
        }
    }

    public class WilayatCollection : WilayatFactory
    {
        private List<Wilayats> wilayat = new List<Wilayats>();
        public List<Wilayats> Wilayat
        {
            get { return wilayat; }
            set { wilayat = value; }
        }
        public int Count { get; set; }
    }

    public class WilayatFactory
    {
        public WilayatCollection GetWilayatByGovernorateID(int govtID)
        {

            WilayatCollection objCollection = new WilayatCollection();
            objCollection.Count = 0;
            try
            {
                DataTable dt = Helper.ExecuteSPWithParameters(WilayatConstants.proc_GetWilayatByGovernorate, GovernorateConstants.par_GovernorateID, Convert.ToString(govtID));
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Wilayats objWilayat = new Wilayats();
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        DataRow dr = dt.Rows[i];
                        objWilayat.WilayatID = Convert.ToInt32(dr[WilayatConstants.col_WilayatID]);
                        objWilayat.Wilayat = dr[WilayatConstants.col_Wilayat].ToString();

                        objCollection.Wilayat.Add(objWilayat);
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

        public bool CreateWilayat(Wilayats objWilayat)
        {
            bool b = false;
            try
            {
                objWilayat.WilayatID = Convert.ToInt32(Helper.ExecuteSPScalarWithParameters(WilayatConstants.Proc_CreateWilayat, WilayatConstants.par_Wilayat,
                            objWilayat.Wilayat, GovernorateConstants.par_GovernorateID, Convert.ToString(objWilayat.Governorate.GovernorateID)));
            }
            catch (Exception ex)
            {

            }
            return b;
        }

        public void DeleteWilayatByID(string ID)
        {
            Wilayats objWilayat = new Wilayats();
            try
            {
                if (objWilayat != null)
                {
                    Helper.ExecuteSPScalarWithParameters(WilayatConstants.Proc_DeleteWilayatByID, WilayatConstants.par_WilayatID, ID);
                }
            }
            catch
            {
                throw;
            }
        }

        public DataTable ReadAllWilayat()
        {
            DataTable dt = new DataTable();
            try
            {
                dt = Helper.ExecuteSPWithNoParameters(WilayatConstants.Proc_ReadAllWilayat);
            }
            catch (Exception ex)
            {

            }
            return dt;
        }

        public Wilayats ReadWilayatByID(string ID)
        {
            Wilayats objWilayat = new Wilayats();

            try
            {
                if (objWilayat != null)
                {
                    DataTable dt = Helper.ExecuteSPWithParameters(WilayatConstants.Proc_ReadWilayatByID, WilayatConstants.par_WilayatID, ID);
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            DataRow dr = dt.Rows[i];
                            objWilayat.Wilayat = dr[WilayatConstants.col_Wilayat].ToString();
                            objWilayat.Governorate.GovernorateID = Convert.ToInt32(dr[GovernorateConstants.col_GovernorateID].ToString());
                            objWilayat.Governorate.Governorate = dr[GovernorateConstants.col_Governorate].ToString();
                            objWilayat.Wilayat = dr[WilayatConstants.col_Wilayat].ToString();

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return objWilayat;
        }

        public void UpdateWilayatByID(Wilayats objWilayat)
        {
            if (objWilayat != null)
            {
                Helper.ExecuteSPScalarWithParameters(WilayatConstants.Proc_UpdateWilayatByID, WilayatConstants.par_WilayatID, Convert.ToString(objWilayat.WilayatID),
                    WilayatConstants.par_Wilayat, objWilayat.Wilayat, GovernorateConstants.par_GovernorateID, Convert.ToString(objWilayat.Governorate.GovernorateID)
                );
            }
        }
    }
}