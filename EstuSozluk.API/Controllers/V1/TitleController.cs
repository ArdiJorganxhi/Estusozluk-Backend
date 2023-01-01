using EstuSozluk.API.Repositories;
using EstuSozluk.API.Services.Abstracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace EstuSozluk.API.Controllers.V1
{
    [ApiController]
    [Route("api/[controller]")]
    public class TitleController : ControllerBase
    {
        private IConfiguration _configuration;
        private ITitleService _titleService;
        private EstuSozlukContext _estuSozlukContext;

        public TitleController(IConfiguration configuration, ITitleService TitleService, EstuSozlukContext estuSozlukContext)
        {
            _configuration = configuration;
            _titleService = TitleService;
            _estuSozlukContext = estuSozlukContext;
        }

        [HttpGet("titles")]
        public IActionResult GetAllEntries([FromQuery(Name = "StartingWith")] string StartingWith)
        {
            if (StartingWith == null)
            {
                return Ok(_titleService.GetAllTitlesStartsWith(""));
            }
            return Ok(_titleService.GetAllTitlesStartsWith(StartingWith));
        }

        // This can conflict with tite landing!
        [HttpGet("landing")]
        public IActionResult GetEntriesByTitle()
        {
            return Ok(_titleService.GetMostLikedEntryOfTitles());
        }

        [HttpGet("{title}")]
        public IActionResult GetEntriesByTitleName(string title)
        {
            return Ok(_titleService.GetEntriesByTitleName(title));
        }
    }
}
