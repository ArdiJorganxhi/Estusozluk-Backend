using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using EstuSozluk.API.Models;
using EstuSozluk.API.Models.Dtos;
using EstuSozluk.API.Models.Mappers;
using EstuSozluk.API.Services.Abstracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EstuSozluk.API.Controllers.V1
{
    // [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private IConfiguration _config;
        private ILoginService _LoginService;

        public UserController(IConfiguration config, ILoginService loginService)
        {

            _config = config;
            _LoginService = loginService;

        }

        [Route("api/[controller]/{username}")]
        [HttpGet]
       
       
        public IActionResult GetUser(string username)
        {
            var CheckUser = _LoginService.GetUser(username);
            return Ok(CheckUser);
        }


        [Route("api/[controller]")]
        [HttpPost]
       
        public IActionResult RegisterUser([FromBody] UserRegistrationDto user)
        {
            return Ok(_LoginService.SaveUser(user));
        }

        
        [Route("api/[controller]/login")]
        [HttpPost]
        
        public IActionResult LoginUser([FromBody] UserLoginDto user)
        {
            object response = _LoginService.Login(user);
            if(response == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(response);
            }
        }

        [Route("api/[controller]/follow")]
        [HttpPost]

        public IActionResult FollowUser([FromBody] FollowshipsDto followshipsDto)
        {
            return Ok(_LoginService.Follow(followshipsDto));
        }



        /*
        [HttpGet("Admins")]
        [Authorize(Roles = "Administrator")]
        public IActionResult AdminsEndpoint()
        {
            var currentUser = GetCurrentUser();

            return Ok($"Hi {currentUser.nickname}, you are an {currentUser.roleId}");
        }

        [HttpGet("Public")]
      public IActionResult Public()
        {
               var currentUser = GetCurrentUser();

            return Ok($"Hi {currentUser.nickname}, you are an {currentUser.roleId}");
        }

        private User GetCurrentUser()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;

            if (identity != null)
            {
                var userClaims = identity.Claims;

                return new User
                {
                    username = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.NameIdentifier)?.Value,
                    email = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Email)?.Value,
                    nickname = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.GivenName)?.Value,


                };
            }
            return null;
        }
        */
    }
}


