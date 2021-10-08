
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ScreenMediaTT.Data.Models
{
    public class Booking
    {
        [Key]
        public int BookingID { get; set; }
        public ICollection<RoomBooking> RoomBookings { get; set; }

        [ForeignKey("PersonalDetails")]
        public int PersonalDetailsID { get; set; }
        public PersonalDetails PersonalDetails { get; set; }

        public int NumberOfGuests { get; set; }

        public DateTime FromDate { get; set; }

        public DateTime ToDate { get; set; }
    }
}
