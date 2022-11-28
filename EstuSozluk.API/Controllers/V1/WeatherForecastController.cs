using EstuSozluk.API.Models;
using EstuSozluk.API.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EstuSozluk.API.Controllers.V1
{
    [ApiController]
    [Route("v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    public class WeatherForecastController : ControllerBase
    {
        public EstuSozlukContext _estuSozlukContext;

        public WeatherForecastController(EstuSozlukContext context)
        {
            _estuSozlukContext = context;
        }

        /// <summary>
        /// returns random weather forecast.
        /// </summary>
        /// <returns>object</returns>
        [MapToApiVersion("1.0")]
        [HttpGet]
        public object Get()
        {

            // return _estuSozlukContext.Users.Include(e => e.permissions).Include(t => t.entries).Include(t => t.Followed).Include(t => t.Following).Select(q => new { q.Following }).First();
            var Context = _estuSozlukContext;
            var sorgu = Context.Set<User>().Where(e => e.username == "rustuefeuzun")
                .Select(e => new
                {
                    e.username,
                    e.email,
                    e.permissions,
                    Followers = e.Followed.Select(e => e.User1.username).ToList(),
                    Following = e.Following.Select(e => e.User2.username).ToList(),
                    LikedEntries = e.LikedEntries.Select(e => new { e.entry.entryid, e.entry.content }).ToList()
                }).First();

            

            return new
            {
                sorgu.username,
                sorgu.email,
                sorgu.permissions,
                sorgu.Followers,
                sorgu.Following,
                FollowerCount = sorgu.Followers.Count,
                FollowedCount = sorgu.Following.Count,
                LikedEntries = sorgu.LikedEntries
                 
            };
        }
    }
}
