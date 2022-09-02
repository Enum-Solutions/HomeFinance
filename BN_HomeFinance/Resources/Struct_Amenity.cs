using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace BN_HomeFinance.Resources
{
    [Serializable]
    public class Amenity : AmenityFactory
    {
        public string AmenityID { get; set; }
        public string AmenityType { get; set; }
    }

    [Serializable]
    public class AmenityCollection : AmenityFactory
    {
        private List<Amenity> amenity = new List<Amenity>();
        public List<Amenity> Amenity
        {
            get { return amenity; }
            set { amenity = value; }
        }
        public int Count { get; set; }
    }

    public class AmenityConstants
    {
        public const string col_AmenityID = "AmenityID";
        public const string col_Amenity = "Amenity";
        public const string par_Amenity = "@Amenity";
        public const string par_AmenityID = "@AmenityID";

        public const string proc_GetAmenityByAmenityID = "GET_AMENITY_BY_AMENITY_ID";
        public const string proc_GetAmenities = "GET_AMENITIES";
        public const string proc_CreateAmenity = "CREATE_AMENITY";
        public const string proc_ReadAmenityByID = "ReadAmenityByID";
        public const string proc_DeleteAmenityByID = "DeleteAmenityByID";
        public const string proc_UpdateAmenityByID = "UpdateAmenityByID";

    }
    [Serializable]
    public class AmenityFactory
    {
        public AmenityCollection GetAmenities(string IDs)
        {
            AmenityCollection objAmenities = new AmenityCollection();

            try
            {
                DataTable dt = Helper.ExecuteSPWithParameters(AmenityConstants.proc_GetAmenityByAmenityID, AmenityConstants.par_AmenityID, IDs);

                if (dt != null && dt.Rows.Count > 0)
                {
                    objAmenities.Count = 0;

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        Amenity objAmenity = new Amenity();

                        objAmenity.AmenityID = Convert.ToString(dt.Rows[i][AmenityConstants.col_AmenityID]);
                        objAmenity.AmenityType = dt.Rows[i][AmenityConstants.col_Amenity].ToString();

                        if (objAmenity != null)
                        {
                            objAmenities.Amenity.Add(objAmenity);
                        }


                        objAmenities.Count++;
                    }
                }
            }
            catch
            {
                throw;
            }

            return objAmenities;
        }

        //public void CreateAmenity(string amenity)
        //{
        //    Helper.ExecuteSPScalarWithParameters(AmenityConstants.proc_CreateAmenity, AmenityConstants.col_Amenity, amenity);

        //}

        public bool CreateAmenity(Amenity objAmenity)
        {
            bool b = false;
            try
            {
                objAmenity.AmenityID = Convert.ToString(Helper.ExecuteSPScalarWithParameters(AmenityConstants.proc_CreateAmenity, AmenityConstants.par_Amenity, objAmenity.AmenityType));
            }
            catch (Exception ex)
            {

            }
            return b;
        }

        public Amenity ReadAmenityByID(string ID)
        {
            Amenity objAmenity = new Amenity();
            try
            {
                if (objAmenity != null)
                {
                    DataTable dt = Helper.ExecuteSPWithParameters(AmenityConstants.proc_ReadAmenityByID, AmenityConstants.par_AmenityID, ID);
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            DataRow dr = dt.Rows[i];
                            objAmenity.AmenityType = dr[AmenityConstants.col_Amenity].ToString();

                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return objAmenity;
        }

        public void UpdateAmenityByID(Amenity objAmenity)
        {
            if (objAmenity != null)
            {
                Helper.ExecuteSPScalarWithParameters(AmenityConstants.proc_UpdateAmenityByID, AmenityConstants.par_AmenityID, Convert.ToString(objAmenity.AmenityID),
                    AmenityConstants.par_Amenity, objAmenity.AmenityType
                );
            }
        }

        public void DeleteAmenityByID(string ID)
        {
            AmenityConstants objAmenity = new AmenityConstants();
            try
            {
                if (objAmenity != null)
                {
                    Helper.ExecuteSPScalarWithParameters(AmenityConstants.proc_DeleteAmenityByID, AmenityConstants.par_AmenityID, ID);
                }
            }
            catch
            {
                throw;
            }
        }
    }
}