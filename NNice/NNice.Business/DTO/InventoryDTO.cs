using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace NNice.Business.DTO
{
    public class InventoryDTO : MaterialDTO
    {
        [Required]
        public DateTime CreatedDate { get; set; }
        public Type Type { get; set; }
    }
    public enum Type
    {
        Import = 1,
        Export = 2
    }
}
