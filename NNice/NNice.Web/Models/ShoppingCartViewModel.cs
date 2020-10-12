using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NNice.Web.Models
{
    public class ShoppingCartViewModel
    {
        public string CartId { get; set; }
        public int? OrderID { get; set; }
        public int? Count { get; set; }

        public int? ProductID { get; set; }
        public string Name { get; set; }
        public double UnitPrice { get; set; }
    }
}
