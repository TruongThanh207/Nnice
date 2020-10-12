using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NNice.Web.Models
{
    public class AccountViewModel
    {
        public int ID { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        public Role Role { get; set; }
        public string Avatar { get; set; }
        public string Token { get; set; }
        public string ReturnUrl { get; set; }
        public bool RememberMe { get; set; }
    }
    public enum Role
    {
        Admin = 1,
        Cashier = 2,
        Accountant = 3
    }
}
