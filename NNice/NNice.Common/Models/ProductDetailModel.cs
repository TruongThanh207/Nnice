using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace NNice.Common.Models
{
    public partial class ProductDetailModel
    {
        public int ProductID { get; set; }
        public int MaterialID { get; set; }
    }
}
