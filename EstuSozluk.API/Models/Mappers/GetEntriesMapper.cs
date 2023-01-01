using EstuSozluk.API.Models.Dtos;

namespace EstuSozluk.API.Models.Mappers
{
    public class GetEntriesMapper
    {
        public static GetEntriesDto GetEntriesDtoByEntry(Entry e)
        {
            GetEntriesDto GetEntriesDto = new GetEntriesDto();

            GetEntriesDto.Entry = e;
            GetEntriesDto.User = e.User.username;
            GetEntriesDto.LikeCount = e.LikedEntries.Count;
            GetEntriesDto.DisLikeCount = e.DislikedEntries.Count;


            // Any better way of handling this?
            e.User = null;
            e.LikedEntries = null;
            e.DislikedEntries = null;

            return GetEntriesDto;
        }
    }
}
