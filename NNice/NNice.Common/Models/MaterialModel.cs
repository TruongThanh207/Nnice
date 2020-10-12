using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace NNice.Common.Models
{
    public partial class MaterialModel : BaseEntity
    {
        [Required]
        [MaxLength(30)]
        public string Name { get; set; }
        public long InventoryNumber { get; set; }
        public double UnitPrice { get; set; }
    }
}
