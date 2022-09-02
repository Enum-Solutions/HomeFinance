using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace BN_HomeFinance.Resources
{
    [Serializable]
    public class BNImage : BNImage_Factory
    {
        public string ImageID { get; set; }

        public string ImageName { get; set; }

        public byte[] ImageData { get; set; }

        public string ImageURL { get; set; }
    }

    [Serializable]
    public class BNImageCollection : BNImage_Factory
    {
        private List<BNImage> bnImages = new List<BNImage>();
        public List<BNImage> BNImages
        {
            get { return bnImages; }
            set { bnImages = value; }
        }

        public int Count { get; set; }
    }

    public class BNImage_Constants
    {
        #region Columns

        public const string Col_ImageID = "ImageID";
        public const string Col_ImageName = "ImageName";
        public const string Col_ImageData = "ImageData";
        public const string Col_CoverImageID = "CoverImageID";
        public const string Col_CoverImageName = "CoverImageName";
        public const string Col_CoverImageData = "CoverImageData";

        #endregion

        #region Procedures

        public const string Proc_SaveImage = "SaveImage";
        public const string Proc_GetImage = "GetImage";
        public const string Proc_GetImages = "GetImages";
        public const string Proc_GetTopImage = "GetTopImage";

        #endregion

        #region Parameters

        public const string Par_ImageID = "@ImageID";
        public const string Par_CoverImageID = "@CoverImageID";
        public const string Par_ImageName = "@ImageName";
        public const string Par_ImageData = "@ImageData";

        #endregion
    }

    [Serializable]
    public class BNImage_Factory
    {
        public BNImage GetImage(string ID)
        {
            BNImage image = new BNImage();

            try
            {
                DataTable dt = Helper.ExecuteSPWithParameters(BNImage_Constants.Proc_GetImage, BNImage_Constants.Par_ImageID, ID);

                if (dt != null && dt.Rows.Count > 0)
                {
                    image.ImageID = dt.Rows[0][BNImage_Constants.Col_ImageID].ToString();
                    image.ImageName = dt.Rows[0][BNImage_Constants.Col_ImageName].ToString();
                    image.ImageData = (byte[])dt.Rows[0][BNImage_Constants.Col_ImageData];

                    if (image != null)
                    {
                        string rarBase64Data = Convert.ToBase64String(image.ImageData);
                        image.ImageURL = string.Format("data:image/png;base64,{0}", rarBase64Data);
                    }
                }
            }
            catch
            {
                throw;
            }

            return image;
        }

        public BNImageCollection GetImages(string IDs)
        {
            BNImageCollection images = new BNImageCollection();

            try
            {
                DataTable dt = Helper.ExecuteSPWithParameters(BNImage_Constants.Proc_GetImages, BNImage_Constants.Par_ImageID, IDs);

                if (dt != null && dt.Rows.Count > 0)
                {
                    images.Count = 0;

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        BNImage image = new BNImage();

                        image.ImageID = dt.Rows[i][BNImage_Constants.Col_ImageID].ToString();
                        image.ImageName = dt.Rows[i][BNImage_Constants.Col_ImageID].ToString();
                        image.ImageData = (byte[])dt.Rows[i][BNImage_Constants.Col_ImageData];

                        if (image.ImageID != null && image.ImageID != "" && image.ImageID != "0")
                        {
                            string rarBase64Data = Convert.ToBase64String(image.ImageData);
                            image.ImageURL = string.Format("data:image/png;base64,{0}", rarBase64Data);
                        }

                        images.BNImages.Add(image);

                        images.Count++;
                    }
                }
            }
            catch
            {
                throw;
            }

            return images;
        }

        public BNImage SaveImage(BNImage image)
        {
            try
            {
                image.ImageID = Helper.ExecuteSPScalarWithParameters(BNImage_Constants.Proc_SaveImage, BNImage_Constants.Col_ImageName, image.ImageName, BNImage_Constants.Par_ImageData, image.ImageData).ToString();
            }
            catch
            {
                throw;
            }

            return image;
        }

        public BNImageCollection SaveImages(BNImageCollection images)
        {
            try
            {
                for (int i = 0; i < images.Count; i++)
                {
                    BNImage image = new BNImage();

                    image = images.BNImages[i];

                    image.ImageID = Helper.ExecuteSPScalarWithParameters(BNImage_Constants.Proc_SaveImage, BNImage_Constants.Col_ImageName, image.ImageName, BNImage_Constants.Par_ImageData, image.ImageData).ToString();

                    images.BNImages[i] = image;
                }
            }
            catch
            {
                throw;
            }

            return images;
        }

        public static string ConcatenateIDS(BNImageCollection collection)
        {

            BNImageCollection newCollection = collection;

            string IDs = string.Empty;

            try
            {
                if (collection.Count > 0)
                {
                    for (int i = 0; i < collection.Count; i++)
                    {
                        if (i == (collection.Count - 1))
                            IDs += collection.BNImages[i].ImageID;
                        else
                            IDs += collection.BNImages[i].ImageID + ",";
                    }
                }
            }
            catch
            {
                throw;
            }
            return IDs;
        }

        public BNImage GetTopImage(string ID)
        {
            BNImage image = new BNImage();

            try
            {
                DataTable dt = Helper.ExecuteSPWithParameters(BNImage_Constants.Proc_GetTopImage, BNImage_Constants.Par_ImageID, ID);

                if (dt != null && dt.Rows.Count > 0)
                {
                    image.ImageID = Convert.ToString(dt.Rows[0][BNImage_Constants.Col_ImageID]);
                    image.ImageName = dt.Rows[0][BNImage_Constants.Col_ImageName].ToString();
                    image.ImageData = (byte[])dt.Rows[0][BNImage_Constants.Col_ImageData];

                    if (image != null)
                    {
                        string rarBase64Data = Convert.ToBase64String(image.ImageData);
                        image.ImageURL = string.Format("data:image/png;base64,{0}", rarBase64Data);
                    }
                }
            }
            catch
            {
                throw;
            }

            return image;
        }
    }
}