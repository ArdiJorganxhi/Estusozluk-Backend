using System;
using System.Collections;
using System.Collections.Generic;

namespace EstuSozluk.API.Models
{
    public class User
    {
       public int userid { get; set; }
       public string username { get; set; }
       public string email { get; set; }
       public string password { get; set; }
       public int userroleId { get; set; }

       public virtual ICollection<Entry> entries { get; set; }
       public virtual Permissions permissions { get; set; }
       public virtual ICollection<Followships> Following { get; set; }
       public virtual ICollection<Followships> Followed { get; set; }
       public virtual ICollection<LikedEntries> LikedEntries { get; set; }
       public virtual ICollection<DislikedEntries> DislikedEntries { get; set; }


    }
}
