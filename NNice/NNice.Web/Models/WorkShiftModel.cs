using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NNice.Web.Models
{
    public class WorkShiftModel
    {
        public int ID { get; set; }
        [Required]
        public List<EmployeeViewModel> Employees { get; set; }
        public DateTime WorkDate { get; set; }
        public int ShiftNumber { get; set; }
    }
}
