using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace NNice.Business.DTO
{
    public class ProductDTO
    {
        public int? ID { get; set; }
        [Required]
        public string Name { get; set; }
        public double UnitPrice { get; set; }
    }
}
