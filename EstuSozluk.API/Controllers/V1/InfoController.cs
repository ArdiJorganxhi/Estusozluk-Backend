using Microsoft.AspNetCore.Mvc;

namespace EstuSozluk.API.Controllers.V1
{
    /// <summary>
    ///  A class that indicates the server is responding or not.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class InfoController : ControllerBase
    {

        [HttpGet]
        public IActionResult GetAllEntries()
        {
            return Ok(new
            {
                success = true
            });
        }

    }
}
