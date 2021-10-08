using System.ComponentModel.DataAnnotations;

namespace ScreenMediaTT.Data.Models
{
    public class RoomType
    {
        [Key]
        public int RoomTypeID { get; set; }
        public string Name { get; set; }
        public int Capacity { get; set; }
    }
}
