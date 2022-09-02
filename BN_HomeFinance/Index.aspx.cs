using BN_HomeFinance.Resources;
using Newtonsoft.Json;
using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Net;
using System.Text;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.UI.WebControls;
using System.Web;
using System.Collections.Generic;

namespace BN_HomeFinance
{
    public partial class Index : System.Web.UI.Page
    {
        public string HtmlCaroseul = "", HTMLCaroseulThumbnails = "", HtmlCarouselIndicators = "";

        public string HtmlChat = "", HtmlChat1 = "", HtmlChat2 = "";

        public string HtmlMessages = "";

        public string HtmlHotProps = "", HTMLTopVendors = "", HTMLTopBuilders;

        DataSet ds = new DataSet();

        BN_HomeFinance.Resources.PropertyCollection objProperties = new BN_HomeFinance.Resources.PropertyCollection();

        Tenure objTenure = new Tenure();

        Downpayment objdownpayment = new Downpayment();

        Properties objProperty = new Properties();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    if (Request.QueryString[Helper.QueryStrings.code.ToString()] != null)
                    {
                        BNUser user = Helper.GetToken(Request.QueryString[Helper.QueryStrings.code.ToString()].ToString());

                        BNUser userExist = user.GetUserWithUserLogin(user.LoginName);


                        if (userExist.IsExists)
                        {
                            Session[Helper.Sessions.User.ToString()] = userExist;
                        }
                        else
                        {
                            user.UserID = Guid.NewGuid().ToString();
                            user.UserType.UserTypeID = (Helper.ExecuteSPWithNoParameters(User_Constants.Proc_GetConsumerTypeID)).Rows[0][User_Constants.Col_UserTypeID].ToString();
                            user.UserType.UserType = User_Constants.UserType.Consumer.ToString();
                            user.Status = User_Constants.UserStatus.Active.ToString();
                            user.Password = "None";
                            user.Email = "Email";
                            user.City = "City";
                            user.Country = "Country";
                            user.Pin = "";
                            user.Address = "Address";
                            user.Contact = "Contact";
                            user.Status = User_Constants.UserStatus.Active.ToString();
                            user.About = "About";

                            Session[Helper.Sessions.User.ToString()] = user.CreateUser(user);
                        }

                        Response.Redirect(Helper.pg_Index);
                    }

                    #region User Logs

                    BNUser userObj = new BNUser();
                    UserLogs_Collection userLogsList = new UserLogs_Collection();
                    userLogsList = userLogsList.GetDormantUsers();
                    userLogsList.RemoveInactiveUsersFromUserLog();

                    for (int i = 0; i < userLogsList.Count; i++)
                    {
                        userObj.DeleteUser(userLogsList.UserLogs[i].UserID);
                    }

                    #endregion

                    #region Banner

                    BNPost postAnnouncements = new BNPost(Helper.Module.Announcements);
                    BNPostCollection colAnnouncements = postAnnouncements.GetTopPosts(postAnnouncements);

                    if (colAnnouncements != null && colAnnouncements.Count > 0)
                    {
                        if (Request.QueryString[Helper.QueryStrings.PreviewMode.ToString()] != null
                                && Request.QueryString[Helper.QueryStrings.PreviewMode.ToString()].ToString() == "1"
                                && Session[Helper.Sessions.PreviewImageHome.ToString()] != null
                                && Request.QueryString[Helper.QueryStrings.Header.ToString()] != null
                                && Request.QueryString[Helper.QueryStrings.Description.ToString()] != null
                                && Request.QueryString[Helper.QueryStrings.HeaderColor.ToString()] != null
                                && Request.QueryString[Helper.QueryStrings.DescriptionColor.ToString()] != null)
                        {
                            HtmlCarouselIndicators += "<button type='button' data-bs-target='#carouselExampleDark' data-bs-slide-to='0' class='active' aria-current='true' aria-label='Slide 1'></button>";
                            HtmlCaroseul += @"<div class='carousel-item active' data-bs-interval='3000'><img src='" + Session[Helper.Sessions.PreviewImageHome.ToString()].ToString() + @"' class='d-block w-100' alt='...'>
                                              <div class='carousel-caption-text'><h1 style='color:#" + Request.QueryString[Helper.QueryStrings.HeaderColor.ToString()].ToString() + "'>" + Request.QueryString[Helper.QueryStrings.Header.ToString()].ToString() + @"</h1>
                                              <p style='color:#" + Request.QueryString[Helper.QueryStrings.DescriptionColor.ToString()].ToString() + "'>" + Request.QueryString[Helper.QueryStrings.Description.ToString()] + @"</p>
                                              <button class='br-100 c-w btn-chat mt-3 bg-transparent' onclick='ScrollToBottom();' type='button'><i class='fas fa-comments c-w'></i> &nbsp;Start a Chat</i></button></div></div>";

                            for (int count = 0; count < colAnnouncements.Count - 1; count++)
                            {
                                PostColors colors = new PostColors();

                                colors.Module = Helper.Module.Announcements.ToString();
                                colors.PostID = colAnnouncements.Posts[count].ID;

                                colors = colors.GetPostColors(colors);

                                HtmlCarouselIndicators += "<button type='button' data-bs-target='#carouselExampleDark' data-bs-slide-to='" + count + @"' aria-label='Slide 2'></button>";
                                HtmlCaroseul += @"<div class='carousel-item' data-bs-interval='3000'><img src='" + colAnnouncements.Posts[count].Image.ImageURL + @"' class='d-block w-100' alt='...'>
                                                  <div class='carousel-caption-text'><h1 style='color:" + colors.HeaderColor + "'>" + colAnnouncements.Posts[count].Header + @"</h1><p style='color:" + colors.DescriptionColor + "'>" + colAnnouncements.Posts[count].Description + @"</p>
                                                  <button class='br-100 c-w btn-chat mt-3 bg-transparent' onclick='ScrollToBottom();' type='button'><i class='fas fa-comments c-w'></i> &nbsp;Start a Chat</button></div></div>";
                            }
                        }
                        else
                        {
                            for (int count = 0; count < colAnnouncements.Count; count++)
                            {
                                PostColors colors = new PostColors();

                                colors.Module = Helper.Module.Announcements.ToString();
                                colors.PostID = colAnnouncements.Posts[count].ID;

                                colors = colors.GetPostColors(colors);

                                if (count == 0)
                                {
                                    HtmlCarouselIndicators += "<button type='button' data-bs-target='#carouselExampleDark' data-bs-slide-to='0' class='active' aria-current='true' aria-label='Slide 1'></button>";
                                    HtmlCaroseul += @"<div class='carousel-item active' data-bs-interval='3000'><img src='" + colAnnouncements.Posts[count].Image.ImageURL + @"' class='d-block w-100' alt='...'>
                                                      <div class='carousel-caption-text'><h1 style='color:" + colors.HeaderColor + "'>" + colAnnouncements.Posts[count].Header + @"</h1><p style='color:" + colors.DescriptionColor + "'>" + colAnnouncements.Posts[count].Description + @"</p>
                                                      <button class='br-100 c-w btn-chat mt-3 bg-transparent' onclick='ScrollToBottom();' type='button'><i class='fas fa-comments c-w'></i> &nbsp;Start a Chat</i></button></div></div>";
                                }
                                else
                                {
                                    HtmlCarouselIndicators += "<button type='button' data-bs-target='#carouselExampleDark' data-bs-slide-to='" + count + @"' aria-label='Slide 2'></button>";
                                    HtmlCaroseul += @"<div class='carousel-item' data-bs-interval='3000'><img src='" + colAnnouncements.Posts[count].Image.ImageURL + @"' class='d-block w-100' alt='...'>
                                                      <div class='carousel-caption-text'><h1 style='color:#" + colors.HeaderColor + "'>" + colAnnouncements.Posts[count].Header + @"</h1><p style='color:" + colors.DescriptionColor + "'>" + colAnnouncements.Posts[count].Description + @"</p>
                                                      <button class='br-100 c-w btn-chat mt-3 bg-transparent' onclick='ScrollToBottom();' type='button'><i class='fas fa-comments c-w'></i> &nbsp;Start a Chat</button></div></div>";
                                }
                            }
                        }
                    }
                    else
                    {
                        if (Request.QueryString[Helper.QueryStrings.PreviewMode.ToString()] != null
                                && Request.QueryString[Helper.QueryStrings.PreviewMode.ToString()].ToString() == "1"
                                && Session[Helper.Sessions.PreviewImageHome.ToString()] != null
                                && Request.QueryString[Helper.QueryStrings.Header.ToString()] != null
                                && Request.QueryString[Helper.QueryStrings.Description.ToString()] != null
                                && Request.QueryString[Helper.QueryStrings.HeaderColor.ToString()] != null
                                && Request.QueryString[Helper.QueryStrings.DescriptionColor.ToString()] != null)
                        {
                            HtmlCarouselIndicators += "<button type='button' data-bs-target='#carouselExampleDark' data-bs-slide-to='0' class='active' aria-current='true' aria-label='Slide 1'></button>";
                            HtmlCaroseul += @"<div class='carousel-item active' data-bs-interval='3000'><img src='" + Session[Helper.Sessions.PreviewImageHome.ToString()].ToString() + @"' class='d-block w-100' alt='...'>
                                              <div class='carousel-caption-text'><h1 style='color:" + Request.QueryString[Helper.QueryStrings.HeaderColor.ToString()].ToString() + "'>" + Request.QueryString[Helper.QueryStrings.Header.ToString()].ToString() + @"</h1>
                                              <p style='color:" + Request.QueryString[Helper.QueryStrings.DescriptionColor.ToString()].ToString() + "'>" + Request.QueryString[Helper.QueryStrings.Description.ToString()] + @"</p>
                                              <button class='br-100 c-w btn-chat mt-3 bg-transparent' onclick='ScrollToBottom();' type='button'><i class='fas fa-comments c-w'></i> &nbsp;Start a Chat</i></button></div></div>";
                        }
                    }

                    #endregion

                    #region Messages

                    BNPost postMessage = new BNPost(Helper.Module.Messages);

                    BNPostCollection colMessages = postMessage.GetTopPosts(postMessage);

                    if (colMessages != null && colMessages.Count > 0)
                    {
                        for (int i = 0; i < colMessages.Posts.Count; i++)
                        {

                            string uri = "/" + Helper.pg_Message + @"?" + Helper.QueryStrings.MessageID + @"=" + colMessages.Posts[i].ID;

                            HtmlMessages += @"<div class='col-12 col-sm-4 col-md-4 col-lg-4 cover-message'><a href='" + uri + "' style='display:table;width: 100%;'><div class='card-message shadow w-100'><h6 class='w-100'><i class='fas fa-circle'></i>&nbsp;&nbsp;&nbsp;" + colMessages.Posts[i].Header + @"</h6></div></a></div>";
                        }
                    }

                    #endregion

                    #region Search

                    ddlGovernorate.DataSource = Helper.GetData(GovernorateConstants.Proc_GetGovernorate, null);
                    ddlGovernorate.DataBind();
                    ddlGovernorate.Items.Insert(0, new ListItem(" Governorate ", "0"));

                    LBPropertyType.DataSource = Helper.GetData(PropertyTypeConstants.proc_GetPropertyType, null);
                    LBPropertyType.DataBind();
                    LBPropertyType.Items.RemoveAt(LBPropertyType.Items.Count - 1);

                    LBWilayat.DataSource = Helper.GetData(WilayatConstants.proc_GetAllWilayats, null);
                    LBWilayat.DataBind();

                    LBProductCategory.DataSource = Helper.GetData(Category.proc_GetCategory, null);
                    LBProductCategory.DataTextField = Category.col_CategoryName;
                    LBProductCategory.DataValueField = Category.col_CategoryID;
                    LBProductCategory.DataBind();

                    DataTable dtPV = Helper.ExecuteSPWithNoParameters(PropertyConstants.proc_GetMaxPropertyValue);
                    DataTable dtPA = Helper.ExecuteSPWithNoParameters(PropertyConstants.proc_GetMaxPropertyArea);
                    DataTable dtPD = Helper.ExecuteSPWithNoParameters(Product_Constants.proc_GetMaxProductValue);

                    if (dtPV != null && dtPV.Rows.Count > 0)
                        HDPropValMax.Value = dtPV.Rows[0][PropertyConstants.col_PropertyValue].ToString();

                    if (dtPA != null && dtPA.Rows.Count > 0)
                        HDPropAreaMax.Value = dtPA.Rows[0][PropertyConstants.col_PropertyAreaInMsq].ToString();

                    if (dtPD != null && dtPD.Rows.Count > 0)
                        HDProdValMax.Value = dtPD.Rows[0][Product_Constants.col_Price].ToString();
                    #endregion

                    #region Calculator

                    ddlPropertyType.DataSource = Helper.GetData(PropertyTypeConstants.proc_GetPropertyType, null);
                    ddlPropertyType.DataBind();
                    ddlPropertyType.Items.Insert(0, new ListItem(" Choose Category", "0"));

                    ddlTenure.DataSource = Helper.GetData(CalculatorConstants.proc_GetTenure, null);
                    ddlTenure.DataBind();
                    ddlTenure.Items.Insert(0, new ListItem(" Tenure", "0"));

                    #endregion

                    #region Hot Properties

                    BN_HomeFinance.Resources.PropertyCollection Pcollection = new BN_HomeFinance.Resources.PropertyCollection();

                    Pcollection = Pcollection.GetRecentProperties();

                    if (Pcollection != null && Pcollection.Count > 0)
                    {
                        for (int i = 0; i < Pcollection.Count; i++)
                        {
                            string uri = "/" + Helper.pg_PropertyDetails + @"?" + Helper.QueryStrings.PropertyID + @"=" + Pcollection.Properties[i].PropertyID;



                            HtmlHotProps += @"<div class='col-6 col-sm-6 col-md-3 col-lg-3 p-cover'><a href='" + uri + "'><div class='card shadow'><img src='" + Pcollection.Properties[i].Image.ImageURL + @"' class='card-img-top' alt='...'>
                                            <div class='card-body'><h5 class='card-title c-b t-c'>" + Pcollection.Properties[i].PropertyName + @"</h5><p class='card-text t-c'>View more details</p></div></div></a></div>";

                        }
                    }

                    #endregion

                    #region Builders

                    BNUserCollection Ucollection = new BNUserCollection();

                    Ucollection = Ucollection.GetTopUser(User_Constants.UserType.Builder.ToString());

                    if (Ucollection != null && Ucollection.Count > 0)
                    {
                        hpSeeBuilders.NavigateUrl = "/" + Helper.pg_StakeHolderList + "?" + Helper.QueryStrings.UserTypeID.ToString() + "=" + Ucollection.BNUser[0].UserType.UserTypeID;

                        for (int i = 0; i < Ucollection.Count; i++)
                        {
                            string uri = "\"/" + Helper.pg_UserProfile + @"?" + Helper.QueryStrings.UserID + @"=" + Ucollection.BNUser[i].UserID + "\"";

                            HTMLTopBuilders += @"<td onclick='route(" + uri + @")'><div class='builder-grid-layout shadow'><div class='builder-img-div'>
                                                 <img src='" + Ucollection.BNUser[i].Picture.ImageURL + @"' alt='Alt Text' height='100' width='100'></div></div></td>";
                        }
                    }
                    #endregion

                    #region Vendors

                    BNUserCollection Vcollection = new BNUserCollection();

                    Vcollection = Vcollection.GetTopUser(User_Constants.UserType.Vendor.ToString());

                    if (Vcollection != null && Vcollection.Count > 0)
                    {
                        hpSeeVendors.NavigateUrl = "/" + Helper.pg_StakeHolderList + "?" + Helper.QueryStrings.UserTypeID.ToString() + "=" + Vcollection.BNUser[0].UserType.UserTypeID;

                        for (int i = 0; i < Vcollection.Count; i++)
                        {
                            string uri = "\"/" + Helper.pg_UserProfile + @"?" + Helper.QueryStrings.UserID + @"=" + Vcollection.BNUser[i].UserID + "\"";

                            HTMLTopVendors += @"<td onclick='route(" + uri + @")'><div class='builder-grid-layout shadow'><div class='builder-img-div'>
                                                 <img src='" + Vcollection.BNUser[i].Picture.ImageURL + @"' alt='Alt Text' height='100' width='100'></div></div></td>";
                        }
                    }

                    #endregion

                    #region Chat

                    if (Session[Helper.Sessions.User.ToString()] != null)
                    {
                        HDCurrentUser.Value = ((BNUser)Session[Helper.Sessions.User.ToString()]).UserID;
                        HDFullName.Value = ((BNUser)Session[Helper.Sessions.User.ToString()]).FullName;
                        HDUrl.Value = ((BNUser)Session[Helper.Sessions.User.ToString()]).Picture.ImageURL;
                    }

                    ChatCollection collection = new ChatCollection();

                    collection = collection.GetRecentChats();

                    if (collection != null && collection.Count > 0)
                    {
                        for (int i = 0; i < collection.Count; i++)
                        {
                            HtmlChat += @"<div class='row chat-container'><div class='col-12 col-sm-12 col-md-12 col-lg-12 message-container'>
                                            <div class='chat-message mt-2 w-100'><div class='row'><div class='col-2 col-sm-2 col-md-2 col-lg-2 img-fluid'>
                                            <img src='" + collection.Chats[i].CreatedBy.Picture.ImageURL + @"'></div><div class='col-6 col-sm-6 col-md-7 col-lg-7'><div class='row'>
                                            <div class='col-12 col-sm-12 col-md-12 col-lg-12 chat-text'><h2 class='c-w'>" + collection.Chats[i].CreatedBy.FullName + @"</h2>
                                            <p class='c-w'>" + collection.Chats[i].Thread + @"</p></div><div class='col-12 col-sm-12 col-md-12 col-lg-12'></div>
                                            </div></div><div class='col-2 col-sm-2 col-md-2 col-lg-2 btn-reply-fluid'><input type='button' class='btn bg-gray br' value='Reply' onclick='ViewSubReply(this)'></div>
                                            <div class='col-2 col-sm-2 col-md-1 col-lg-1 comment-icon-container'>
                                            <i class='far fa-comments c-w'></i></div><div class='col-12 col-sm-12 col-md-12 col-lg-12 text-reply-fluid'>
                                            <div class='row'><div class='col-9 col-sm-9 col-md-10 col-lg-10'><input type='text' class='form-control' placeholder='Enter Reply...' /></div>
                                            <div class='col-3 col-sm-3 col-md-2 col-lg-2'><input type='button' class='btn bg-gray br c-b btn-submit f-r' value='Submit' onclick='SubmitReply(this);'>
                                            </div></div></div></div>
                                            <!-- Reply Added Here -->";

                            for (int j = 0; j < collection.Chats[i].Messages.IDs.Count; j++)
                            {
                                HtmlChat += @"<div class='reply'><div class='row reply-box'><div class='col-3 col-sm-3 col-md-2 col-lg-2 img-fluid-reply'>
                                              <img src='" + collection.Chats[i].Messages.CreatedBy[j].Picture.ImageURL + @"'></div><div class='col-6 col-sm-6 col-md-8 col-lg-8'><div class='row'>
                                              <div class='col-12 col-sm-12 col-md-12 col-lg-12 chat-text'><h6>" + collection.Chats[i].Messages.CreatedBy[j].FullName + @"</h6>
                                              <p>" + collection.Chats[i].Messages.Messsages[j] + @"</p>
                                              </div></div></div><div class='col-3 col-sm-3 col-md-2 col-lg-2 btn-reply-fluid'><input type='button' class='btn bg-purple 
                                              br c-w' value='Reply' onclick='ViewSubReply(this)'></div><div class='col-12 col-sm-12 col-md-12 col-lg-12 text-reply-fluid'>
                                              <div class='row'><div class='col-9 col-sm-9 col-md-10 col-lg-10'><input type='text' class='form-control' placeholder='Enter Reply...' />
                                              </div><div class='col-3 col-sm-3 col-md-2 col-lg-2'><input type='button' class='btn bg-purple br c-w btn-submit f-r' value='Submit' onclick='SubmitSubReply(this);'>
                                              </div></div></div>
                                              <!-- Sub reply to be added here -->";
                                if (collection.Chats[i].Messages.Replies[j].IDs.Count > 0)
                                {
                                    for (int k = 0; k < collection.Chats[i].Messages.Replies[j].IDs.Count; k++)
                                    {
                                        HtmlChat += @"<div class=' col-12 col-sm-12 col-md-12 col-lg-12 sub-reply'><div class='row bg-gray c-b sub-reply-box'>
                                              <div class='col-3 col-sm-3 col-md-2 col-lg-2 img-fluid-reply'><img src='" + collection.Chats[i].Messages.Replies[j].CreatedBy[k].Picture.ImageURL + @"'></div>
                                              <div class='col-6 col-sm-6 col-md-8 col-lg-8'><div class='row'><div class='col-12 col-sm-12 col-md-12 col-lg-12 chat-text'>
                                              <h6>" + collection.Chats[i].Messages.Replies[j].CreatedBy[k].FullName + @"</h6><p>" + collection.Chats[i].Messages.Replies[j].Replies[k] + @"</p>
                                              </div></div></div><div class='col-3 col-sm-3 col-md-2 col-lg-2 btn-reply-fluid'><i class='fas fa-reply c-p .fs-7'></i></div></div></div>";
                                    }
                                }
                                HtmlChat += @"<!-- Sub reply to be added here -->
                                                </div></div>";
                            }

                            HtmlChat += @"<!-- Reply Added Here -->
                                              </div></div></div>";
                        }
                    }
                    else
                    {

                    }

                    #endregion
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            else
                return;
        }

        protected void btnSearchProperty_Click(object sender, EventArgs e)
        {
            try
            {
                objProperty.PropertyGovernorate.GovernorateID = ddlGovernorate.SelectedIndex;

                for (int i = 0; i < LBWilayat.Items.Count; i++)
                {
                    if (LBWilayat.Items[i].Selected)
                    {
                        objProperty.PropertyWilayat.WilayatIDs += LBWilayat.Items[i].Value + ",";
                    }
                }

                if (objProperty.PropertyWilayat.WilayatIDs != null && objProperty.PropertyWilayat.WilayatIDs.Contains(","))
                    objProperty.PropertyWilayat.WilayatIDs = objProperty.PropertyWilayat.WilayatIDs.TrimEnd(',');
                else
                    objProperty.PropertyWilayat.WilayatIDs = "";


                for (int i = 0; i < LBPropertyType.Items.Count; i++)
                {
                    if (LBPropertyType.Items[i].Selected)
                    {
                        objProperty.PropertyType.PropertyTypeIDs += LBPropertyType.Items[i].Value + ",";
                    }
                }

                if (objProperty.PropertyType.PropertyTypeIDs != null && objProperty.PropertyType.PropertyTypeIDs.Contains(","))
                    objProperty.PropertyType.PropertyTypeIDs = objProperty.PropertyType.PropertyTypeIDs.TrimEnd(',');
                else
                    objProperty.PropertyType.PropertyTypeIDs = "";


                for (int i = 0; i < LBBedroom.Items.Count; i++)
                {
                    if (LBBedroom.Items[i].Selected)
                    {
                        objProperty.Bedrooms += LBBedroom.Items[i].Value + ",";
                    }
                }

                if (objProperty.Bedrooms != null && objProperty.Bedrooms.Contains(","))
                    objProperty.Bedrooms = objProperty.Bedrooms.TrimEnd(',');
                else
                    objProperty.Bedrooms = "";

                objProperty.AreaMin = HDAreaMin.Value;
                objProperty.AreaMax = HDAreaMax.Value;
                objProperty.ValueMin = HDPropertyMin.Value;
                objProperty.ValueMax = HDPropertyMax.Value;

                objProperties = objProperties.SearchProperty(Convert.ToString(objProperty.PropertyGovernorate.GovernorateID), objProperty.PropertyWilayat.WilayatIDs,
                    objProperty.PropertyType.PropertyTypeIDs, objProperty.Bedrooms, objProperty.AreaMin, objProperty.AreaMax,
                    objProperty.ValueMin, objProperty.ValueMax);

                Session[Helper.Sessions.Properties.ToString()] = objProperties;

                Response.Redirect(Helper.pg_SearchProperties);

            }
            catch (Exception ex)
            {

            }
        }

        protected void ddlGovernorate_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ds.Clear();
                if (ddlGovernorate.SelectedIndex == 0)
                {
                    LBWilayat.Enabled = false;
                }
                else
                {
                    LBWilayat.Enabled = true;
                    SqlParameter governorateIDParamForWilayat = new SqlParameter("@governorateID", ddlGovernorate.SelectedValue);

                    System.Data.DataSet DS = Helper.GetData(WilayatConstants.proc_GetWilayatByGovernorate, governorateIDParamForWilayat);

                    LBWilayat.DataSource = DS.Tables[0];
                    LBWilayat.DataBind();
                    LBWilayat.Items.Insert(0, new ListItem("Select All"));


                }
            }
            catch (Exception ex)
            {

            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                BNUser user = new BNUser();

                if (txtUserName.Text.ToLower() != "admin")
                {
                    if (user.ValidateCredentials(txtUserName.Text, txtPassword.Text))
                    {
                        user = user.GetUser(txtUserName.Text);

                        Session[Helper.Sessions.User.ToString()] = user;

                        if (Session[Helper.Sessions.Source.ToString()] != null)
                        {
                            string URI = (string)Session[Helper.Sessions.Source.ToString()].ToString();
                            Session[Helper.Sessions.Source.ToString()] = null;
                            Response.Redirect(URI);
                        }
                        else
                        {
                            if (user.UserType.UserType == User_Constants.UserType.Builder.ToString() || user.UserType.UserType == User_Constants.UserType.Vendor.ToString())
                                Response.Redirect(Helper.pg_UserProfile);
                            else
                                Response.Redirect(Helper.pg_Index);
                        }
                    }
                    else
                    {
                        //lblError.Visible = true;
                        //lblError.Text = "User Name or Password is Incorrect";
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        protected void btnSignup_Click(object sender, EventArgs e)
        {
            Response.Redirect(Helper.pg_Signup);
        }

        protected void btnSearchProduct_Click(object sender, EventArgs e)
        {
            try
            {
                Product product = new Product();

                for (int i = 0; i < LBProductCategory.Items.Count; i++)
                {
                    if (LBProductCategory.Items[i].Selected)
                    {
                        product.Category.CategoryIDs += LBProductCategory.Items[i].Value + ",";
                    }
                }

                if (product.Category.CategoryIDs != null && product.Category.CategoryIDs.Contains(","))
                    product.Category.CategoryIDs = product.Category.CategoryIDs.TrimEnd(',');
                else
                    product.Category.CategoryIDs = "";

                ProductCollection collection = product.SearchProduct(product.Category.CategoryIDs, HDProductMin.Value, HDProductMax.Value);

                Session[Helper.Sessions.Products.ToString()] = collection;

                Response.Redirect(Helper.pg_SearchedProducts);
            }
            catch (Exception ex)
            {

            }
        }

        protected void btnPost_Click(object sender, EventArgs e)
        {
            try
            {
                Chat chat = new Chat();

                //chat.Thread = txtPost.Value;
                BNUser user = new BNUser();
                user.UserID = HDCurrentUser.Value;
                chat.CreatedBy = user;
                chat.ID = chat.PostMessage(chat);

                if (chat.ID != 0)
                    Response.Redirect(Helper.pg_ChatPage + "?" + Helper.QueryStrings.ChatID.ToString() + "=" + chat.ID.ToString());
            }
            catch
            {
                throw;
            }
        }

        [WebMethod]
        public static bool SubmitReply(string MessageID, string Reply, string UserID)
        {
            try
            {
                ChatMessgeReplies reply = new ChatMessgeReplies();

                reply.IDs.Add(Convert.ToInt32(MessageID));
                reply.Replies.Add(Reply);
                BNUser user = new BNUser();
                user.UserID = UserID;
                reply.CreatedBy.Add(user);
                reply.Count = 1;

                reply.InsertReply(reply);
            }
            catch
            {
                throw;
            }

            return true;
        }

        [WebMethod]
        public static bool SubmitMessage(string ChatID, string Message, string UserID)
        {
            try
            {
                ChatMessages message = new ChatMessages();

                message.IDs.Add(Convert.ToInt32(ChatID));
                message.Messsages.Add(Message);
                BNUser user = new BNUser();
                user.UserID = UserID;
                message.CreatedBy.Add(user);
                message.Count = 1;

                message.InsertMessage(message);

                //Response.Redirect(Helper.pg_ChatPage + "?" + Helper.QueryStrings.ChatID.ToString() + "=" + chat.ID.ToString());
            }
            catch
            {
                throw;
            }

            return true;
        }

        [WebMethod]
        public static string GetWilayats(int govtID)
        {
            WilayatCollection objWilayats = new WilayatCollection();


            objWilayats = objWilayats.GetWilayatByGovernorateID(govtID);


            return JsonConvert.SerializeObject(objWilayats).ToString();
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