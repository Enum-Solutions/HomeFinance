using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BN_HomeFinance.Resources;

namespace BN_HomeFinance
{
    public partial class CreatePromotion : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    FUPromotionImage.Attributes["onchange"] = "UploadFile(this)";

                    if (Request.QueryString[Helper.QueryStrings.PromotionID.ToString()] != null)
                    {
                        btnSubmit.Visible = false;
                        btnUpdate.Visible = true;
                        imgAttachment.Visible = true;
                        btnDelete.Visible = true;

                        divAttachment.Style.Add("display", "none");

                        BNPost Promotion = new BNPost(Helper.Module.Promotions);

                        Promotion.ID = Convert.ToInt32(Request.QueryString[Helper.QueryStrings.PromotionID.ToString()]);

                        Promotion = Promotion.GetPost(Promotion);

                        txtHeader.Text = Promotion.Header;
                        txtDescription.Value = Promotion.Description;
                        ddlStatus.SelectedValue = Promotion.Status;
                        imgAttachment.ImageUrl = Promotion.Image.ImageURL;

                        DataTable dt = new DataTable();

                        dt.Columns.Add(BNImage_Constants.Col_ImageName, typeof(string));
                        dt.Columns.Add(BNImage_Constants.Col_ImageData, typeof(byte[]));

                        DataRow dr = dt.NewRow();

                        dr[BNImage_Constants.Col_ImageName] = Promotion.Image.ImageName;
                        dr[BNImage_Constants.Col_ImageData] = Promotion.Image.ImageData;

                        dt.Rows.Add(dr);

                        ViewState[Helper.ViewStates.Image.ToString()] = dt;
                    }
                }
                else
                {
                    return;
                }
            }
            catch (Exception ex)
            {

            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session[Helper.Sessions.User.ToString()] != null)
                {
                    if (ViewState[Helper.ViewStates.Image.ToString()] != null)
                    {
                        DataTable dt = (DataTable)ViewState[Helper.ViewStates.Image.ToString()];

                        if (dt != null && dt.Rows.Count > 0)
                        {
                            BNPost Promotion = new BNPost(Helper.Module.Promotions);

                            Promotion.Header = txtHeader.Text;
                            Promotion.Description = txtDescription.Value;
                            Promotion.Creator = (BNUser)Session[Helper.Sessions.User.ToString()];
                            Promotion.Status = ddlStatus.SelectedValue;

                            Promotion.Image.ImageData = (byte[])dt.Rows[0][BNImage_Constants.Col_ImageData];
                            Promotion.Image.ImageName = dt.Rows[0][BNImage_Constants.Col_ImageName].ToString();

                            Promotion.InsertPost(Promotion);
                        }
                    }
                    if (Request.QueryString[Helper.QueryStrings.IsAdmin.ToString()] != null
                       && Request.QueryString[Helper.QueryStrings.IsAdmin.ToString()] == "1"
                       && Session[Helper.Sessions.User.ToString()] != null && ((BNUser)Session[Helper.Sessions.User.ToString()]).UserType.UserType == "Admin")
                        ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Promotion Added Successfully');location.href='/Admin/" + Helper.pg_AllPromotions + "'", true);
                    else
                        ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Promotion Added Successfully');location.href='" + Helper.pg_PromotionsTray + "'", true);
                }
            }
            catch (Exception ex)
            {

            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            if (Session[Helper.Sessions.User.ToString()] != null)
            {
                if (ViewState[Helper.ViewStates.Image.ToString()] != null)
                {
                    BNPost Promotion = new BNPost(Helper.Module.Promotions);

                    Promotion.ID = Convert.ToInt32(Request.QueryString[Helper.QueryStrings.PromotionID.ToString()]);

                    if (Promotion.DeletePost(Promotion))
                    {
                        DataTable dt = (DataTable)ViewState[Helper.ViewStates.Image.ToString()];

                        if (dt != null && dt.Rows.Count > 0)
                        {
                            Promotion.Header = txtHeader.Text;
                            Promotion.Description = txtDescription.Value;
                            Promotion.Creator = (BNUser)Session[Helper.Sessions.User.ToString()];
                            Promotion.Status = ddlStatus.SelectedValue;

                            Promotion.Image.ImageData = (byte[])dt.Rows[0][BNImage_Constants.Col_ImageData];
                            Promotion.Image.ImageName = dt.Rows[0][BNImage_Constants.Col_ImageName].ToString();

                            Promotion.InsertPost(Promotion);
                        }
                    }
                }
                if (Request.QueryString[Helper.QueryStrings.IsAdmin.ToString()] != null
                       && Request.QueryString[Helper.QueryStrings.IsAdmin.ToString()] == "1"
                       && Session[Helper.Sessions.User.ToString()] != null && ((BNUser)Session[Helper.Sessions.User.ToString()]).UserType.UserType == "Admin")
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Promotion Updated Successfully');location.href='/Admin/" + Helper.pg_AllPromotions + "'", true);
                else
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Promotion Updated Successfully');location.href='" + Helper.pg_PromotionsTray + "'", true);
            }
        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            try
            {
                divAttachment.Style.Add("display", "none");
                imgAttachment.Visible = true;

                using (Stream stream = FUPromotionImage.PostedFile.InputStream)
                {
                    using (BinaryReader br = new BinaryReader(stream))
                    {
                        byte[] ImageData = br.ReadBytes((Int32)stream.Length);
                        string rarBase64Data = Convert.ToBase64String(ImageData);
                        string ImageURL = string.Format("data:image/png;base64,{0}", rarBase64Data);

                        imgAttachment.ImageUrl = ImageURL;

                        DataTable dt = new DataTable();

                        dt.Columns.Add(BNImage_Constants.Col_ImageName, typeof(string));
                        dt.Columns.Add(BNImage_Constants.Col_ImageData, typeof(byte[]));

                        DataRow dr = dt.NewRow();

                        dr[BNImage_Constants.Col_ImageName] = FUPromotionImage.PostedFile.FileName;
                        dr[BNImage_Constants.Col_ImageData] = ImageData;
                        dt.Rows.Add(dr);

                        ViewState[Helper.ViewStates.Image.ToString()] = dt;
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                BNPost Promotion = new BNPost(Helper.Module.Promotions);

                Promotion.ID = Convert.ToInt32(Request.QueryString[Helper.QueryStrings.PromotionID.ToString()]);

                if (Promotion.DeletePost(Promotion))
                    if (Request.QueryString[Helper.QueryStrings.IsAdmin.ToString()] != null
                       && Request.QueryString[Helper.QueryStrings.IsAdmin.ToString()] == "1"
                       && Session[Helper.Sessions.User.ToString()] != null && ((BNUser)Session[Helper.Sessions.User.ToString()]).UserType.UserType == "Admin")
                        ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Promotion Deleted Successfully');location.href='/Admin/" + Helper.pg_AllPromotions + "'", true);
                    else
                        ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Promotion Deleted Successfully');location.href='" + Helper.pg_PromotionsTray + "'", true);
                else
                    ClientScript.RegisterStartupScript(this.GetType(), "Alert", "alert('This Promotion cant be deleted.')");
            }
            catch
            {

            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            if (Request.QueryString[Helper.QueryStrings.IsAdmin.ToString()] != null
                       && Request.QueryString[Helper.QueryStrings.IsAdmin.ToString()] == "1"
                       && Session[Helper.Sessions.User.ToString()] != null && ((BNUser)Session[Helper.Sessions.User.ToString()]).UserType.UserType == "Admin")
                Response.Redirect("/Admin/" + Helper.pg_AllPromotions);
            else
                Response.Redirect(Helper.pg_PromotionsTray);
        }
    }
}