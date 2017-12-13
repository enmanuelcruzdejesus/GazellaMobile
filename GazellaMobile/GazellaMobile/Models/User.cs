
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GazellaMobile.Models
{

    [Table("Users")]
    public class User
    {
        [PrimaryKey, MaxLength(15)]
        public string UserId { get; set; }

        [MaxLength(100)]
        public string Username { get; set; }

        [MaxLength(100)]
        public string Password { get; set; }

        public int Status { get; set; }

        public DateTime Created { get; set; }

        public DateTime LastUpdate { get; set; }

        public bool KeepLog { get; set; }

        public User()
        {
            this.UserId = string.Empty;
            this.Username  = string.Empty;
            this.Password= string.Empty;
            
        }

        public User(string UserId, string Password)
        {
            this.UserId = UserId;
            this.Password = Password;
        }

    }
}
