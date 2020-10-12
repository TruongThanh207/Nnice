using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NNice.Web.Models
{
    public class WorkShiftDetailModel
    {
        public int ID { get; set; }
        [Required]
        public List<int> Employees { get; set; }
        [Required]
        public DateTime WorkDate { get; set; }
        [Required, Range(1, 4)]
        public int ShiftNumber { get; set; }
    }
}
