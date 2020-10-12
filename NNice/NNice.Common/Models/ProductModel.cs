using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace NNice.Common.Models
{
    public partial class ProductModel : BaseEntity
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        public double UnitPrice { get; set; }

    }
}
