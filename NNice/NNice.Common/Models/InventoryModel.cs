using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace NNice.Common.Models
{
    public partial class InventoryModel : BaseEntity
    {
        [Required]
        public DateTime CreatedDate { get; set; }
        public double TotalAmount { get; set; }
        public Type Type { get; set; }
    }

    public enum Type
    {
        Import = 1,
        Export = 2
    }
}
