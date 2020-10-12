using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NNice.Common.Models
{
    [Table("Parties")]
    public partial class PartyModel : BaseEntity
    {
        [MaxLength(30)]
        public string Name { get; set; }
        public int RoomID { get; set; }
        public double Deposit { get; set; }
    }
}
