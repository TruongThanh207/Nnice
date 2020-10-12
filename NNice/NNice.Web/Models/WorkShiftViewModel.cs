using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NNice.Web.Models
{
    public class WorkShiftViewModel
    {
        public List<WorkShiftModel> workShifts;
        public List<DateTime> dates { get; set; }
    }
}
