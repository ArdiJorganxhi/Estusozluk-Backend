using EstuSozluk.API.Models.Dtos;
using System.Collections.Generic;

namespace EstuSozluk.API.Services.Abstracts
{
    public interface ITitleService
    {
        public IEnumerable<string> GetAllTitlesStartsWith(string StartsWith);
        public IEnumerable<KeyValuePair<string, LandingPageResponse>> GetMostLikedEntryOfTitles();
        public IEnumerable<GetEntriesDto> GetEntriesByTitleName(string title);
    }
}
