using EstuSozluk.API.Models.Dtos;

namespace EstuSozluk.API.Models.Mappers
{
    public class LandingMapper
    {
        public static LandingPageResponse MapFrom(Entry entry)
        {

            LandingPageResponse response = new LandingPageResponse();

            response.titlename = entry.titlename;
            response.content = entry.content;
            response.writedate = entry.writedate;
            response.editdate = entry.editdate;
            response.userid = entry.userid;
            response.username = entry.User.username;

            return response;

        }
    }
}