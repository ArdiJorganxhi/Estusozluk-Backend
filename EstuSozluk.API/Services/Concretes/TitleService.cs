using EstuSozluk.API.Models.Dtos;
using EstuSozluk.API.Models.Mappers;
using EstuSozluk.API.Repositories;
using EstuSozluk.API.Services.Abstracts;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace EstuSozluk.API.Services.Concretes
{
    public class TitleService : ITitleService
    {

        EstuSozlukContext _estuSozlukContext;
        ILoginService _loginService;

        public TitleService(EstuSozlukContext estuSozlukContext, ILoginService loginService)
        {
            _estuSozlukContext = estuSozlukContext;
            _loginService = loginService;
        }

        public IEnumerable<string> GetAllTitlesStartsWith(string StartsWith)
        {
            return _estuSozlukContext.Entries
                .Where(q => q.titlename.StartsWith(StartsWith))
                .GroupBy(e => e.titlename)
                .OrderByDescending(group => group.Count())
                .Select(e => e.Key);
        }

        public IEnumerable<GetEntriesDto> GetEntriesByTitleName(string title)
        {
            return _estuSozlukContext.Entries
                .Where(e => e.titlename == title)
                .Include(e => e.User)
                .Include(e => e.LikedEntries)
                .Include(e => e.DislikedEntries)
                .Select(e => GetEntriesMapper.GetEntriesDtoByEntry(e))
                .ToList();
        }

        public IEnumerable<KeyValuePair<string, LandingPageResponse>> GetMostLikedEntryOfTitles()
        {
            return _estuSozlukContext.Entries
                .Include(e => e.LikedEntries)
                .Include(e => e.DislikedEntries)
                .Include(e => e.User)
                .Select(e => e)
                .ToList()
                .GroupBy(q => q.titlename)
                .ToDictionary(e => e.Key, e =>
                    e.Select(q => LandingMapper.MapFrom(q, q.LikedEntries.Count, q.DislikedEntries.Count))
                        .OrderByDescending(z => z.LikeCount)
                        .First()
                ).ToList();
        }
    }
}
