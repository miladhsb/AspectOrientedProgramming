using Microsoft.AspNetCore.Mvc;

namespace Aop.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly IBlogService _blogService;

        public WeatherForecastController(IBlogService blogService)
        {
            this._blogService = blogService;
        }



        [HttpGet()]
        public IActionResult Get()
        {
            _blogService.AddBlog(new BlogModel() { Id=1,PostBody= "PostBody", PostTitle= "PostTitle" });
            return Ok("ok");
        }
    }
}