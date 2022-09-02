using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace BN_HomeFinance.Resources
{
    public class PostColors : PostColorsFactory
    {
        public int ID { get; set; }

        public int PostID { get; set; }

        public string HeaderColor { get; set; }

        public string DescriptionColor { get; set; }

        public string Module { get; set; }
    }

    public class PostColorsFactory
    {
        public PostColors GetPostColors(PostColors color)
        {
            DataTable dt = Helper.ExecuteSPWithParameters(PostColorsConstants.Proc_GetColor, PostColorsConstants.Par_PostID, color.PostID.ToString(), PostColorsConstants.Par_Module, color.Module);

            if (dt != null)
            {
                BNImage image = new BNImage();

                color.ID = Convert.ToInt32(dt.Rows[0][PostColorsConstants.Col_ID]);
                color.PostID = Convert.ToInt32(dt.Rows[0][PostColorsConstants.Col_PostID].ToString());
                color.HeaderColor = dt.Rows[0][PostColorsConstants.Col_HeaderColor].ToString();
                color.DescriptionColor = dt.Rows[0][PostColorsConstants.Col_DescriptionColor].ToString();
                color.Module = dt.Rows[0][PostColorsConstants.Col_Module].ToString();
            }

            return color;
        }

        public PostColors InsertPostColor(PostColors color)
        {
            Helper.ExecuteSPScalarWithParameters(PostColorsConstants.Proc_InsertColor,
                PostColorsConstants.Par_PostID, color.PostID.ToString(),
                PostColorsConstants.Par_HeaderColor, color.HeaderColor, PostColorsConstants.Par_DescriptionColor, color.DescriptionColor,
                PostColorsConstants.Par_Module, color.Module);

            return color;
        }

        public bool DeletePostColor(PostColors color)
        {
            bool b = false;

            try
            {
                Helper.ExecuteSPScalarWithParameters(PostColorsConstants.Proc_DeleteColor, PostColorsConstants.Par_PostID, color.PostID.ToString(), PostColorsConstants.Par_Module, color.Module);

                b = true;
            }
            catch
            {
                throw;
            }
            return b;
        }
    }

    public class PostColorsConstants
    {
        #region Parameters

        public const string Par_ID = "@ID";
        public const string Par_PostID = "@PostID";
        public const string Par_HeaderColor = "@HeaderColor";
        public const string Par_DescriptionColor = "@DescriptionColor";
        public const string Par_Module = "@Module";

        #endregion

        #region Columns

        public const string Col_ID = "ID";
        public const string Col_PostID = "PostID";
        public const string Col_HeaderColor = "HeaderColor";
        public const string Col_DescriptionColor = "DescriptionColor";
        public const string Col_Module = "Module";

        #endregion

        #region Procedures

        public const string Proc_GetColor = "GetColor";
        public const string Proc_InsertColor = "InsertColor";
        public const string Proc_DeleteColor = "DeleteColor";

        #endregion
    }
}