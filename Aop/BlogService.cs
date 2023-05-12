namespace Aop
{
    public class BlogService: IBlogService
    {

       

        public bool AddBlog(BlogModel blogModel)
        {
            Console.WriteLine("add blog :"+ blogModel.Id + blogModel.PostTitle);
            return true;
        }


        public BlogModel GetBlogById(int id)
        {
            return new BlogModel() { Id = id, PostBody = "PostBody", PostTitle = "PostTitle" };
        }
    }
}
