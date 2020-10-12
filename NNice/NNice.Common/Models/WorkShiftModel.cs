using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace NNice.Common.Models
{
    [Table("WorkShifts")]
    public partial class WorkShiftModel : BaseEntity
    {
        [Required]
        public DateTime WorkDate { get; set; }
        [Required]
        public int ShiftNumber { get; set; }
    }
}
