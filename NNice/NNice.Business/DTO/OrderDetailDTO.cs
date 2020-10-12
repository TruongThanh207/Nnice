using System;
using System.Collections.Generic;
using System.Text;

namespace NNice.Business.DTO
{
    public class OrderDetailDTO
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public double UnitPrice { get; set; }
        public double TotalPrice { get; set; }
    }
}
