using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace BN_HomeFinance.Resources
{
    public class PropertyTypeConstants
    {
        public const string col_PropertyTypeID = "PropertyTypeID";
        public const string col_PropertyType = "PropertyType";

        public const string par_PropertyTypeID = "@PropertyTypeID";
        public const string par_PropertyType = "@PropertyType";

        public const string proc_GetPropertyType = "GET_PROPERTY_TYPE"; public const string Proc_CreatePropertyType = "CreatePropertyType";
        public const string Proc_ReadPropertyTypeByID = "ReadPropertyTypeByID";
        public const string Proc_DeletePropertyTypeByID = "DeletePropertyTypeByID";
        public const string Proc_UpdatePropertyTypeByID = "UpdatePropertyTypeByID";
    }

    public class PropertyTypes : PropertyTypes_Factory
    {
        public int PropertyTypeID { get; set; }
        public string PropertyTypeIDs { get; set; }
        public string PropertyType { get; set; }
    }

    public class PropertyTypes_Factory
    {
        public bool CreatePropertyType(PropertyTypes objPropertyType)
        {
            bool b = false;
            try
            {
                objPropertyType.PropertyTypeID = Convert.ToInt32(Helper.ExecuteSPScalarWithParameters(PropertyTypeConstants.Proc_CreatePropertyType, PropertyTypeConstants.par_PropertyType,
                            objPropertyType.PropertyType));
            }
            catch (Exception ex)
            {

            }
            return b;
        }

        public DataTable ReadAllPropertyType()
        {
            DataTable dt = new DataTable();
            try
            {
                dt = Helper.ExecuteSPWithNoParameters(PropertyTypeConstants.proc_GetPropertyType);
            }
            catch (Exception ex)
            {

            }
            return dt;
        }

        public PropertyTypes ReadPropertyTypeByID(string ID)
        {
            PropertyTypes objPropertyType = new PropertyTypes();
            try
            {
                if (objPropertyType != null)
                {
                    DataTable dt = Helper.ExecuteSPWithParameters(PropertyTypeConstants.Proc_ReadPropertyTypeByID, PropertyTypeConstants.par_PropertyTypeID, ID);
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            DataRow dr = dt.Rows[i];
                            objPropertyType.PropertyType = dr[PropertyTypeConstants.col_PropertyType].ToString();

                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return objPropertyType;
        }

        public void UpdatePropertyTypeByID(PropertyTypes objPropertyType)
        {
            if (objPropertyType != null)
            {
                Helper.ExecuteSPScalarWithParameters(PropertyTypeConstants.Proc_UpdatePropertyTypeByID, PropertyTypeConstants.par_PropertyTypeID, Convert.ToString(objPropertyType.PropertyTypeID),
                    PropertyTypeConstants.par_PropertyType, objPropertyType.PropertyType
                );
            }
        }

        public void DeletePropertyTypeByID(string ID)
        {
            PropertyTypeConstants objPropertyType = new PropertyTypeConstants();
            try
            {
                if (objPropertyType != null)
                {
                    Helper.ExecuteSPScalarWithParameters(PropertyTypeConstants.Proc_DeletePropertyTypeByID, PropertyTypeConstants.par_PropertyTypeID, ID);
                }
            }
            catch
            {
                throw;
            }
        }
    }
}