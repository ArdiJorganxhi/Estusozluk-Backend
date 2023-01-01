using EstuSozluk.API.Models.Dtos;
using EstuSozluk.API.Repositories;
using EstuSozluk.API.Services.Abstracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace EstuSozluk.API.Controllers.V1
{
    [ApiController]
    [Route("api/[controller]")]
    public class EntryController : ControllerBase
    {
        // GET
        private IConfiguration _configuration;
        private IEntryService _entryService;
        private EstuSozlukContext _estuSozlukContext;

        public EntryController(IConfiguration configuration, IEntryService entryService, EstuSozlukContext estuSozlukContext)
        {
            _configuration = configuration;
            _entryService = entryService;
            _estuSozlukContext = estuSozlukContext;
        }
        [HttpGet]
        public IActionResult GetAllEntries()
        {
            return Ok(_entryService.GetAllEntries());
        }
        [HttpPost]
        [Authorize]
        public IActionResult AddNewEntry([FromBody] EntryDto entry)
        {
            return Ok(_entryService.AddEntry(entry));
        }

        [HttpGet("user/{userid}")]

        public IActionResult GetEntryByUser(int userid)
        {

            return Ok(_entryService.GetEntryByUser(userid));
        }

        [HttpGet("user/liked/{userid}")]

        public IActionResult GetLikedEntryByUser(int userid)
        {
            return Ok(_entryService.GetLikedEntryByUser(userid));
        }

        [HttpGet("user/disliked/{userid}")]

        public IActionResult GetDislikedEntryByUser(int userid)
        {
            return Ok(_entryService.GetDislikedEntryByUser(userid));
        }

        [HttpPost("like")]
        [Authorize]
        public IActionResult LikeEntry([FromBody] LikedEntriesDto likedEntriesDto)
        {
            return Ok(_entryService.LikeEntry(likedEntriesDto));
        }

        [HttpPost("dislike")]
        [Authorize]
        public IActionResult DislikeEntry([FromBody] DislikedEntriesDto dislikedEntriesDto)
        {
            return Ok(_entryService.DislikeEntry(dislikedEntriesDto));
        }

        [HttpDelete("delete/{entryid}")]
        [Authorize]

        public IActionResult DeleteEntry(int entryid)
        {
            return Ok(_entryService.DeleteEntry(entryid));
        }
    }
}