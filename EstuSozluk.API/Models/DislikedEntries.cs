using System;
namespace EstuSozluk.API.Models
{
    public class DislikedEntries
    {
        public int userid { get; set; }
        public virtual User user { get; set; }
        public int dislikedentryid { get; set; }
        public virtual Entry entry { get; set; }
    }
}
