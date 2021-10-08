using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ScreenMediaTT.Data.Models
{
    public class Room
    {
        [Key]
        public int RoomID { get; set; }
        public string Number { get; set; }

        [ForeignKey("Hotel")]
        public int HotelID { get; set; }
        public Hotel Hotel { get; set; }

        [ForeignKey("RoomType")]
        public int RoomTypeID { get; set; }
        public RoomType RoomType { get; set; }

    }
}
