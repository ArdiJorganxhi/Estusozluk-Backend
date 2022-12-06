using System;
using System.Linq;
using EstuSozluk.API.Models;
using EstuSozluk.API.Models.Dtos;
using EstuSozluk.API.Models.Mappers;
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

        [HttpGet("landing")]
        public IActionResult GetEntriesByTitle()
        {

            return Ok(_entryService.GetFirstEntryOfTitle());
        }


        [HttpGet("titles")]
        public IActionResult GetTitles()
        {
            return Ok(_entryService.GetTitles());
        }

        [HttpGet("{title}")]
        public IActionResult GetEntriesByTitleName(string title)
        {
            return Ok(_entryService.GetEntryByTitleName(title));
        }

     
        
        
    }
}