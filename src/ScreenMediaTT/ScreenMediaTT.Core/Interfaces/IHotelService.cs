using ScreenMediaTT.Data.Models;
using System.Collections.Generic;

namespace ScreenMediaTT.Core.Interfaces
{
    public interface IHotelService
    {
        /// <summary>
        /// Find hotels using hotelName parameter. Searches for the term within any stored hotel name.
        /// </summary>
        /// <param name="hotelName"></param>
        /// <returns></returns>
        IEnumerable<Hotel> FindHotel(string hotelName);
    }
}
