using ScreenMediaTT.Core.Interfaces;
using ScreenMediaTT.Core.Models;
using ScreenMediaTT.Data.Interfaces;
using ScreenMediaTT.Data.Models;
using System.Collections.Generic;
using System.Linq;

namespace ScreenMediaTT.Core.Services
{
    public class AvailabilityService : IAvailabilityService
    {
        private readonly IRoomRepository _roomRepo;

        public AvailabilityService(IRoomRepository roomRepo)
        {
            _roomRepo = roomRepo;
        }

        /// <summary>
        /// Find available rooms from the AvailabilitySearchCriteria. Will return a list of available rooms.
        /// </summary>
        /// <param name="availabilitySearchCriteria"></param>
        /// <returns></returns>
        public IEnumerable<Room> FindAvailableRooms(AvailabilitySearchCriteria availabilitySearchCriteria)
        {
            var rooms = _roomRepo.GetAvailableRooms(availabilitySearchCriteria.FromDate, availabilitySearchCriteria.ToDate, availabilitySearchCriteria.HotelID);

            if(rooms is null || !rooms.Any())
            {
                // TODO create a return object rather than passing null back
                return new Room[] { };
            }

            // TODO Should use a while loop here to be more efficient
            var totalAvailableCapactity = rooms.Sum(x => x.RoomType.Capacity);

            if(availabilitySearchCriteria.NoOfPeople > totalAvailableCapactity)
            {
                return new Room[] { };
            }
            
            return rooms;
        }
    }
}
