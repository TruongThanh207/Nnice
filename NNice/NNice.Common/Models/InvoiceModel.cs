using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NNice.Common.Models
{
    [Table("Invoices")]
    public partial class InvoiceModel : BaseEntity
    {
        public int RoomID { get; set; }

        public int UserID { get; set; }
        public int? PartyID { get; set; }
        public bool BookingParty { get; set; }
        [Required]
        public DateTime StartTime { get; set; }
        [Required]
        public DateTime EndTime { get; set; }
        [Required]
        public DateTime CreatedDate { get; set; }
        public double TotalAmount { get; set; }
    }
}
