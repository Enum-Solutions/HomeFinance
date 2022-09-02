using BN_HomeFinance;
using BN_HomeFinance.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BN_HomeFinance
{
    public partial class UserProfile : System.Web.UI.Page
    {
        public string PropPromo = "", Links = "", About = "", Background = "", Promotions = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BNUser objUser = new BNUser();

                if (Request.QueryString[Helper.QueryStrings.UserID.ToString()] != null)
                {
                    objUser = objUser.GetUserWithUserID(Request.QueryString[Helper.QueryStrings.UserID.ToString()].ToString());

                    if (objUser == (BNUser)Session[Helper.Sessions.User.ToString()])
                    {
                        if (objUser.UserType.UserType == User_Constants.UserType.Vendor.ToString())
                        {
                            Links += @"<hr /><div class='row px-lg-4'><div class='col-md-12'><h2 class='h3 mb-3'>My Links</h2></div>
                                        <div class='col-6 col-sm-6 col-md-4 col-lg-4'><div class='plan'><div class='planContainer'><div class='title'><h2>Edit Profile</h2></div><div class='button'><a href='" + Helper.pg_Signup + @"?EditMode=1'>Click Here</a></div></div></div></div>
                                        <div class='col-6 col-sm-6 col-md-4 col-lg-4'><div class='plan'><div class='planContainer'><div class='title'><h2>My Products</h2></div><div class='button'><a href = '" + Helper.pg_MyProducts + @"' > Click Here</a></div></div></div></div>
                                        <div class='col-6 col-sm-6 col-md-4 col-lg-4'><div class='plan'><div class='planContainer'><div class='title'><h2>My Promotions</h2></div><div class='button'><a href = '" + Helper.pg_MyPromotions + @"' > Click Here</a></div></div></div></div></div>";
                        }
                        else if (objUser.UserType.UserType == User_Constants.UserType.Builder.ToString())
                        {
                            Links += @"<hr /><div class='row px-lg-4'><div class='col-md-12'><h2 class='h3 mb-3'>My Links</h2></div>
                                        <div class='col-6 col-sm-6 col-md-4 col-lg-4'><div class='plan'><div class='planContainer'><div class='title'><h2>Edit Profile</h2></div><div class='button'><a href='" + Helper.pg_Signup + @"?EditMode=1'>Click Here</a></div></div></div></div>
                                        <div class='col-6 col-sm-6 col-md-4 col-lg-4'><div class='plan'><div class='planContainer'><div class='title'><h2>My Properties</h2></div><div class='button'><a href = '" + Helper.pg_MyProperties + @"' > Click Here</a></div></div></div></div>
                                        <div class='col-6 col-sm-6 col-md-4 col-lg-4'><div class='plan'><div class='planContainer'><div class='title'><h2>My Promotions</h2></div><div class='button'><a href = '" + Helper.pg_MyPromotions + @"' > Click Here</a></div></div></div></div></div>";
                        }
                    }
                }
                else
                {
                    try
                    {
                        objUser = (BNUser)Session[Helper.Sessions.User.ToString()];

                        if (objUser.UserType.UserType == User_Constants.UserType.Vendor.ToString())
                        {
                            Links += @"<hr /><div class='row px-lg-4'><div class='col-md-12'><h2 class='h3 mb-3'>My Links</h2></div>
                                        <div class='col-6 col-sm-6 col-md-4 col-lg-4'><div class='plan'><div class='planContainer'><div class='title'><h2>Edit Profile</h2></div><div class='button'><a href='" + Helper.pg_Signup + @"?EditMode=1'>Click Here</a></div></div></div></div>
                                        <div class='col-6 col-sm-6 col-md-4 col-lg-4'><div class='plan'><div class='planContainer'><div class='title'><h2>My Products</h2></div><div class='button'><a href = '" + Helper.pg_MyProducts + @"' > Click Here</a></div></div></div></div>
                                        <div class='col-6 col-sm-6 col-md-4 col-lg-4'><div class='plan'><div class='planContainer'><div class='title'><h2>My Promotions</h2></div><div class='button'><a href = '" + Helper.pg_MyPromotions + @"' > Click Here</a></div></div></div></div></div>";
                        }
                        else if (objUser.UserType.UserType == User_Constants.UserType.Builder.ToString())
                        {
                            Links += @"<hr /><div class='row px-lg-4'><div class='col-md-12'><h2 class='h3 mb-3'>My Links</h2></div>
                                        <div class='col-6 col-sm-6 col-md-4 col-lg-4'><div class='plan'><div class='planContainer'><div class='title'><h2>Edit Profile</h2></div><div class='button'><a href='" + Helper.pg_Signup + @"?EditMode=1'>Click Here</a></div></div></div></div>
                                        <div class='col-6 col-sm-6 col-md-4 col-lg-4'><div class='plan'><div class='planContainer'><div class='title'><h2>My Properties</h2></div><div class='button'><a href = '" + Helper.pg_MyProperties + @"' > Click Here</a></div></div></div></div>
                                        <div class='col-6 col-sm-6 col-md-4 col-lg-4'><div class='plan'><div class='planContainer'><div class='title'><h2>My Promotions</h2></div><div class='button'><a href = '" + Helper.pg_MyPromotions + @"' > Click Here</a></div></div></div></div></div>";
                        }
                    }
                    catch
                    {
                        Response.Redirect(Helper.pg_Login);
                    }
                }

                if (objUser != null)
                {
                    BNImageCollection objImages = new BNImageCollection();
                    BNImage objImage = new BNImage();

                    PostCollection objPromotion = new PostCollection(Helper.Module.Promotions);
                    objPromotion = objPromotion.GetPostsByUserIDPost(Convert.ToString(Helper.Module.Promotions), objUser.UserID);

                    lblFullName.Text = objUser.FullName;

                    imgUSer.ImageUrl = objUser.Picture.ImageURL;

                    if (objUser.BGPicture.ImageURL != null && objUser.BGPicture.ImageURL != "")
                        Background = "<div class='p-3 p-lg-4 text-white' style='background-image: url(" + objUser.BGPicture.ImageURL + "); max-width: 100%; max-height: 100%; background-repeat: no-repeat; background-size: cover; background-color:var(--purple)'>";
                    else
                        Background = "<div class='p-3 p-lg-4 text-white' style='background-image: url(" + Helper.pc_Background_User + "); max-width: 100%; max-height: 100%; background-repeat: no-repeat; background-size: cover; background-color:var(--purple)'>";

                    About += HttpUtility.HtmlDecode(objUser.About);

                    if (objPromotion.Count > 0)
                    {
                        Promotions += @"<hr /><div class='work-experience-section px-3 px-lg-4'><h2 class='h3 mb-4'>Promotions</h2><div class='timeline'>
                                <div id='myCarousel' class='carousel-dark slide' data-bs-ride='carousel'><div class='carousel-inner'>";

                        for (int i = 0; i < objPromotion.Count; i++)
                        {
                            if (i == 0)
                                Promotions += @"<div class='carousel-item active promotion-item' data-bs-interval='3000'>";
                            else
                                Promotions += @"<div class='carousel-item promotion-item'data-bs-interval='3000'>";

                            Promotions += @"<div class='timeline-card timeline-card-success card shadow-sm' style='background-image: url(" + objPromotion.BNPosts[i].Image.ImageURL + @");background-size: cover;background-repeat:no-repeat'>
                                            <div class='card-body' style='padding: 10rem'>
                                                <div class='h5 mb-1'></div>
                                                    <div class='text-muted text-small mb-2'></div>
                                                </div>
                                            </div>
                                      </div>";

                        }

                        Promotions += "</div></div></div></div>";
                    }

                    if (objUser.UserType.UserType.Equals(User_Constants.UserType.Builder.ToString()))
                    {
                        PropertyCollection objProperties = new PropertyCollection();
                        objProperties = objProperties.GetPropertiesByUserIDForBuilderProfile(objUser.UserID);

                        if (objProperties != null && objProperties.Count > 0)
                        {
                            PropPromo += @"<hr /><div class='row'><div class='col-6 col-sm-6 col-md-6 col-lg-6'><div class='education-section px-3 px-lg-3 pb-4' style='padding-bottom: 0px!Important'>
                                    <h2 class='h3 mb-4' style='padding-bottom: 0px!Important'>Properties</h2></div></div></div><div class='row'>
                                    <div id='carouselExampleDark' class='carousel carousel-dark slide' data-bs-ride='carousel'><div class='carousel-indicators'>
                                    <button type='button' data-bs-target='#carouselExampleDark' data-bs-slide-to='0' class='active' aria-current='true' aria-label='Slide 1'></button>
                                    <button type='button' data-bs-target='#carouselExampleDark' data-bs-slide-to='1' aria-label='Slide 2'></button></div><div class='carousel-inner'>";

                            for (int i = 0; i < objProperties.Count; i = i + 3)
                            {
                                try
                                {
                                    if (i == 0)
                                        PropPromo += @"<div class='carousel-item active'><div class='row'>";
                                    else
                                        PropPromo += @"<div class='carousel-item'><div class='row'>";

                                    for (int j = i; j < (i + 3); j++)
                                    {
                                        try
                                        {
                                            if (objProperties.Properties[j].Image.ImageID.Contains(","))
                                            {
                                                objProperties.Properties[j].Image.ImageID = objProperties.Properties[j].Image.ImageID.Split(',')[0];
                                            }

                                            objImage = objImage.GetTopImage(objProperties.Properties[j].Image.ImageID);

                                            PropPromo += @"<div class='col-6 col-sm-6 col-md-4 col-lg-4 p-cover'><div class='card shadow'><img src='" + objImage.ImageURL + @"' class='card-img-top' alt='...'>
                                            <div class='card-body'><h5 class='card-title c-b t-c'>" + objProperties.Properties[j].PropertyName + @"</h5><p class='card-text t-c'><a href='/" + Helper.pg_PropertyDetails + "?" + Helper.QueryStrings.PropertyID + "=" + objProperties.Properties[j].PropertyID + "'>View more details</a></p></div></div></div>";
                                        }
                                        catch { }
                                    }

                                    PropPromo += @"</div></div>";
                                }
                                catch { }
                            }

                            PropPromo += @"<button class='carousel-control-prev' type='button' data-bs-target='#carouselExampleDark' data-bs-slide='prev'>
                                    <span class='carousel-control-prev-icon' aria-hidden='true'></span><span class='visually-hidden'>Previous</span></button>
                                    <button class='carousel-control-next' type='button' data-bs-target='#carouselExampleDark' data-bs-slide='next'>
                                    <span class='carousel-control-next-icon' aria-hidden='true'></span><span class='visually-hidden'>Next</span></button></div></div></div>";
                        }
                    }
                    else if (objUser.UserType.UserType.Equals(User_Constants.UserType.Vendor.ToString()))
                    {
                        ProductCollection objProducts = new ProductCollection();
                        objProducts = objProducts.GetProductsByUserID(objUser.UserID);

                        if (objProducts != null && objProducts.Count > 0)
                        {
                            PropPromo += @"<hr /><div class='row'><div class='col-6 col-sm-6 col-md-6 col-lg-6'><div class='education-section px-3 px-lg-3 pb-4' style='padding-bottom: 0px!Important'>
                                    <h2 class='h3 mb-4' style='padding-bottom: 0px!Important'>Properties</h2></div></div></div><div class='row'>
                                    <div id='carouselExampleDark' class='carousel carousel-dark slide' data-bs-ride='carousel'><div class='carousel-indicators'>
                                    <button type='button' data-bs-target='#carouselExampleDark' data-bs-slide-to='0' class='active' aria-current='true' aria-label='Slide 1'></button>
                                    <button type='button' data-bs-target='#carouselExampleDark' data-bs-slide-to='1' aria-label='Slide 2'></button></div><div class='carousel-inner'>";

                            for (int i = 0; i < objProducts.Count; i++)
                            {
                                try
                                {
                                    if (i == 0)
                                        PropPromo += @"<div class='carousel-item active'><div class='row'>";
                                    else
                                        PropPromo += @"<div class='carousel-item'><div class='row'>";

                                    for (int j = i; j < (i + 3); j++)
                                    {
                                        try
                                        {
                                            if (objProducts.Products[j].Image.ImageID.Contains(","))
                                            {
                                                objProducts.Products[j].Image.ImageID = objProducts.Products[j].Image.ImageID.Split(',')[0];
                                            }

                                            objImage = objImage.GetTopImage(objProducts.Products[j].Image.ImageID);

                                            PropPromo += @"<div class='col-6 col-sm-6 col-md-4 col-lg-4 p-cover'><div class='card shadow'><img src='" + objImage.ImageURL + @"' class='card-img-top' alt='...'>
                                            <div class='card-body'><h5 class='card-title c-b t-c'>" + objProducts.Products[j].ProductName + @"</h5><p class='card-text t-c'><a href='/" + Helper.pg_ProductDetails + "?" + Helper.QueryStrings.ProductID + "=" + objProducts.Products[j].ProductID + "'>View more details</a></p></div></div></div>";
                                        }
                                        catch { }
                                    }

                                    PropPromo += @"</div></div>";
                                }
                                catch { }
                            }

                            PropPromo += @"<button class='carousel-control-prev' type='button' data-bs-target='#carouselExampleDark' data-bs-slide='prev'>
                                    <span class='carousel-control-prev-icon' aria-hidden='true'></span><span class='visually-hidden'>Previous</span></button>
                                    <button class='carousel-control-next' type='button' data-bs-target='#carouselExampleDark' data-bs-slide='next'>
                                    <span class='carousel-control-next-icon' aria-hidden='true'></span><span class='visually-hidden'>Next</span></button></div></div></div>";
                        }
                    }

                    txtName.Text = objUser.FullName;

                    if (objUser.Address != null && objUser.Address != "")
                        txtAddress.Text += objUser.Address + ", ";
                    if (objUser.City != null && objUser.City != "")
                        txtAddress.Text += objUser.City + ", ";
                    if (objUser.Country != null && objUser.Country != "")
                        txtAddress.Text += objUser.Country + ".";

                    txtWhatsapp.Text = objUser.Contact;

                    //geo.Src = objUser.Pin;
                }
            }
        }
    }
}