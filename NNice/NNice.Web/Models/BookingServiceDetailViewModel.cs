using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NNice.Web.Models
{
    public class BookingServiceDetailViewModel
    {
        public int ID { get; set; }
        public string RoomName { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public bool BookingParty { get; set; }
        public string PartyName { get; set; }
        public string CreatedBy { get; set; }
        public IEnumerable<OrderDetailDTO> OrderDetails { get; set; }

    }
    public class OrderDetailDTO
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public double UnitPrice { get; set; }
        public double TotalPrice { get; set; }
    }
}
