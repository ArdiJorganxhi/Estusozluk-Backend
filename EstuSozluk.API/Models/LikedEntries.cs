using System;
namespace EstuSozluk.API.Models
{
    public class LikedEntries
    {
       public int userid { get; set; }
       public virtual User user { get; set; }
       public int likedentryid { get; set; }
       public virtual Entry entry { get; set; } 
    }
}
