using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BN_HomeFinance.Resources;

namespace BN_HomeFinance
{
    public partial class StakeHolderList : System.Web.UI.Page
    {
        public string HTML = "", HTMLBuilders = "", HTMLIndicators = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString[Helper.QueryStrings.UserTypeID.ToString()] != null)
                {
                    string UserTypeID = Request.QueryString[Helper.QueryStrings.UserTypeID.ToString()].ToString();

                    BNUserCollection objUsers = new BNUserCollection();

                    BNImage objImage = new BNImage();

                    objUsers = objUsers.GetUsersByUserTypeID(UserTypeID);

                    if (objUsers != null && objUsers.Count > 0)
                    {
                        if (objUsers.BNUser[0].UserType.UserType == User_Constants.UserType.Vendor.ToString())
                        {
                            lblHeader.Text = "Our Vendors";
                        }
                        else if (objUsers.BNUser[0].UserType.UserType == User_Constants.UserType.Builder.ToString())
                        {
                            lblHeader.Text = "Our Builders";
                        }

                        BNPost post;

                        BNPostCollection collection = new BNPostCollection();

                        if (objUsers.BNUser[0].UserType.UserType == User_Constants.UserType.Builder.ToString())
                            post = new BNPost(Helper.Module.BuilderAnnouncements);
                        else
                            post = new BNPost(Helper.Module.VendorAnnouncements);

                        collection = post.GetTopPosts(post);

                        if (collection != null && collection.Count > 0)
                        {
                            string ImageURL = "";

                            if (objUsers.BNUser[0].UserType.UserType == User_Constants.UserType.Vendor.ToString())
                            {
                                if (Session[Helper.Sessions.PreviewImageVendor.ToString()] != null)
                                {
                                    ImageURL = Session[Helper.Sessions.PreviewImageVendor.ToString()].ToString();
                                }
                            }
                            else
                            {
                                if (Session[Helper.Sessions.PreviewImageBuilder.ToString()] != null)
                                {
                                    ImageURL = Session[Helper.Sessions.PreviewImageBuilder.ToString()].ToString();
                                }
                            }

                            if (Request.QueryString[Helper.QueryStrings.PreviewMode.ToString()] != null
                                && Request.QueryString[Helper.QueryStrings.PreviewMode.ToString()].ToString() == "1"
                                && ImageURL != "")
                            {
                                HTMLIndicators += "<button type='button' data-bs-target='#carouselExampleDark' data-bs-slide-to='0' class='active' aria-current='true' aria-label='Slide 0'></button>";
                                HTML += @"<div class='carousel-item active' data-bs-interval='5000'><img src='" + ImageURL + @"' class='d-block w-100' alt='...'></div>";

                                for (int i = 0; i < collection.Count - 1; i++)
                                {
                                    HTMLIndicators += "<button type='button' data-bs-target='#carouselExampleDark' data-bs-slide-to='" + (i + 1) + "' aria-current='true' aria-label='Slide " + (i + 1) + "'></button>";
                                    HTML += @"<div class='carousel-item' data-bs-interval='5000'><img src='" + collection.Posts[i].Image.ImageURL + @"' class='d-block w-100' alt='...'></div>";
                                }
                            }
                            else
                            {
                                for (int i = 0; i < collection.Count; i++)
                                {
                                    if (i == 0)
                                    {
                                        HTMLIndicators += "<button type='button' data-bs-target='#carouselExampleDark' data-bs-slide-to='" + i + "' class='active' aria-current='true' aria-label='Slide " + i + "'></button>";
                                        HTML += @"<div class='carousel-item active' data-bs-interval='5000'><img src='" + collection.Posts[i].Image.ImageURL + @"' class='d-block w-100' alt='...'></div>";
                                    }
                                    else
                                    {
                                        HTMLIndicators += "<button type='button' data-bs-target='#carouselExampleDark' data-bs-slide-to='" + i + "' aria-current='true' aria-label='Slide " + i + "'></button>";
                                        HTML += @"<div class='carousel-item' data-bs-interval='5000'><img src='" + collection.Posts[i].Image.ImageURL + @"' class='d-block w-100' alt='...'></div>";
                                    }
                                }
                            }
                        }
                        else
                        {
                            string ImageURL = "";

                            if (objUsers.BNUser[0].UserType.UserType == User_Constants.UserType.Vendor.ToString())
                            {
                                if (Session[Helper.Sessions.PreviewImageVendor.ToString()] != null)
                                {
                                    ImageURL = Session[Helper.Sessions.PreviewImageVendor.ToString()].ToString();
                                }
                            }
                            else
                            {
                                if (Session[Helper.Sessions.PreviewImageBuilder.ToString()] != null)
                                {
                                    ImageURL = Session[Helper.Sessions.PreviewImageBuilder.ToString()].ToString();
                                }
                            }

                            if (Request.QueryString[Helper.QueryStrings.PreviewMode.ToString()] != null
                                && Request.QueryString[Helper.QueryStrings.PreviewMode.ToString()].ToString() == "1"
                                && ImageURL != "")
                            {
                                HTMLIndicators += "<button type='button' data-bs-target='#carouselExampleDark' data-bs-slide-to='0' class='active' aria-current='true' aria-label='Slide 0'></button>";
                                HTML += @"<div class='carousel-item active' data-bs-interval='5000'><img src='" + ImageURL + @"' class='d-block w-100' alt='...'></div>";

                            }
                        }

                        HTMLBuilders += @"<div class='row mt-4'>";

                        foreach (var item in objUsers.BNUser)
                        {
                            objImage = objImage.GetImage(item.Picture.ImageID);

                            HTMLBuilders += @"<div class='col-6 col-sm-4 col-md-4 col-lg-3 col-xl-3 pd-1'><div class='gridlayout shadow'><a href = 'UserProfile.aspx?UserID=" + item.UserID + @"'>
                                <div class='imgDiv'><img src = '" + objImage.ImageURL + @"' alt='Alternate Text' height='100' width='100' /></div>
                                <div class='textDiv'><p style = 'font-size:12px' ><strong>Name:</strong> " + item.FullName + @"</p>
                                <p style = 'font-size:12px' ><strong>Phone:</strong> " + item.Contact + @"</p>
                                <p style = 'font-size:12px' ><strong>Email:</strong> " + item.Email + @"</p>
                                <p style = 'font-size:12px' ><strong>Address:</strong> " + item.Address + @", " + item.City + @", " + item.Country + @"</p>
                                </div></a></div></div>";
                        }
                        HTMLBuilders += @"</div></div>";
                    }
                }
            }
        }
    }
}