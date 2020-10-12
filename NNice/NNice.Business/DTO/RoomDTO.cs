using System.ComponentModel.DataAnnotations;

namespace NNice.Business.DTO
{
    public class RoomDTO
    {
        public int? ID { get; set; }
        [Required]
        public string Name { get; set; }
        public bool IsAvailable { get; set; }
        public int Capacity { get; set; }
    }
}
