using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace NNice.Business.DTO
{
    public class AccountDTO
    {
        public int ID { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public Role Role { get; set; }
        public string Password { get; set; }
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
