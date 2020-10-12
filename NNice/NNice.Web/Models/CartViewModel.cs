using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NNice.Web.Models
{
    public class CartViewModel
    {
        public string CartId { get; set; }
        public int? ProductID { get; set; }
        public int Count { get; set; }
    }
}
