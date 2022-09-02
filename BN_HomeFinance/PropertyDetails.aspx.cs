using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using BN_HomeFinance.Resources;

namespace BN_HomeFinance
{
    public partial class PropertyDetails : System.Web.UI.Page
    {
        public string HTML = "", var_propertyID = "";
        public int slideCounter = 1;

        Properties objProperty = new Properties();

        PropertyConstants objPropertyConst = new PropertyConstants();

        AmenityCollection objAmenities = new AmenityCollection();

        BNImageCollection objImages = new BNImageCollection();

        BNUser objUser = new BNUser();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                #region Calculator

                ddlPropertyType.DataSource = Helper.GetData(PropertyTypeConstants.proc_GetPropertyType, null);
                ddlPropertyType.DataBind();

                ddlTenure.DataSource = Helper.GetData(CalculatorConstants.proc_GetTenure, null);
                ddlTenure.DataBind();
                ddlTenure.Items.Insert(0, new ListItem("Select Tenure", "0"));

                #endregion

                Properties objProperty = new Properties();

                string AmenityIDs = "";

                if (Request.QueryString[Helper.QueryStrings.PropertyID.ToString()] != null)
                {
                    string PropertyID = Request.QueryString[Helper.QueryStrings.PropertyID.ToString()].ToString();

                    objProperty = objProperty.GetPropertyByPropertyID(PropertyID);
                    var_propertyID = Convert.ToString(objProperty.PropertyID);
                    AmenityIDs = objProperty.Amenity.AmenityID;
                    objImages = objImages.GetImages(objProperty.Image.ImageID);
                    objUser = objUser.GetUserWithUserID(objProperty.Creator.UserID);
                    userLogo.ImageUrl = objUser.Picture.ImageURL;
                    lblName.Text = objUser.FullName;
                    lblAddress.Text = objUser.Address + ", " + objUser.City + ", " + objUser.Country + ".";
                    lblPhone.Text = objUser.Contact;
                    lblName.Style.Add("font-weight", "bold");
                    geo.Src = objProperty.PropertyGeoLocation;

                    ddlPropertyType.SelectedValue = objProperty.PropertyType.PropertyTypeID.ToString();
                    ddlPropertyType.Enabled = false;

                    txtTotalValue.Text = objProperty.PropertyValue.ToString();
                    txtTotalValue.Enabled = false;

                    if (AmenityIDs != "")
                        objAmenities = objAmenities.GetAmenities(AmenityIDs);

                    HTML = @"<div class='row grey-title-space'><div col-md-12>
                       <h2 class='page-headings'>" + objProperty.PropertyName + @"</h3><h2 class='heading-desc'>" + objProperty.PropertyAddress + @"</h3>
                       </div></div><div class='row'><div class='slideshowcontainer'> 
                                        ";
                    for (int i = 0; i < objImages.Count; i++)
                    {
                        HTML += @"<div class='mySlides'><div class='row'><div class= 'col-12 col-sm-12 col-md-12 col-ld-12'>
                            <div class='numbertext'> " + (i + 1) + @"/ " + objImages.Count + @"</div><img src = '" + objImages.BNImages[i].ImageURL + @"'
                            class='img-fluid' style='width:100%;height:76vh'/></div></div></div>";

                        slideCounter++;
                    }
                    HTML += @"<a class='prev' onclick='plusSlides(-1)'>❮</a><a class='next' onclick='plusSlides(1)'>❯</a><div class='innerContainer'>
                        <div class='row' style='margin: inherit;padding:20px; background-color:black;border-radius:35px;width:100%!important;overflow:hidden'>
                                        ";
                    HTML += @"<div class='col-12 col-sm-12 col-md-1 col-lg-1 gallery-trigger-container'><a id='prev' class='gallery-prev'> ❮ </a></div>
                        <div class='col-12 col-sm-12 col-md-10 col-lg-10 scroll' id='gallery'><table id='table-images' class='table-responsive'><tr>";

                    for (int i = 0; i < objImages.Count; i++)
                    {
                        HTML += "<td><img class='demo cursor' src='" + objImages.BNImages[i].ImageURL + @"' style='width:150px !important;height:75px;object-fit:cover;border-radius:20px;' onclick='currentSlide(" + (i + 1) + ")' alt='The Woods'></td>";
                    }

                    HTML += @"</tr></table></div>
                        <div class='col-12 col-sm-12 col-md-1 col-lg-1 gallery-trigger-container'><a id='next' class='gallery-next'> ❯ </a></div></div></div></div></div>";

                    HTML += @"<div class='innerContainer'><div class='row new-section'><div class='col-md-6'>
                        <h4 class='body-headings' style='color:#6E288C'>Property Value: " + HttpUtility.HtmlEncode(string.Format("{0:N}", objProperty.PropertyValue)) + @" OMR </h4></div>
                        <div class= 'col-md-6'><input type='button' data-bs-toggle='modal' data-bs-target='#exampleModal' style='margin-left:10%; width:55%%' value='Calculate your installment now'></div></div>
                        <div class='row new-section'><div class='col-sm-5'><h4 class='body-headings'>Quick Summary </h4>
                        <label>Location: </label><p class='quick-summary-elements'>" + objProperty.PropertyAddress + @"</p><br />
                        <label>Property Type: </label><p class='quick-summary-elements'>" + objProperty.PropertyType.PropertyType + @"</p><br />
                        <label>Area: </label><p class='quick-summary-elements'>" + objProperty.PropertyAreaInMsq + @" m<sup>2</sup></p> <br />
                        <label>Beds: </label><p class='quick-summary-elements'>" + objProperty.PropertyBedrooms + @"</p><br />
                        <label>Baths: </label><p class='quick-summary-elements'>" + objProperty.PropertyBathrooms + @"</p><br /><label>Garage: </label>
                        <p class='quick-summary-elements'>" + objProperty.PropertyGarage + @"</p>  <br /></div><div class='col-sm-1' style='padding:0px;'>
                        </div><div class='col-sm-6'><h4 class='body-headings''>Amenities</h4><ul> <div class='row'>";

                    if (AmenityIDs != "")
                    {
                        foreach (var item in objAmenities.Amenity)
                        {
                            HTML += "<div class='col-md-4'><li>" + item.AmenityType + @"</li> </div>";
                        }
                    }
                    else
                    {
                        HTML += "<div class='col-md-4'><li>No Amenities Added.</li> </div>";
                    }

                    HTML += @"</div></ul></div></div>";

                    HTML += @"<div class='row new-section'><div class='col-12'><h4 class='body-headings'>Property Description </h4> 
                        <p>" + HttpUtility.HtmlDecode(objProperty.PropertyDescription) + @"</p></div></div>";

                    if (objProperty.PropertyVideo != "")
                        HTML += @"<div class='row new-section'>   
                            <div class='col-md-12'><h4 class='body-headings''>Property Video</h4><br /><iframe src='" + objProperty.PropertyVideo + @"' height='315' width='100%'></iframe> <br /></div></div>";
                }
                else
                    return;
            }
            catch { }
        }

        protected void btnSendEmail_Click(object sender, EventArgs e)
        {//name email comment
            try
            {
                MailMessage message = new MailMessage("samarjhaider@gmail.com", "samarjhaider@gmail.com");
                //message.Body = TextAreaComments.Value;
                SmtpClient client = new SmtpClient("smtp.gmail.com", 587);

                client.Credentials = new System.Net.NetworkCredential("samarjhaider@gmail.com", "xyz");
                client.EnableSsl = true;
                //client.DeliveryMethod = SmtpDeliveryMethod.Network;
                //client.UseDefaultCredentials = false;

                client.Send(message);

            }
            catch (Exception ex)
            {

            }
        }

        [WebMethod]
        public static int GetInterestRate(int CategoryID, int TenureValue)
        {
            int rate = 0;

            try
            {
                DataTable dt = Helper.ExecuteSPWithParameters(InterestRateConstants.Proc_GetInterestRate, Category.par_CategoryID, CategoryID.ToString(), TenureConstants.Par_Tenure, TenureValue.ToString());

                if (dt != null && dt.Rows.Count > 0)
                    rate = Convert.ToInt16(dt.Rows[0][InterestRateConstants.Col_InterestRate]);
            }
            catch
            {

            }
            return rate;
        }
    }
}