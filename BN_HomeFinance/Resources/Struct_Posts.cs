using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using BN_HomeFinance.Resources;

namespace BN_HomeFinance.Resources
{
    [Serializable]
    public class BNPostCollection : BNPostFactory
    {
        private List<BNPost> posts = new List<BNPost>();
        public List<BNPost> Posts
        {
            get { return posts; }
            set { posts = value; }
        }

        private int count = 0;
        public int Count
        {
            get { return count; }
            set { count = value; }
        }
    }

    [Serializable]
    public class PostCollection : BNPostFactory
    {
        private List<BNPost> bnposts = new List<BNPost>();
        public List<BNPost> BNPosts
        {
            get { return bnposts; }
            set { bnposts = value; }
        }
        public int Count { get; set; }

        public BNPost ImageID { get; set; }

        public string Module = "";

        public PostCollection()
        {

        }
        public PostCollection(Helper.Module Mode)
        {
            Module = Mode.ToString();
        }
    }

    [Serializable]
    public class BNPost : BNPostFactory
    {
        public int ID { get; set; }

        public string Header { get; set; }

        public string Description { get; set; }

        private BNImage image = new BNImage();
        public BNImage Image
        {
            get { return image; }
            set { image = value; }
        }

        public DateTime DateCreated { get; set; }

        public string Status { get; set; }

        private BNUser creator = new BNUser();
        public BNUser Creator
        {
            get { return creator; }
            set { creator = value; }
        }

        public string Module = "";

        public BNPost()
        {
            Module = "None";
        }

        public BNPost(Helper.Module Mode)
        {
            Module = Mode.ToString();
        }
    }

    [Serializable]
    public class BNPostFactory
    {
        public BNPost GetPost(BNPost post)
        {
            DataTable dt = Helper.ExecuteSPWithParameters(BNPostConstants.Proc_GetPost, BNPostConstants.Par_ID, post.ID.ToString(), BNPostConstants.Par_Module, post.Module);

            if (dt != null)
            {
                BNImage image = new BNImage();

                post.ID = Convert.ToInt32(dt.Rows[0][BNPostConstants.Col_ID]);
                post.Header = dt.Rows[0][BNPostConstants.Col_Header].ToString();
                post.Description = dt.Rows[0][BNPostConstants.Col_Description].ToString();
                post.Status = dt.Rows[0][BNPostConstants.Col_Status].ToString();
                post.DateCreated = Convert.ToDateTime(dt.Rows[0][BNPostConstants.Col_DateCreated]);
                post.Creator.FullName = dt.Rows[0][User_Constants.Col_FullName].ToString();
                post.Creator.UserID = dt.Rows[0][BNPostConstants.Col_CreatedBy].ToString();
                post.Image = image.GetImage(dt.Rows[0][BNImage_Constants.Col_ImageID].ToString());
            }

            return post;
        }

        public BNPost InsertPost(BNPost Post)
        {
            if (Post.Image.ImageData != null)
                Post.Image.ImageID = (Post.Image.SaveImage(Post.Image)).ImageID;
            else
                Post.Image.ImageID = "0";

            //if (Convert.ToInt32(Post.Image.ImageID) > 0)
            Post.ID = Convert.ToInt32(Helper.ExecuteSPScalarWithParameters(BNPostConstants.Proc_InsertPost,
                BNPostConstants.Par_Header, Post.Header, BNPostConstants.Par_Description, Post.Description,
                BNImage_Constants.Par_ImageID, Post.Image.ImageID.ToString(), BNPostConstants.Par_DateCreated, DateTime.Now.ToString(),
                User_Constants.Par_UserID, Post.Creator.UserID, BNPostConstants.Par_Status, Post.Status, BNPostConstants.Par_Module, Post.Module));

            return Post;
        }

        public bool DeletePost(BNPost post)
        {
            bool b = false;

            try
            {
                Helper.ExecuteSPScalarWithParameters(BNPostConstants.Proc_DeletePost, BNPostConstants.Par_ID, post.ID.ToString(), BNPostConstants.Par_Module, post.Module);

                b = true;
            }
            catch
            {
                throw;
            }
            return b;
        }

        public DataTable GetAllPostsDatatable(BNPost post)
        {
            DataTable dt = new DataTable();

            try
            {
                dt = Helper.ExecuteSPWithParameters(BNPostConstants.Proc_GetAllPosts, BNPostConstants.Par_Module, post.Module);
            }
            catch
            {
                throw;
            }

            return dt;
        }

        public DataTable GetAllPostsDatatableActive(BNPost post)
        {
            DataTable dt = new DataTable();

            try
            {
                dt = Helper.ExecuteSPWithParameters(BNPostConstants.Proc_GetAllActivePosts, BNPostConstants.Par_Module, post.Module);
            }
            catch
            {
                throw;
            }

            return dt;
        }

        public BNPostCollection GetAllActivePosts(BNPost pos)
        {
            BNPostCollection collection = new BNPostCollection();

            try
            {
                DataTable dt = GetAllPostsDatatableActive(pos);

                if (dt != null && dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        BNPost post = new BNPost(Helper.Module.Messages);

                        post.ID = Convert.ToInt32(dt.Rows[i][BNPostConstants.Col_ID]);
                        post.Header = dt.Rows[i][BNPostConstants.Col_Header].ToString();
                        post.Description = dt.Rows[i][BNPostConstants.Col_Description].ToString();
                        post.DateCreated = Convert.ToDateTime(dt.Rows[i][BNPostConstants.Col_DateCreated]);
                        post.Creator.FullName = dt.Rows[i][BNPostConstants.Col_CreatedBy].ToString();
                        post.Status = dt.Rows[i][BNPostConstants.Col_Status].ToString();

                        if (post.Status.Equals("Active"))
                        {
                            collection.Posts.Add(post);

                            collection.Count++;
                        }
                    }
                }
            }
            catch
            {
                throw;
            }

            return collection;
        }

        public BNPostCollection GetAllPosts(BNPost pos)
        {
            BNPostCollection collection = new BNPostCollection();

            try
            {
                DataTable dt = GetAllPostsDatatable(pos);

                if (dt != null && dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        BNPost post = new BNPost(Helper.Module.Messages);

                        post.ID = Convert.ToInt32(dt.Rows[i][BNPostConstants.Col_ID]);
                        post.Header = dt.Rows[i][BNPostConstants.Col_Header].ToString();
                        post.Description = dt.Rows[i][BNPostConstants.Col_Description].ToString();
                        post.DateCreated = Convert.ToDateTime(dt.Rows[i][BNPostConstants.Col_DateCreated]);
                        post.Creator.FullName = dt.Rows[i][BNPostConstants.Col_CreatedBy].ToString();
                        post.Status = dt.Rows[i][BNPostConstants.Col_Status].ToString();

                        collection.Posts.Add(post);

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

        public BNPost GetTopPost(BNPost post)
        {
            try
            {
                DataTable dt = Helper.ExecuteSPWithParameters(BNPostConstants.Proc_GetTopPost, BNPostConstants.Par_Module, post.Module);

                if (dt != null && dt.Rows.Count > 0)
                {
                    post.ID = Convert.ToInt32(dt.Rows[0][BNPostConstants.Col_ID]);
                    post.Header = dt.Rows[0][BNPostConstants.Col_Header].ToString();
                    post.Description = dt.Rows[0][BNPostConstants.Col_Description].ToString();
                    post.DateCreated = Convert.ToDateTime(dt.Rows[0][BNPostConstants.Col_DateCreated]);
                    post.Creator.FullName = dt.Rows[0][BNPostConstants.Col_CreatedBy].ToString();
                    post.Status = dt.Rows[0][BNPostConstants.Col_Status].ToString();

                    if (dt.Rows[0][BNImage_Constants.Col_ImageID].ToString() != "")
                    {
                        post.Image.ImageID = dt.Rows[0][BNImage_Constants.Col_ImageID].ToString();
                        post.Image.ImageName = dt.Rows[0][BNImage_Constants.Col_ImageName].ToString();
                        post.Image.ImageData = (byte[])dt.Rows[0][BNImage_Constants.Col_ImageData];
                        if (post.Image.ImageData != null)
                            post.Image.ImageURL = Helper.ConvertUrlFromByteArray(post.Image.ImageData);
                    }
                }
            }
            catch
            {
                throw;
            }

            return post;
        }

        public BNPostCollection GetTopPosts(BNPost pos)
        {
            BNPostCollection collection = new BNPostCollection();

            try
            {
                DataTable dt = Helper.ExecuteSPWithParameters(BNPostConstants.Proc_GetTopPosts, BNPostConstants.Par_Module, pos.Module);

                if (dt != null && dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        BNPost post = new BNPost();

                        post.ID = Convert.ToInt32(dt.Rows[i][BNPostConstants.Col_ID]);
                        post.Header = dt.Rows[i][BNPostConstants.Col_Header].ToString();
                        post.Description = dt.Rows[i][BNPostConstants.Col_Description].ToString();
                        post.DateCreated = Convert.ToDateTime(dt.Rows[i][BNPostConstants.Col_DateCreated]);
                        post.Creator.FullName = dt.Rows[i][BNPostConstants.Col_CreatedBy].ToString();
                        post.Status = dt.Rows[i][BNPostConstants.Col_Status].ToString();

                        if (dt.Rows[i][BNImage_Constants.Col_ImageID].ToString() != "")
                        {
                            post.Image.ImageID = dt.Rows[i][BNImage_Constants.Col_ImageID].ToString();
                            post.Image.ImageName = dt.Rows[i][BNImage_Constants.Col_ImageName].ToString();
                            post.Image.ImageData = (byte[])dt.Rows[i][BNImage_Constants.Col_ImageData];
                            post.Image.ImageURL = Helper.ConvertUrlFromByteArray(post.Image.ImageData);
                        }
                        collection.Posts.Add(post);

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

        public BNPostCollection GetPostsByUserID(string module, string userID)
        {
            BNPostCollection objCollection = new BNPostCollection();
            try
            {
                DataTable dt = Helper.ExecuteSPWithParameters(BNPostConstants.Proc_GetPostsByUserID, BNPostConstants.Par_Module, module, User_Constants.Par_UserID, userID);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    BNPost objPost = new BNPost();

                    if (dt != null && dt.Rows.Count > 0)
                    {
                        DataRow dr = dt.Rows[i];
                        objPost.ID = Convert.ToInt32(dr[BNPostConstants.Col_ID]);
                        objPost.Header = dr[BNPostConstants.Col_Header].ToString();
                        objPost.Description = dr[BNPostConstants.Col_Description].ToString();
                        objPost.DateCreated = (DateTime)dr[BNPostConstants.Col_DateCreated];
                        objPost.Status = dr[BNPostConstants.Col_Status].ToString();
                        objPost.Creator.UserID = dr[BNPostConstants.Col_CreatedBy].ToString();
                        objPost.Image.ImageID = dr[BNImage_Constants.Col_ImageID].ToString();
                        objPost.Image.ImageData = (byte[])dr[BNImage_Constants.Col_ImageData];
                        objPost.Image.ImageName = dr[BNImage_Constants.Col_ImageName].ToString();

                        if (objPost.Image != null)
                        {
                            string rarBase64Data = Convert.ToBase64String(objPost.Image.ImageData);
                            objPost.Image.ImageURL = string.Format("data:image/png;base64,{0}", rarBase64Data);
                        }

                        objCollection.Posts.Add(objPost);
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

        public PostCollection GetPostsByUserIDPost(string module, string userID)
        {
            PostCollection objCollection = new PostCollection();

            try
            {
                DataTable dt = Helper.ExecuteSPWithParameters(BNPostConstants.Proc_GetTopPostsByUserID, BNPostConstants.Par_Module, module, User_Constants.Par_UserID, userID);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    BNPost objPost = new BNPost();
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        DataRow dr = dt.Rows[i];
                        objPost.ID = Convert.ToInt32(dr[BNPostConstants.Col_ID]);
                        objPost.Header = dr[BNPostConstants.Col_Header].ToString();
                        objPost.Description = dr[BNPostConstants.Col_Description].ToString();
                        objPost.DateCreated = (DateTime)dr[BNPostConstants.Col_DateCreated];
                        objPost.Status = dr[BNPostConstants.Col_Status].ToString();
                        objPost.Creator.UserID = dr[BNPostConstants.Col_CreatedBy].ToString();
                        objPost.Image.ImageID = dr[BNImage_Constants.Col_ImageID].ToString();
                        objPost.Image.ImageData = (byte[])dr[BNImage_Constants.Col_ImageData];
                        objPost.Image.ImageName = dr[BNImage_Constants.Col_ImageName].ToString();

                        if (objPost.Image != null)
                        {
                            string rarBase64Data = Convert.ToBase64String(objPost.Image.ImageData);
                            objPost.Image.ImageURL = string.Format("data:image/png;base64,{0}", rarBase64Data);
                        }

                        objCollection.BNPosts.Add(objPost);
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

        public DataTable GetPostsByUserIDDatatable(string module, string userID)
        {
            DataTable dt = new DataTable();

            try
            {
                dt = Helper.ExecuteSPWithParameters(BNPostConstants.Proc_GetPostsByUserID, BNPostConstants.Par_Module, module, User_Constants.Par_UserID, userID);

            }
            catch
            {
                throw;
            }

            return dt;
        }

        public BNPostCollection GetAllPostsWithImages(BNPost post)
        {
            BNPostCollection collection = new BNPostCollection();

            try
            {
                DataTable dt = Helper.ExecuteSPWithParameters(BNPostConstants.Proc_GetAllPostsWithImages, BNPostConstants.Par_Module, post.Module);

                if (dt != null && dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        post = new BNPost();

                        post.ID = Convert.ToInt32(dt.Rows[i][BNPostConstants.Col_ID]);
                        post.Header = dt.Rows[i][BNPostConstants.Col_Header].ToString();
                        post.Description = dt.Rows[i][BNPostConstants.Col_Description].ToString();
                        post.DateCreated = Convert.ToDateTime(dt.Rows[i][BNPostConstants.Col_DateCreated]);
                        post.Creator.FullName = dt.Rows[i][BNPostConstants.Col_CreatedBy].ToString();
                        post.Status = dt.Rows[i][BNPostConstants.Col_Status].ToString();
                        post.Image.ImageID = dt.Rows[i][BNImage_Constants.Col_ImageID].ToString();
                        post.Image.ImageName = dt.Rows[i][BNImage_Constants.Col_ImageName].ToString();
                        try { post.Image.ImageData = (byte[])dt.Rows[i][BNImage_Constants.Col_ImageData]; } catch { }
                        if (post.Image.ImageData != null)
                            post.Image.ImageURL = Helper.ConvertUrlFromByteArray(post.Image.ImageData);

                        collection.Posts.Add(post);
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
    }

    public class BNPostConstants
    {
        #region Parameters

        public const string Par_ID = "@ID";
        public const string Par_Header = "@Header";
        public const string Par_Description = "@Description";
        public const string Par_DateCreated = "@Datecreated";
        public const string Par_Status = "@Status";
        public const string Par_Module = "@Module";

        #endregion

        #region Columns

        public const string Col_ID = "ID";
        public const string Col_Header = "Header";
        public const string Col_Description = "Description";
        public const string Col_DateCreated = "DateCreated";
        public const string Col_CreatedBy = "CreatedBy";
        public const string Col_Status = "Status";

        #endregion

        #region Procedures

        public const string Proc_DeletePost = "DeletePost";
        public const string Proc_GetPost = "GetPost";
        public const string Proc_GetPosts = "GetPosts";
        public const string Proc_GetAllPosts = "GetAllPosts";
        public const string Proc_GetAllActivePosts = "GetAllActivePosts";
        public const string Proc_InsertPost = "InsertPost";
        public const string Proc_UpdatePostStatus = "UpdatePostStatus";
        public const string Proc_GetTopPost = "GetTopPost";
        public const string Proc_GetTopPosts = "GetTopPosts";
        public const string Proc_GetPostsByUserID = "GetPostsByUserID";
        public const string Proc_GetTopPostsByUserID = "GetTopPostsByUserID";
        public const string Proc_GetAllPostsWithImages = "GetAllPostsWithImages";

        #endregion

        #region Enums

        public enum Status
        {
            Active,
            Inactive
        }

        #endregion
    }
}