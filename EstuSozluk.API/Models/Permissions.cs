using System;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace EstuSozluk.API.Models
{
    public class Permissions
    {
        [Key]
        public int userroleid { get; set; }
        public string rolename { get; set; }
     

        [JsonIgnore]
        public virtual User user { get; set; }

    }
}
