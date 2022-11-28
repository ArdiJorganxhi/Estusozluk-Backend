using System;
using System.Collections.Generic;

namespace EstuSozluk.API.Models
{
    public class Entry
    {

      
        public int entryid { get; set; }
        public int userid { get; set; }
        
     
        public string titlename { get; set; }
        public string content { get; set; }
        public DateTime writedate { get; set; }
        public DateTime editdate { get; set; }

        public virtual User User { get; set; }
        public virtual ICollection<LikedEntries> LikedEntries { get; set; }
        public virtual ICollection<DislikedEntries> DislikedEntries { get; set; }

        public Entry()
        {
            
        }
    }
}
