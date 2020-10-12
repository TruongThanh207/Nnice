using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace NNice.Business.DTO
{
    public abstract class MaterialDTO
    {
        [Required]
        public string Name { get; set; }
        public long InventoryNumber { get; set; }
        public double UnitPrice { get; set; }
    }
}
