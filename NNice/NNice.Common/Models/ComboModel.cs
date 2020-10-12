using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace NNice.Common.Models
{
    [Table("Combos")]
    public partial class ComboModel : BaseEntity
    {
        [Required]
        [MaxLength(20)]
        public string Name { get; set; }
        public double UnitPrice { get; set; }
    }
}
