using ScreenMediaTT.Data.Models;
using ScreenMediaTT.Data.Repositories;
using System;
using System.Collections.Generic;

namespace ScreenMediaTT.Data.Interfaces
{
    public interface IRoomRepository : IRepository<Room>
    {
        IEnumerable<Room> GetAvailableRooms(DateTime fromDate, DateTime toDate, int? hotelID = null);
    }
}
