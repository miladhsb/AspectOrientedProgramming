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



        [HttpGet("GetWeatherForecast")]
        public IActionResult Get()
        {
            _blogService.AddBlog(new BlogModel() { Id=1,PostBody="ffs",PostTitle="gdgd"});
            return Ok("ok");
        }
    }
}