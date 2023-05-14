namespace Aop
{
    public class BlogService: IBlogService
    {

       

        public bool AddBlog(BlogModel blogModel)
        {
            Console.WriteLine("add blog");
            return true;
        }


        public BlogModel GetBlogById(int id)
        {
            return new BlogModel() { Id = id, PostBody = "PostBody", PostTitle = "PostTitle" };
        }
    }
}
