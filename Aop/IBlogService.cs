namespace Aop
{
    public interface IBlogService
    {
        bool AddBlog(BlogModel blogModel);
        BlogModel GetBlogById(int id);
    }
}
