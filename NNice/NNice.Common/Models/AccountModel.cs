using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace NNice.Common.Models
{
    [Table("Accounts")]
    public partial class AccountModel : BaseEntity
    {
        [Required, MinLength(6), MaxLength(50)]
        public string Username { get; set; }
        [Required, MinLength(6), MaxLength(256)]
        public Role Role { get; set; }
        public string Password { get; set; }
        public byte[] PasswordHash { get; set; }
        [Required]
        public byte[] PasswordSalt { get; set; }
        public string Avatar { get; set; }
        public string Token { get; set; }
    }
    public enum Role
    {
        Admin = 1,
        Cashier = 2,
        Accountant = 3
    }
}
