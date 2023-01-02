using System;

namespace EstuSozluk.API.Models.Dtos
{
    public class LandingPageResponse
    {

        public int entryid { get; set; }
        public string titlename { get; set; }
        public string content { get; set; }
        public DateTime writedate { get; set; }
        public DateTime editdate { get; set; }
        public int userid { get; set; }
        public string username { get; set; }
        public int LikeCount { get; set; }
        
        public int DislikeCount { get; set; }

    }
}