using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BN_HomeFinance.Resources;

namespace BN_HomeFinance
{
    public partial class SearchedProperties : System.Web.UI.Page
    {
        public string Properties = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session[Helper.Sessions.Properties.ToString()] != null)
                {
                    PropertyCollection properties = (PropertyCollection)Session[Helper.Sessions.Properties.ToString()];

                    if (properties != null && properties.Count > 0)
                    {
                        for (int i = 0; i < properties.Count; i++)
                        {
                            Properties += @"<div class='col-md-4'><div class='card-box-a card-shadow'><div class='img-box-a'><img src='" + properties.Properties[i].Image.ImageURL + @"' alt='' class='img-a img-fluid'></div>
                                      <div class='card-overlay'><div class='card-overlay-a-content'><div class='card-header-a'><h2 class='card-title-a'>
                                      <a href='" + Helper.pg_PropertyDetails + @"?" + Helper.QueryStrings.PropertyID.ToString() + @"=" + properties.Properties[i].PropertyID + @"'>" + properties.Properties[i].PropertyName + @"</a></h2></div>
                                      <div class='card-body-a'><div class='price-box d-flex'><span class='price-a'>" + properties.Properties[i].PropertyType.PropertyType + @" | OMR " + string.Format("{0:N}", properties.Properties[i].PropertyValue) + @"</span></div>
                                      <a href='" + Helper.pg_PropertyDetails + @"?" + Helper.QueryStrings.PropertyID.ToString() + @"=" + properties.Properties[i].PropertyID + @"' class='link-a'>Click here to view<span class='bi bi-chevron-right'></span></a></div>
                                      <div class='card-footer-a'><ul class='card-info d-flex justify-content-around'>
                                      <li><h4 class='card-info-title'>Area</h4><span>" + properties.Properties[i].PropertyAreaInMsq + @"m<sup>2</sup></span></li><li><h4 class='card-info-title'>Beds</h4><span>" + properties.Properties[i].PropertyBedrooms + @"</span></li>
                                      <li><h4 class='card-info-title'>Baths</h4><span>" + properties.Properties[i].PropertyBathrooms + @"</span></li><li><h4 class='card-info-title'>Garages</h4><span>" + properties.Properties[i].PropertyGarage + @"</span></li>
                                      </ul></div></div></div></div></div>";
                        }
                    }
                }
            }
            else
                return;
        }
    }
}