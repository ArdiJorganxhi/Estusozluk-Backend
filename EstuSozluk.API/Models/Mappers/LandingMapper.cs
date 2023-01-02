using EstuSozluk.API.Models.Dtos;

namespace EstuSozluk.API.Models.Mappers
{
    public class LandingMapper
    {
        public static LandingPageResponse MapFrom(Entry entry, int LikeCount, int DislikeCount)
        {

            LandingPageResponse response = new LandingPageResponse();

            response.entryid = entry.entryid;

            response.titlename = entry.titlename;
            response.content = entry.content;
            response.writedate = entry.writedate;
            response.editdate = entry.editdate;
            response.userid = entry.userid;
            response.username = entry.User.username;
            response.LikeCount = LikeCount;
            response.DislikeCount = DislikeCount;

            return response;

        }
    }
}