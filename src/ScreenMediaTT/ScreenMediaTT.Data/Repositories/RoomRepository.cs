using Microsoft.EntityFrameworkCore;
using ScreenMediaTT.Data.Interfaces;
using ScreenMediaTT.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ScreenMediaTT.Data.Repositories
{
    public class RoomRepository : Repository<Room>, IRoomRepository
    {
        public RoomRepository(ScreenMediaTtContext dbContext) : base(dbContext)
        {
            
        }

        public IEnumerable<Room> GetAvailableRooms(DateTime fromDate, DateTime toDate, int? hotelID)
        {
            // TODO make this more efficent limited with SQL LITE
            var sql = $"SELECT * FROM Rooms r WHERE RoomID IN (SELECT RoomID FROM RoomAvailability WHERE RoomID = r.RoomID AND Date >= '{fromDate.ToString("yyyy-MM-dd")}' AND Date <= '{toDate.ToString("yyyy-MM-dd")}' {(hotelID.HasValue? $" AND HotelID = {hotelID.Value}": null)} GROUP BY RoomID HAVING COUNT(RoomID) >= {(toDate - fromDate).TotalDays})";

            var results = _dbContext.Rooms.FromSqlRaw(sql)?.Include(x => x.RoomType).ToList();

            return results;
        }
    }
}
