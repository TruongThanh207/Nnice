using System;
using System.Collections.Generic;
using System.Text;

namespace NNice.Business.DTO
{
    public class EmployeeDTO
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public decimal Salary { get; set; }
        public int? AccountID { get; set; }
    }
}
