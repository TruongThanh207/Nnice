using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace NNice.Web.Models
{
    public class BookingServiceViewModel
    {
        public int ID { get; set; }
        public string RoomName { get; set; }
        public int RoomID { get; set; }
        public int UserID { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MMM/yyyy}")]
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public bool BookingParty { get; set; }
        public int? PartyID { get; set; }
        public string PartyName { get; set; }
        public string CreatedBy { get; set; }
        public double TotalAmount { get; set; }
        public IEnumerable<ShoppingCartViewModel> ShoppingCarts{ get; set; }

        public IEnumerable<SelectListItem> RoomItems { get; set; } 
    }
    public class SelectBox
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
