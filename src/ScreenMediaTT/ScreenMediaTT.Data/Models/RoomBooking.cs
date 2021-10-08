using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ScreenMediaTT.Data.Models
{
    public class RoomBooking
    {
        [Key]
        public int RoomBookingId { get; set; }

        [ForeignKey("Room")]
        public int RoomID { get; set; }
        public Room Room { get; set; }

        [ForeignKey("Booking")]
        public int BookingID { get; set; }
        public Booking Booking { get; set; }
        public int People { get; set; }
    }
}
