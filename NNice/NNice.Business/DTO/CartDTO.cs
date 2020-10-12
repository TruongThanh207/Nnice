using System;
using System.Collections.Generic;
using System.Text;

namespace NNice.Business.DTO
{
    public class CartDTO
    {
        public string CartId { get; set; }
        public int ProductID { get; set; }
        public int Count { get; set; }
        public System.DateTime DateCreated { get; set; }

    }
}
