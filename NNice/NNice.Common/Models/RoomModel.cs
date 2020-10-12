using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NNice.Common.Models
{
    [Table("Rooms")]
    public partial class RoomModel: BaseEntity
    {
        [Required]
        [MaxLength(30)]
        public string Name { get; set; }
        public bool IsAvailable { get; set; }
        public int Capacity { get; set; }
    }
}
