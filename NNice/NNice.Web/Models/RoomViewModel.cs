using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NNice.Web.Models
{
    public class RoomViewModel
    {
        public int? ID { get; set; }  
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }
        public bool IsAvailable { get; set; }
        public int Capacity { get; set; }
    }
}
