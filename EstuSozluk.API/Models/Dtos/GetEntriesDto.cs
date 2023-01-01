namespace EstuSozluk.API.Models.Dtos
{
    public class GetEntriesDto
    {
        public Entry Entry { get; set; }
        public int LikeCount { get; set; }
        public int DisLikeCount { get; set; }
        public string User { get; set; }
    }
}
