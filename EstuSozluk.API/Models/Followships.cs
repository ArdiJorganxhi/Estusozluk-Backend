using System;
namespace EstuSozluk.API.Models
{
    public class Followships
    {
      public int follower { get; set; }
      public virtual User User1 { get; set; }
      public int followed { get; set; }
      public virtual User User2 { get; set; }
      
       
    }
}
