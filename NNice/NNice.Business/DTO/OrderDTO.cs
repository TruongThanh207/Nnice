using System;
using System.Collections;
using System.Collections.Generic;

namespace NNice.Business.DTO
{
    public class OrderDTO
    {
        public int ID { get; set; }
        public string RoomName { get; set; }
        public int RoomID { get; set; }
        public string CreatedBy { get; set; }
        public int UserID { get; set; }
        public double TotalAmount { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        public bool BookingParty { get; set; }
        public string PartyName { get; set; }
        public int? PartyID { get; set; }

        public IEnumerable<OrderDetailDTO> OrderDetails { get; set; }
    }
}
