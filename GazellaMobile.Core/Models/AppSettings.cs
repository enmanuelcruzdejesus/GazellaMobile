
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace GazellaMobile.Models
{
    [Table("AppSettings")]
    public class AppSettings
    {
        [PrimaryKey]
        public int ID { get; set; }
        public string Server { get; set; }
        public string UserId { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public bool AllowKeepLog { get; set; }


    }
}
