using EstuSozluk.API.Models.Dtos;

namespace EstuSozluk.API.Models.Mappers
{
    public class LikedEntriesMapper
    {
        public static LikedEntries GetLikedEntriesFromDto(LikedEntriesDto likedEntriesDto)
        {
            LikedEntries likedEntries = new LikedEntries();

            likedEntries.likedentryid = likedEntriesDto.likedentryid;
            likedEntries.userid = likedEntriesDto.userid;

            return likedEntries;
        }
    }
}