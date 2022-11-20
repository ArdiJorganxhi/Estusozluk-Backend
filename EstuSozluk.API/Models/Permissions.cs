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
        public int canpost { get; set; }
        public int candelete { get; set; }
        public int canban { get; set; }

        [JsonIgnore]
        public virtual User user { get; set; }

    }
}
