using ScreenMediaTT.Core.Models;
using ScreenMediaTT.Data.Models;
using System.Collections.Generic;

namespace ScreenMediaTT.Core.Interfaces
{
    public interface IAvailabilityService
    {
        /// <summary>
        /// Stores a booking if room(s) has availability. 
        /// </summary>
        /// <param name="booking"></param>
        /// <returns></returns>
        IEnumerable<Room> FindAvailableRooms(AvailabilitySearchCriteria availabilitySearchCriteria);
    }
}
