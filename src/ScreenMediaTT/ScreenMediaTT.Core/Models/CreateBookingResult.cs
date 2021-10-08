using System;
using System.Collections.Generic;
using System.Text;

namespace ScreenMediaTT.Core.Models
{
    public class CreateBookingResult
    {
        public bool Success { get; internal set; }
        public string BookingReference { get; internal set; }
    }
}
