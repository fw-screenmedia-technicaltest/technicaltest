using ScreenMediaTT.Data.Models;
using ScreenMediaTT.Data.Repositories;
using System;
using System.Collections.Generic;

namespace ScreenMediaTT.Data.Interfaces
{
    public interface IRoomAvailabilityRepository : IRepository<RoomAvailability>
    {
        void RemoveRange(IEnumerable<RoomAvailability> roomAvailability);
        bool RoomHasAvailability(int roomID, DateTime fromDate, DateTime toDate);
    }
}
