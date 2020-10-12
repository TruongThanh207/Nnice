using System;
using System.Collections.Generic;
using System.Text;

namespace NNice.Business.DTO
{
    public class WorkShiftDTO
    {
        public int ID { get; set; }
        public IEnumerable<EmployeeDTO> Employees { get; set; }
        public DateTime WorkDate { get; set; }
        public int ShiftNumber { get; set; }
    }
}
