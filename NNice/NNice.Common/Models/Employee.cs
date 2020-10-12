using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace NNice.Common.Models
{
    public partial class Employee : BaseEntity
    {
        [Required, MaxLength(50)]
        public string FirstName { get; set; }
        [Required, MaxLength(50)]
        public string LastName { get; set; }
        [MaxLength(200)]
        public string Address { get; set; }
        [MaxLength(100)]
        public string Email { get; set; }

        public decimal Salary { get; set; }
        public int? AccountID { get; set; }
    }
}
