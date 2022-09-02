using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Text;
using System.Data.SqlClient;
using System.Configuration;

namespace BN_HomeFinance.Resources
{
    public class PropertyConstants
    {
        public const string col_PropertyID = "PropertyID";
        public const string col_PropertyName = "PropertyName";
        public const string col_PropertyAddress = "PropertyAddress";
        public const string col_PropertyVideo = "PropertyVideo";
        public const string col_PropertyDescription = "PropertyDescription";
        public const string col_PropertyQuickSummary = "PropertyQuickSummary";
        public const string col_PropertyFloorPlan = "PropertyFloorPlan";
        public const string col_PropertyGeoLocation = "PropertyGeoLocation";
        public const string col_PropertyGarage = "PropertyGarage";
        public const string col_PropertyAreaInMsq = "PropertyAreaInMsq";
        public const string col_PropertyStakeholder = "PropertyStakeholder";
        public const string col_PropertyBedrooms = "PropertyBedrooms";
        public const string col_PropertyBathrooms = "PropertyBathrooms";
        public const string col_PropertyValue = "PropertyValue";
        public const string col_DateCreated = "DateCreated";
        public const string col_CreatedBy = "CreatedBy";
        /* Foreign Key Fields *************** START*/
        public const string col_PropertyTypeID = "PropertyTypeID";
        public const string col_PropertyStatusID = "PropertyStatusID";
        public const string col_PropertyAmenityIDs = "AmenityID";
        public const string col_PorpertyWilayatID = "WilayatID";
        public const string col_PropertyGovernorateID = "GovernorateID";
        public const string col_PropertyImageID = "ImageID";
        /* Foreign Key Fields *************** END */

        public const string par_PropertyID = "@PropertyID";
        public const string par_PropertyName = "@PropertyName";
        public const string par_PropertyAddress = "@PropertyAddress";
        public const string par_PropertyVideo = "@PropertyVideo";
        public const string par_PropertyDescription = "@PropertyDescription";
        public const string par_PropertyQuickSummary = "@PropertyQuickSummary";
        public const string par_PropertyFloorPlan = "@PropertyFloorPlan";
        public const string par_PropertyGeoLocation = "@PropertyGEOLocation";
        public const string par_PropertyGarage = "@PropertyGarage";
        public const string par_PropertyAreaInMsq = "@PropertyAreaInMsq";
        public const string par_PropertyStakeholder = "@PropertyStakeholders";
        public const string par_PropertyBedrooms = "@PropertyBedrooms";
        public const string par_PropertyBathrooms = "@PropertyBathrooms";
        public const string par_PropertyValue = "@PropertyValue";
        public const string par_PropertyAmenities = "@AmenityID";
        public const string par_PropertyWilayatIDs = "@WilayatIDs";
        public const string par_PropertyWilayat = "@Wilayat";
        public const string par_PropertyGovernorateID = "@GovernorateID";
        public const string par_PropertyGovernorate = "@Governorate";
        public const string par_PropertyTypeID = "@PropertyTypeID";
        public const string par_PropertyType = "@PropertyType";
        public const string par_PropertyStatusID = "@PropertyStatusID";
        public const string par_PropertyStatus = "@PropertyStatus";
        public const string par_PropertyImage = "@ImageID";
        public const string par_CreatedBy = "@UserID";
        public const string par_AreaMin = "@AreaMin";
        public const string par_AreaMax = "@AreaMax";
        public const string par_ValueMin = "@ValueMin";
        public const string par_ValueMax = "@ValueMax";

        public const string proc_CreateProperty = "CREATE_PROPERTY";
        public const string proc_GetAllProperties = "GET_ALL_PROPERTIES";
        public const string proc_GetAllProducts = "GetAllProducts";
        public const string proc_GetPropertyByPropertyID = "GET_PROPERTY_BY_PROPERTY_ID";
        public const string proc_InsertPropertyPermissions = "";
        public const string proc_GetPropertyDetailsWithUserName = "";
        public const string proc_DeleteProperty = "DeleteProperty";
        public const string proc_UpdateAnyParameterOfPropertyByPropertyID = "UPDATE_ANY_PARAMETER_OF_PROPERTY_BY_PROPERTY_ID";
        public const string proc_CreatePropertyType = "CreatePropertyType";
        public const string proc_SearchProperty = "SEARCH_PROPERTY";
        public const string proc_GetMyProperties = "GetMyProperties";
        public const string proc_GetPropertiesByUserID = "GET_PROPERTIES_BY_USER_ID";
        public const string proc_GetHotProperties = "GetHotProperties";
        public const string proc_GetMaxPropertyValue = "GetMaxPropertyValue";
        public const string proc_GetMaxPropertyArea = "GetMaxPropertyArea";
    }

    public class Properties : PropertyFactory
    {
        public int PropertyID { get; set; }
        public string PropertyName { get; set; }
        public string PropertyAddress { get; set; }
        public string PropertyVideo { get; set; }
        public string PropertyDescription { get; set; }
        public string PropertyQuickSummary { get; set; }
        public string PropertyFloorPlan { get; set; }
        public string PropertyGeoLocation { get; set; }
        public string PropertyAreaInMsq { get; set; }
        public string AreaMin { get; set; }
        public string AreaMax { get; set; }
        public string PropertyStakeholder { get; set; }
        public int PropertyValue { get; set; }
        public string ValueMin { get; set; }
        public string ValueMax { get; set; }
        public int PropertyBedrooms { get; set; }
        public string Bedrooms { get; set; }
        public int PropertyBathrooms { get; set; }
        public int PropertyGarage { get; set; }

        private PropertyTypes propertytype = new PropertyTypes();
        public PropertyTypes PropertyType
        {
            get { return propertytype; }
            set { propertytype = value; }
        }

        private BNImage image = new BNImage();
        public BNImage Image
        {
            get { return image; }
            set { image = value; }
        }

        private BNImageCollection images = new BNImageCollection();
        public BNImageCollection Images
        {
            get { return images; }
            set { images = value; }
        }

        private Amenity amenity = new Amenity();
        public Amenity Amenity
        {
            get { return amenity; }
            set { amenity = value; }
        }

        private AmenityCollection amenities = new AmenityCollection();
        public AmenityCollection Amenities
        {
            get { return amenities; }
            set { amenities = value; }
        }

        private Wilayats propertyWilayat = new Wilayats();
        public Wilayats PropertyWilayat
        {
            get { return propertyWilayat; }
            set { propertyWilayat = value; }
        }

        private Governorates propertygovernorate = new Governorates();
        public Governorates PropertyGovernorate
        {
            get { return propertygovernorate; }
            set { propertygovernorate = value; }
        }
        public string ConcatinatedIDs { get; set; }
        private BNUser creator = new BNUser();
        public BNUser Creator
        {
            get { return creator; }
            set { creator = value; }
        }
        public DateTime DateCreated { get; set; }
    }

    public class PropertyCollection : PropertyFactory
    {
        private List<Properties> property = new List<Properties>();
        public List<Properties> Properties
        {
            get { return property; }
            set { property = value; }
        }
        public int Count { get; set; }
    }

    public class PropertyFactory
    {
        public bool CreateProperty(Properties pObj)
        {
            bool b = false;

            if (pObj != null)
            {
                pObj.PropertyID = Convert.ToInt32(Helper.ExecuteSPScalarWithParameters(PropertyConstants.proc_CreateProperty, PropertyConstants.par_PropertyName,
                     pObj.PropertyName, PropertyConstants.par_PropertyAddress, pObj.PropertyAddress, PropertyConstants.par_PropertyDescription, pObj.PropertyDescription,
                     PropertyConstants.par_PropertyGeoLocation, pObj.PropertyGeoLocation, PropertyConstants.par_PropertyGarage,
                     Convert.ToString(pObj.PropertyGarage), PropertyConstants.par_PropertyAreaInMsq, pObj.PropertyAreaInMsq,
                     PropertyConstants.par_PropertyValue, Convert.ToString(pObj.PropertyValue), PropertyConstants.par_PropertyBedrooms,
                     Convert.ToString(pObj.PropertyBedrooms), PropertyConstants.par_PropertyBathrooms, Convert.ToString(pObj.PropertyBathrooms),
                     GovernorateConstants.par_GovernorateID, Convert.ToString(pObj.PropertyGovernorate.GovernorateID), WilayatConstants.par_WilayatID,
                     Convert.ToString(pObj.PropertyWilayat.WilayatID), PropertyTypeConstants.par_PropertyTypeID,
                     Convert.ToString(pObj.PropertyType.PropertyTypeID), BNImage_Constants.Par_ImageID, Convert.ToString(pObj.ConcatinatedIDs),
                     AmenityConstants.par_AmenityID, Convert.ToString(pObj.Amenity.AmenityID), PropertyConstants.par_CreatedBy, pObj.Creator.UserID

                     ));
            }


            return b;
        }

        public Properties GetPropertyByPropertyID(string ID)
        {
            Properties objProperty = new Properties();

            try
            {
                if (objProperty != null)
                {
                    DataTable dt = Helper.ExecuteSPWithParameters(PropertyConstants.proc_GetPropertyByPropertyID, PropertyConstants.par_PropertyID, ID);
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            DataRow dr = dt.Rows[i];
                            objProperty.PropertyID = Convert.ToInt32(dr[PropertyConstants.col_PropertyID]);
                            objProperty.PropertyName = dr[PropertyConstants.col_PropertyName].ToString();
                            objProperty.PropertyAddress = dr[PropertyConstants.col_PropertyAddress].ToString();
                            objProperty.PropertyVideo = dr[PropertyConstants.col_PropertyVideo].ToString();
                            objProperty.PropertyDescription = dr[PropertyConstants.col_PropertyDescription].ToString();
                            objProperty.PropertyGeoLocation = dr[PropertyConstants.col_PropertyGeoLocation].ToString();
                            objProperty.PropertyGarage = Convert.ToInt32(dr[PropertyConstants.col_PropertyGarage]);
                            objProperty.PropertyAreaInMsq = dr[PropertyConstants.col_PropertyAreaInMsq].ToString();
                            objProperty.PropertyValue = Convert.ToInt32(dr[PropertyConstants.col_PropertyValue]);
                            objProperty.PropertyBedrooms = Convert.ToInt32(dr[PropertyConstants.col_PropertyBedrooms]);
                            objProperty.PropertyBathrooms = Convert.ToInt32(dr[PropertyConstants.col_PropertyBathrooms]);
                            objProperty.PropertyWilayat.Wilayat = Convert.ToString(dr[WilayatConstants.col_Wilayat]);
                            objProperty.PropertyWilayat.WilayatID = Convert.ToInt32(dr[PropertyConstants.col_PorpertyWilayatID]);
                            objProperty.PropertyGovernorate.Governorate = Convert.ToString(dr[GovernorateConstants.col_Governorate]);
                            objProperty.PropertyGovernorate.GovernorateID = Convert.ToInt32(dr[PropertyConstants.col_PropertyGovernorateID]);
                            objProperty.PropertyType.PropertyType = Convert.ToString(dr[PropertyTypeConstants.col_PropertyType]);
                            objProperty.PropertyType.PropertyTypeID = Convert.ToInt32(dr[PropertyTypeConstants.col_PropertyTypeID]);
                            objProperty.Amenity.AmenityID = dr[PropertyConstants.col_PropertyAmenityIDs].ToString();
                            objProperty.Image.ImageID = Convert.ToString(dr[PropertyConstants.col_PropertyImageID].ToString());
                            objProperty.Creator.UserID = dr[PropertyConstants.col_CreatedBy].ToString();

                            if (objProperty.Image.ImageID != "")
                            {
                                objProperty.Images = objProperty.Images.GetImages(objProperty.Image.ImageID);
                            }
                        }
                    }
                }
            }
            catch
            {
                throw;
            }
            return objProperty;
        }

        public PropertyCollection GetAllProperties()
        {
            PropertyCollection objCollection = new PropertyCollection();
            objCollection.Count = 0;
            try
            {
                DataTable dt = Helper.ExecuteSPWithNoParameters(PropertyConstants.proc_GetAllProperties);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Properties objProperty = new Properties();
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        DataRow dr = dt.Rows[i];
                        objProperty.PropertyID = Convert.ToInt32(dr[PropertyConstants.col_PropertyID]);
                        objProperty.PropertyName = dr[PropertyConstants.col_PropertyName].ToString();
                        objProperty.PropertyGarage = Convert.ToInt32(dr[PropertyConstants.col_PropertyGarage]);
                        objProperty.PropertyAreaInMsq = dr[PropertyConstants.col_PropertyAreaInMsq].ToString();
                        objProperty.PropertyValue = Convert.ToInt32(dr[PropertyConstants.col_PropertyValue]);
                        objProperty.PropertyBedrooms = Convert.ToInt32(dr[PropertyConstants.col_PropertyBedrooms]);
                        objProperty.PropertyBathrooms = Convert.ToInt32(dr[PropertyConstants.col_PropertyBathrooms]);
                        objProperty.PropertyType.PropertyTypeID = Convert.ToInt32(dr[PropertyConstants.col_PropertyTypeID]);
                        objProperty.PropertyType.PropertyType = dr[PropertyTypeConstants.col_PropertyType].ToString();
                        objProperty.Image.ImageID = Convert.ToString(dr[PropertyConstants.col_PropertyImageID]);
                        objProperty.Image.ImageData = (byte[])dr[BNImage_Constants.Col_ImageData];

                        if (objProperty.Image.ImageData != null)
                        {
                            objProperty.Image.ImageURL = Helper.ConvertUrlFromByteArray(objProperty.Image.ImageData);
                        }
                        else
                        {
                            objProperty.Image.ImageURL = Helper.pc_NoProperty;
                        }
                        objCollection.Properties.Add(objProperty);
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

        public DataTable GetAllPropertiesDatatable()
        {
            DataTable dt = new DataTable();

            try
            {
                dt = Helper.ExecuteSPWithNoParameters(PropertyConstants.proc_GetAllProperties);
            }
            catch
            {
                throw;
            }

            return dt;
        }

        public void UpdatePropertyByPropertyID(Properties pObj)
        {
            if (pObj != null)
            {
                Helper.ExecuteSPScalarWithParameters(PropertyConstants.proc_UpdateAnyParameterOfPropertyByPropertyID, PropertyConstants.par_PropertyID,
                Convert.ToString(pObj.PropertyID), PropertyConstants.par_PropertyName,
                pObj.PropertyName, PropertyConstants.par_PropertyAddress, pObj.PropertyAddress, PropertyConstants.par_PropertyVideo,
                pObj.PropertyVideo, PropertyConstants.par_PropertyDescription, pObj.PropertyDescription,
                PropertyConstants.par_PropertyGeoLocation, pObj.PropertyGeoLocation, PropertyConstants.par_PropertyGarage,
                Convert.ToString(pObj.PropertyGarage), PropertyConstants.par_PropertyAreaInMsq, pObj.PropertyAreaInMsq,
                PropertyConstants.par_PropertyValue,
                Convert.ToString(pObj.PropertyValue), PropertyConstants.par_PropertyBedrooms, Convert.ToString(pObj.PropertyBedrooms),
                PropertyConstants.par_PropertyBathrooms, Convert.ToString(pObj.PropertyBathrooms), WilayatConstants.par_WilayatID,
                Convert.ToString(pObj.PropertyWilayat.WilayatID), GovernorateConstants.par_GovernorateID, Convert.ToString(pObj.PropertyGovernorate.GovernorateID),
                PropertyTypeConstants.par_PropertyTypeID, Convert.ToString(pObj.PropertyType.PropertyTypeID),
                BNImage_Constants.Par_ImageID, Convert.ToString(pObj.ConcatinatedIDs), AmenityConstants.par_AmenityID,
                Convert.ToString(pObj.Amenity.AmenityID)
                );
            }
        }

        public bool DeletePropertyByPropertyID(string ID)
        {
            bool b = false;

            try
            {
                Helper.ExecuteSPScalarWithParameters(PropertyConstants.proc_DeleteProperty, PropertyConstants.par_PropertyID, ID);
                b = true;
            }
            catch
            {
                throw;
            }

            return b;
        }

        public void CreatePropertyType(string propertyType)
        {
            if (propertyType != null)
            {
                Helper.ExecuteSPScalarWithParameters(PropertyConstants.proc_CreatePropertyType, PropertyConstants.par_PropertyType, propertyType);
            }
        }

        public DataTable GetMyPropertiesDatatable(string UserID)
        {
            DataTable dt = new DataTable();

            try
            {
                dt = Helper.ExecuteSPWithParameters(PropertyConstants.proc_GetMyProperties, User_Constants.Par_UserID, UserID);
            }
            catch
            {
                throw;
            }

            return dt;
        }

        public PropertyCollection SearchProperty(string governorateID, string wilayatIDs, string typeIDs, string bedrooms, string areaMin, string areaMax, string valMin, string valMax)
        {
            PropertyCollection searchCollection = new PropertyCollection();
            searchCollection.Count = 0;
            try
            {
                DataTable dt = Helper.ExecuteSPWithParameters(PropertyConstants.proc_SearchProperty, PropertyConstants.par_PropertyGovernorateID, governorateID,
                    PropertyConstants.par_PropertyWilayatIDs, wilayatIDs, PropertyTypeConstants.par_PropertyTypeID, typeIDs, PropertyConstants.par_PropertyBedrooms, bedrooms,
                    PropertyConstants.par_AreaMin, areaMin, PropertyConstants.par_AreaMax, areaMax, PropertyConstants.par_ValueMin, valMin, PropertyConstants.par_ValueMax,
                    valMax
                    );
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Properties objProperty = new Properties();
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        DataRow dr = dt.Rows[i];
                        objProperty.PropertyID = Convert.ToInt32(dr[PropertyConstants.col_PropertyID]);
                        objProperty.PropertyName = dr[PropertyConstants.col_PropertyName].ToString();
                        objProperty.PropertyAreaInMsq = dr[PropertyConstants.col_PropertyAreaInMsq].ToString();
                        objProperty.PropertyValue = Convert.ToInt32(dr[PropertyConstants.col_PropertyValue]);
                        try { objProperty.Bedrooms = Convert.ToString(dr[PropertyConstants.col_PropertyBedrooms]); } catch { objProperty.Bedrooms = "0"; };
                        try { objProperty.PropertyBathrooms = Convert.ToInt32(dr[PropertyConstants.col_PropertyBathrooms]); } catch { objProperty.PropertyBathrooms = 0; };
                        try { objProperty.PropertyBathrooms = Convert.ToInt32(dr[PropertyConstants.col_PropertyBathrooms]); } catch { objProperty.PropertyBathrooms = 0; }
                        objProperty.PropertyType.PropertyTypeID = Convert.ToInt32(dr[PropertyTypeConstants.col_PropertyTypeID]);
                        objProperty.PropertyType.PropertyType = Convert.ToString(dr[PropertyTypeConstants.col_PropertyType]);

                        objProperty.Image.ImageID = Convert.ToString(dr[BNImage_Constants.Col_ImageID]);
                        objProperty.Image.ImageData = (byte[])dr[BNImage_Constants.Col_ImageData];

                        if (objProperty.Image.ImageData != null)
                        {
                            objProperty.Image.ImageURL = Helper.ConvertUrlFromByteArray(objProperty.Image.ImageData);
                        }

                        searchCollection.Properties.Add(objProperty);
                        searchCollection.Count++;
                    }
                }
            }
            catch
            {
                throw;
            }

            return searchCollection;
        }

        public PropertyCollection GetRecentProperties()
        {
            PropertyCollection objCollection = new PropertyCollection();

            objCollection.Count = 0;

            try
            {
                DataTable dt = Helper.ExecuteSPWithNoParameters(PropertyConstants.proc_GetHotProperties);

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Properties objProperty = new Properties();

                    if (dt != null && dt.Rows.Count > 0)
                    {
                        DataRow dr = dt.Rows[i];

                        objProperty.PropertyID = Convert.ToInt32(dr[PropertyConstants.col_PropertyID]);
                        objProperty.PropertyName = dr[PropertyConstants.col_PropertyName].ToString();
                        objProperty.PropertyAddress = dr[PropertyConstants.col_PropertyAddress].ToString();
                        objProperty.PropertyValue = Convert.ToInt32(dr[PropertyConstants.col_PropertyValue]);
                        objProperty.Image.ImageID = Convert.ToString(dr[PropertyConstants.col_PropertyImageID]);

                        if (objProperty.Image.ImageID.Contains(","))
                        {
                            objProperty.Image.ImageID = objProperty.Image.ImageID.Split(',')[0];
                        }

                        objProperty.Image = objProperty.Image.GetImage(objProperty.Image.ImageID);

                        objCollection.Properties.Add(objProperty);
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

        public PropertyCollection GetPropertiesByUserIDForBuilderProfile(string userID)
        {
            PropertyCollection objCollection = new PropertyCollection();
            objCollection.Count = 0;
            try
            {
                DataTable dt = Helper.ExecuteSPWithParameters(PropertyConstants.proc_GetPropertiesByUserID, PropertyConstants.par_CreatedBy, userID);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Properties objProperty = new Properties();
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        DataRow dr = dt.Rows[i];
                        objProperty.PropertyID = Convert.ToInt32(dr[PropertyConstants.col_PropertyID]);
                        objProperty.PropertyName = dr[PropertyConstants.col_PropertyName].ToString();
                        objProperty.PropertyAddress = dr[PropertyConstants.col_PropertyAddress].ToString();
                        objProperty.PropertyVideo = dr[PropertyConstants.col_PropertyVideo].ToString();
                        objProperty.PropertyDescription = dr[PropertyConstants.col_PropertyDescription].ToString();
                        objProperty.PropertyGeoLocation = dr[PropertyConstants.col_PropertyGeoLocation].ToString();
                        objProperty.PropertyGarage = Convert.ToInt32(dr[PropertyConstants.col_PropertyGarage]);
                        objProperty.PropertyAreaInMsq = dr[PropertyConstants.col_PropertyAreaInMsq].ToString();
                        objProperty.PropertyValue = Convert.ToInt32(dr[PropertyConstants.col_PropertyValue]);
                        objProperty.PropertyBedrooms = Convert.ToInt32(dr[PropertyConstants.col_PropertyBedrooms]);
                        objProperty.PropertyBathrooms = Convert.ToInt32(dr[PropertyConstants.col_PropertyBathrooms]);
                        objProperty.PropertyWilayat.Wilayat = Convert.ToString(dr[WilayatConstants.col_Wilayat]);
                        objProperty.PropertyWilayat.WilayatID = Convert.ToInt32(dr[PropertyConstants.col_PorpertyWilayatID]);
                        objProperty.PropertyGovernorate.Governorate = Convert.ToString(dr[GovernorateConstants.col_Governorate]);
                        objProperty.PropertyGovernorate.GovernorateID = Convert.ToInt32(dr[PropertyConstants.col_PropertyGovernorateID]);
                        objProperty.PropertyType.PropertyType = Convert.ToString(dr[PropertyTypeConstants.col_PropertyType]);
                        objProperty.PropertyType.PropertyTypeID = Convert.ToInt32(dr[PropertyTypeConstants.col_PropertyTypeID]);
                        objProperty.Amenity.AmenityID = dr[PropertyConstants.col_PropertyAmenityIDs].ToString();
                        objProperty.Image.ImageID = Convert.ToString(dr[PropertyConstants.col_PropertyImageID].ToString());
                        objProperty.Creator.UserID = dr[PropertyConstants.col_CreatedBy].ToString();
                        objCollection.Properties.Add(objProperty);
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