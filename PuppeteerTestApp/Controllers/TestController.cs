using Microsoft.AspNetCore.Mvc;

namespace PuppeteerTestApp.Controllers
{
    [ApiController]
    public class TestController : ControllerBase
    {
        [HttpGet]
        [Route("test")]
        public IActionResult TestMethod()
        {
            return Ok();
        }
    }
}
