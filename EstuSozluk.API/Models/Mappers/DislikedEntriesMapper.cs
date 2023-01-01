using EstuSozluk.API.Models.Dtos;

namespace EstuSozluk.API.Models.Mappers
{
    public class DislikedEntriesMapper
    {
        public static DislikedEntries GetDislikedEntriesFromDto(DislikedEntriesDto dislikedEntriesDto)
        {
            DislikedEntries dislikedEntries = new DislikedEntries();

            dislikedEntries.dislikedentryid = dislikedEntriesDto.dislikedentryid;
            dislikedEntries.userid = dislikedEntriesDto.userid;

            return dislikedEntries;
        }
    }
}