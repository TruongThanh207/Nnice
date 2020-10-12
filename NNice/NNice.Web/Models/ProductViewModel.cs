using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NNice.Web.Models
{
    public class ProductViewModel
    {
        public int? ID { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public double UnitPrice { get; set; }
    }
}
