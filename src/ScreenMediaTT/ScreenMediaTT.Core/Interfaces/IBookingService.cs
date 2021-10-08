using ScreenMediaTT.Core.Models;
using ScreenMediaTT.Data.Models;

namespace ScreenMediaTT.Core.Interfaces
{
    public interface IBookingService
    {
        /// <summary>
        /// Stores a booking if room(s) has availability. 
        /// </summary>
        /// <param name="booking"></param>
        /// <returns></returns>
        CreateBookingResult CreateBooking(Booking booking);

        /// <summary>
        /// Finds a previously stored booking.
        /// </summary>
        /// <param name="bookingReference"></param>
        /// <returns></returns>
        Booking FindBooking(string bookingReference);
    }
}
