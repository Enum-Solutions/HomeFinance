using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace BN_HomeFinance.Resources
{
    [Serializable]
    public class Product : Product_Factory
    {
        public int ProductID { get; set; }

        public string ProductName { get; set; }

        private Category category = new Category();
        public Category Category
        {
            get { return category; }
            set { category = value; }
        }

        private BNUser createdBy = new BNUser();
        public BNUser CreatedBy
        {
            get { return createdBy; }
            set { createdBy = value; }
        }

        public int Quantity { get; set; }

        public double Price { get; set; }

        public DateTime CreatedAt { get; set; }

        public string Description { get; set; }

        public string IsDeliveryPossible { get; set; }

        public string DeliveryTime { get; set; }

        public string DeliveryCharges { get; set; }

        public string IsAvailable { get; set; }

        public string PriceMin { get; set; }

        public string PriceMax { get; set; }

        private BNImage image = new BNImage();
        public BNImage Image
        {
            get { return image; }
            set { image = value; }
        }

        private BNImageCollection images = new BNImageCollection();
        public BNImageCollection Images
        {
            get { return images; }
            set { images = value; }
        }
    }

    [Serializable]
    public class ProductCollection : Product_Factory
    {
        private List<Product> products = new List<Product>();
        public List<Product> Products
        {
            get { return products; }
            set { products = value; }
        }
        public int Count { get; set; }
        public List<Amenity> Amenity { get; set; }
    }

    [Serializable]
    public class Category : Product_Factory
    {
        public int CategoryID { get; set; }
        public string CategoryIDs { get; set; }
        public string CategoryName { get; set; }

        public const string col_CategoryID = "CategoryID";
        public const string col_CategoryName = "CategoryName";

        public const string par_CategoryID = "@CategoryID";
        public const string par_CategoryName = "@CategoryName";
        public const string par_CategoryIDs = "@CategoryIDs";

        public const string proc_GetCategory = "GetCategories";
    }

    public class Product_Constants
    {
        #region Columns

        public const string col_ProductID = "ProductID";
        public const string col_ProductName = "ProductName";
        public const string col_Quantity = "Quantity";
        public const string col_Price = "Price";
        public const string col_CreatedAt = "CreatedAt";
        public const string col_Description = "Description";
        public const string col_IsDeliveryPossible = "IsDeliveryPossible";
        public const string col_DeliveryTime = "DeliveryTime";
        public const string col_DeliveryCharges = "DeliveryCharges";
        public const string col_IsAvailable = "IsAvailable";

        #endregion

        #region Procedures

        public const string proc_GetProduct = "GetProduct";
        public const string proc_DeleteProduct = "DeleteProduct";
        public const string proc_InsertProduct = "InsertProduct";
        public const string proc_GetProductsByUserID = "GET_PRODUCTS_BY_USER_ID";
        public const string proc_GetMyProducts = "GetMyProducts";
        public const string proc_GetMaxProductValue = "GetMaxProductValue";
        public const string proc_SearchProduct = "SEARCH_PRODUCTS";
        public const string proc_CreateProductCategory = "CreateProductCategories";
        public const string proc_ReadProductCategoryByID = "ReadProductCategoriesByID";
        public const string proc_DeleteProductCategoryByID = "DeleteProductCategoriesByID";
        public const string proc_UpdateProductCategoryByID = "UpdateProductCategoriesByID";

        #endregion

        #region Parameters

        public const string par_ProductID = "@ProductID";
        public const string par_ProductName = "@ProductName";
        public const string par_Quantity = "@Quantity";
        public const string par_Price = "@Price";
        public const string par_CreatedAt = "@CreatedAt";
        public const string par_Description = "@Description";
        public const string par_IsDeliveryPossible = "@IsDeliveryPossible";
        public const string par_DeliveryTime = "@DeliveryTime";
        public const string par_DeliveryCharges = "@DeliveryCharges";
        public const string par_IsAvailable = "@IsAvailable";
        public const string par_PriceMin = "@PriceMin";
        public const string par_PriceMax = "@PriceMax";

        #endregion
    }

    [Serializable]
    public class Product_Factory
    {
        public Product GetProduct(string ProductID)
        {
            Product product = new Product();

            try
            {
                DataTable dt = Helper.ExecuteSPWithParameters(Product_Constants.proc_GetProduct, Product_Constants.par_ProductID, ProductID);

                if (dt != null && dt.Rows.Count > 0)
                {
                    for (int count = 0; count < dt.Rows.Count; count++)
                    {
                        product.ProductID = Convert.ToInt32(dt.Rows[count][Product_Constants.col_ProductID]);
                        product.ProductName = dt.Rows[count][Product_Constants.col_ProductName].ToString();
                        product.Category.CategoryID = Convert.ToInt32(dt.Rows[count][Category.col_CategoryID]);
                        product.Category.CategoryName = dt.Rows[count][Category.col_CategoryName].ToString();
                        product.Quantity = Convert.ToInt16(dt.Rows[count][Product_Constants.col_Quantity]);
                        product.Price = Convert.ToDouble(dt.Rows[count][Product_Constants.col_Price]);
                        product.CreatedBy.UserID = dt.Rows[count][User_Constants.Col_UserID].ToString();
                        product.CreatedAt = Convert.ToDateTime(dt.Rows[count][Product_Constants.col_CreatedAt]);
                        product.Description = dt.Rows[count][Product_Constants.col_Description].ToString();
                        product.IsDeliveryPossible = dt.Rows[count][Product_Constants.col_IsDeliveryPossible].ToString();
                        try { product.DeliveryTime = dt.Rows[count][Product_Constants.col_DeliveryTime].ToString(); }
                        catch { product.DeliveryTime = ""; }
                        product.DeliveryCharges = dt.Rows[count][Product_Constants.col_DeliveryCharges].ToString();
                        product.IsAvailable = dt.Rows[count][Product_Constants.col_IsAvailable].ToString();

                        BNImageCollection collection = new BNImageCollection();
                        if (dt.Rows[count][BNImage_Constants.Col_ImageID].ToString() != "")
                            product.Images = collection.GetImages(dt.Rows[count][BNImage_Constants.Col_ImageID].ToString());
                        else
                            product.Images.Count = 0;
                    }
                }
            }
            catch
            {
                throw;
            }

            return product;
        }

        public string InsertImages(BNImageCollection images)
        {
            string imageID = "";

            try
            {
                if (images != null && images.Count > 0)
                {
                    for (int count = 0; count < images.Count; count++)
                    {
                        BNImage image = images.BNImages[count];

                        image.ImageID = Helper.ExecuteSPScalarWithParameters(BNImage_Constants.Proc_SaveImage, BNImage_Constants.Col_ImageName, image.ImageName, BNImage_Constants.Par_ImageData, image.ImageData).ToString();

                        if (count == (images.Count - 1))
                            imageID += image.ImageID;
                        else
                            imageID += image.ImageID + ",";

                        images.BNImages[count] = image;
                    }
                }
            }
            catch
            {
                throw;
            }
            return imageID;
        }

        public Product InsertProduct(Product product)
        {
            try
            {
                if (product != null)
                {
                    product.ProductID = Convert.ToInt32(Helper.ExecuteSPScalarWithParameters(Product_Constants.proc_InsertProduct,
                    Product_Constants.par_ProductName, product.ProductName,
                    Category.par_CategoryID, product.Category.CategoryID.ToString(),
                    User_Constants.Par_UserID, product.CreatedBy.UserID,
                    Product_Constants.par_Quantity, product.Quantity.ToString(),
                    Product_Constants.par_Price, product.Price.ToString(),
                    Product_Constants.par_CreatedAt, product.CreatedAt.ToString(),
                    Product_Constants.par_Description, product.Description,
                    Product_Constants.par_IsDeliveryPossible, product.IsDeliveryPossible,
                    Product_Constants.par_DeliveryTime, product.DeliveryTime,
                    Product_Constants.par_DeliveryCharges, product.DeliveryCharges,
                    Product_Constants.par_IsAvailable, product.IsAvailable.ToString(),
                    BNImage_Constants.Par_ImageID, InsertImages(product.Images)));
                }
            }
            catch
            {
                throw;
            }

            return product;
        }

        public bool DeleteProduct(string ProductID)
        {
            bool b = false;

            try
            {
                Helper.ExecuteSPScalarWithParameters(Product_Constants.proc_DeleteProduct, Product_Constants.par_ProductID, ProductID);
                b = true;
            }
            catch
            {
                throw;
            }

            return b;
        }

        public Product GetProducts(string ProductID)
        {
            Product product = new Product();

            try
            {
                DataTable dt = Helper.ExecuteSPWithParameters(Product_Constants.proc_GetProduct, Product_Constants.par_ProductID, ProductID);

                if (dt != null && dt.Rows.Count > 0)
                {
                    for (int count = 0; count < dt.Rows.Count; count++)
                    {
                        product.ProductID = Convert.ToInt32(dt.Rows[count][Product_Constants.col_ProductID]);
                        product.ProductName = dt.Rows[count][Product_Constants.col_ProductID].ToString();
                        product.Category.CategoryID = Convert.ToInt32(dt.Rows[count][Category.col_CategoryID]);
                        product.Category.CategoryName = dt.Rows[count][Category.col_CategoryName].ToString();
                        product.Quantity = Convert.ToInt16(dt.Rows[count][Product_Constants.col_Quantity]);
                        product.Price = Convert.ToInt32(dt.Rows[count][Product_Constants.col_Price]);
                        product.CreatedAt = Convert.ToDateTime(dt.Rows[count][Product_Constants.col_CreatedAt]);
                        product.Description = dt.Rows[count][Product_Constants.col_Description].ToString();
                        product.IsDeliveryPossible = dt.Rows[count][Product_Constants.col_IsDeliveryPossible].ToString();
                        product.DeliveryTime = dt.Rows[count][Product_Constants.col_DeliveryTime].ToString();
                        product.DeliveryCharges = dt.Rows[count][Product_Constants.col_DeliveryCharges].ToString();
                        product.IsAvailable = dt.Rows[count][Product_Constants.col_IsAvailable].ToString();

                        BNImageCollection collection = new BNImageCollection();
                        product.Images = collection.GetImages(dt.Rows[count][BNImage_Constants.Par_ImageID].ToString());
                    }
                }
            }
            catch
            {
                throw;
            }

            return product;
        }

        public ProductCollection GetProductsByUserID(string userID)
        {
            ProductCollection productCollection = new ProductCollection();
            productCollection.Count = 0;
            try
            {
                DataTable dt = Helper.ExecuteSPWithParameters(Product_Constants.proc_GetProductsByUserID,
                    User_Constants.Par_UserID, userID);

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Product objProduct = new Product();
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        DataRow dr = dt.Rows[i];
                        objProduct.ProductID = Convert.ToInt32(dr[Product_Constants.col_ProductID]);
                        objProduct.ProductName = dr[Product_Constants.col_ProductName].ToString();
                        objProduct.Category.CategoryID = Convert.ToInt16(dr[Category.col_CategoryID].ToString());
                        objProduct.Category.CategoryName = dr[Category.col_CategoryName].ToString();
                        objProduct.CreatedBy.UserID = dr[User_Constants.Col_UserID].ToString();
                        objProduct.Quantity = Convert.ToInt32(dr[Product_Constants.col_Quantity]);
                        objProduct.Price = Convert.ToInt32(dr[Product_Constants.col_Price]);
                        objProduct.CreatedAt = (DateTime)dr[Product_Constants.col_CreatedAt];
                        objProduct.Description = dr[Product_Constants.col_Description].ToString();
                        objProduct.IsDeliveryPossible = Convert.ToString(dr[Product_Constants.col_IsDeliveryPossible]);
                        objProduct.DeliveryTime = dr[Product_Constants.col_DeliveryTime].ToString();
                        objProduct.DeliveryCharges = Convert.ToString(dr[Product_Constants.col_DeliveryCharges]);
                        objProduct.IsAvailable = Convert.ToString(dr[Product_Constants.col_IsAvailable]);
                        objProduct.Image.ImageID = Convert.ToString(dr[BNImage_Constants.Col_ImageID]);
                        //objProperty.Image.ImageName = dr[BNImage_Constants.Col_ImageName].ToString();
                        //objProperty.Image.ImageData = (byte[])dr[BNImage_Constants.Col_ImageData];

                        //if (objProperty.Image != null)
                        //{
                        //    string rarBase64Data = Convert.ToBase64String(objProperty.Image.ImageData);
                        //    objProperty.Image.ImageURL = string.Format("data:image/png;base64,{0}", rarBase64Data);
                        //}

                        productCollection.Products.Add(objProduct);
                        productCollection.Count++;
                    }
                }
            }
            catch
            {
                throw;
            }

            return productCollection;
        }

        public ProductCollection GetAllProducts()
        {
            ProductCollection objCollection = new ProductCollection();

            objCollection.Count = 0;
            try
            {
                DataTable dt = Helper.ExecuteSPWithNoParameters(PropertyConstants.proc_GetAllProducts);

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Product product = new Product();

                    if (dt != null && dt.Rows.Count > 0)
                    {
                        DataRow dr = dt.Rows[i];
                        product.ProductID = Convert.ToInt32(dr[Product_Constants.col_ProductID]);
                        product.ProductName = dr[Product_Constants.col_ProductName].ToString();
                        product.Price = Convert.ToInt32(dr[Product_Constants.col_Price]);
                        product.Category.CategoryID = Convert.ToInt32(dr[Category.col_CategoryID]);
                        product.Category.CategoryName = dr[Category.col_CategoryName].ToString();

                        product.Image.ImageID = Convert.ToString(dr[PropertyConstants.col_PropertyImageID]);
                        product.Image.ImageData = (byte[])dr[BNImage_Constants.Col_ImageData];

                        if (product.Image.ImageData != null)
                        {
                            product.Image.ImageURL = Helper.ConvertUrlFromByteArray(product.Image.ImageData);
                        }
                        else
                        {
                            product.Image.ImageURL = Helper.pc_NoProperty;
                        }
                        objCollection.Products.Add(product);
                        objCollection.Count++;
                    }
                }
            }
            catch
            {
                throw;
            }

            return objCollection;
        }

        public DataTable GetAllProductsDatatable()
        {
            DataTable dt = Helper.ExecuteSPWithNoParameters(PropertyConstants.proc_GetAllProducts);
            return dt;
        }

        public DataTable GetMyProductsDatatable(string UserID)
        {
            DataTable dt = new DataTable();

            try
            {
                dt = Helper.ExecuteSPWithParameters(Product_Constants.proc_GetMyProducts, User_Constants.Par_UserID, UserID);
            }
            catch
            {
                throw;
            }

            return dt;
        }

        public ProductCollection SearchProduct(string ProductTypes, string valMin, string valMax)
        {
            ProductCollection collection = new ProductCollection();

            collection.Count = 0;
            try
            {
                DataTable dt = Helper.ExecuteSPWithParameters(Product_Constants.proc_SearchProduct, Category.par_CategoryIDs, ProductTypes, Product_Constants.par_PriceMin, valMin, Product_Constants.par_PriceMax, valMax);

                if (dt != null && dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        Product product = new Product();

                        DataRow dr = dt.Rows[i];

                        product.ProductID = Convert.ToInt32(dr[Product_Constants.col_ProductID]);
                        product.ProductName = dr[Product_Constants.col_ProductName].ToString();
                        product.Category.CategoryID = Convert.ToInt32(dr[Category.col_CategoryID]);
                        product.Category.CategoryName = dr[Category.col_CategoryName].ToString();
                        try { product.Price = Convert.ToInt32(dr[Product_Constants.col_Price]); } catch { product.Price = 0; }

                        product.Image.ImageID = Convert.ToString(dr[BNImage_Constants.Col_ImageID]);
                        product.Image.ImageData = (byte[])dr[BNImage_Constants.Col_ImageData];

                        if (product.Image.ImageData != null)
                        {
                            product.Image.ImageURL = Helper.ConvertUrlFromByteArray(product.Image.ImageData);
                        }

                        collection.Products.Add(product);
                        collection.Count++;
                    }
                }
            }
            catch
            {
                throw;
            }

            return collection;
        }

        public bool CreateProductCategory(Category objCategory)
        {
            bool b = false;
            try
            {
                Helper.ExecuteSPScalarWithParameters(Product_Constants.proc_CreateProductCategory, Category.par_CategoryName,
                            objCategory.CategoryName);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return b;
        }

        public Category ReadProductCategoryByID(string ID)
        {
            Category objCategory = new Category();
            try
            {
                if (objCategory != null)
                {
                    DataTable dt = Helper.ExecuteSPWithParameters(Product_Constants.proc_ReadProductCategoryByID, Category.par_CategoryID, ID);
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            DataRow dr = dt.Rows[i];
                            objCategory.CategoryName = dr[Category.col_CategoryName].ToString();

                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return objCategory;
        }

        public void UpdateProductCategoryByID(Category objCategory)
        {
            if (objCategory != null)
            {
                Helper.ExecuteSPScalarWithParameters(Product_Constants.proc_UpdateProductCategoryByID, Category.par_CategoryID, Convert.ToString(objCategory.CategoryID),
                    Category.par_CategoryName, objCategory.CategoryName
                );
            }
        }

        public void DeleteProductCategoryByID(string ID)
        {
            Category objCategory = new Category();
            try
            {
                if (objCategory != null)
                {
                    Helper.ExecuteSPScalarWithParameters(Product_Constants.proc_DeleteProductCategoryByID, Category.par_CategoryID, ID);
                }
            }
            catch
            {
                throw;
            }
        }

    }
}