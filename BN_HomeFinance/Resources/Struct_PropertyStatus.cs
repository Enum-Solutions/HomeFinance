using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BN_HomeFinance.Resources
{
    public class PropertyStatusConstants
    {
        public const string col_PropertyStatusID = "PropertyStatusID";
        public const string col_PropertyStatus = "PropertyStatus";
        public const string par_PropertyStatusID = "@PropertyStatusID";

        public const string proc_GetPropertyStatus = "GET_PROPERTY_STATUS";
    }

    public class PropertyStatuses
    {
        public int PropertyStatusID { get; set; }
        public string PropertyStatus { get; set; }
    }
}