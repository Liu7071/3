using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core
{
    [Table("Users")]
    public class Users : EntityBase
    {
        [StringLength(20)]
        public string UserName { get; set; }
        [StringLength(100, MinimumLength = 6)]
        public string Password { get; set; }
    }
}
