using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ScreenMediaTT.Data.Models
{
    public class RoomAvailability
    {
        [Key, ForeignKey("Room")]
        public int RoomID { get; set; }
        public Room Room { get; set; }

        [Key]
        public DateTime Date { get; set; }
    }
}
