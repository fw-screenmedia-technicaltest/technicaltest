using ScreenMediaTT.Core.Interfaces;
using ScreenMediaTT.Data.Interfaces;
using ScreenMediaTT.Data.Models;
using System.Collections.Generic;
using System.Linq;

namespace ScreenMediaTT.Core.Services
{
    public class HotelService : IHotelService
    {
        private readonly IHotelRepository _hotelRepository;

        public HotelService(IHotelRepository hotelRepository)
        {
            _hotelRepository = hotelRepository;
        }

        /// <summary>
        /// Find hotels using hotelName parameter. Searches for the term within any stored hotel name.
        /// </summary>
        /// <param name="hotelName"></param>
        /// <returns></returns>
        public IEnumerable<Hotel> FindHotel(string hotelName)
        {
            hotelName = hotelName.ToLower();
            return _hotelRepository.GetAll().Where(x => x.Name.ToLower().Contains(hotelName));
        }
    }
}
