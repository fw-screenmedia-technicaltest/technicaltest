using Microsoft.EntityFrameworkCore;
using ScreenMediaTT.Data.Interfaces;
using ScreenMediaTT.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ScreenMediaTT.Data.Repositories
{
    public class RoomAvailabilityRepository : Repository<RoomAvailability>, IRoomAvailabilityRepository
    {
        public RoomAvailabilityRepository(ScreenMediaTtContext dbContext) : base(dbContext)
        {
            
        }

        public void RemoveRange(IEnumerable<RoomAvailability> roomAvailability)
        {
            _dbContext.RemoveRange(roomAvailability);

            _dbContext.SaveChanges();
        }

        public bool RoomHasAvailability(int roomID, DateTime fromDate, DateTime toDate)
        {
            // TODO Make more efficient
            var rooms = _dbContext.Rooms.FromSqlRaw($"SELECT * FROM Rooms r WHERE RoomID IN (SELECT RoomID FROM RoomAvailability WHERE RoomID = {roomID} AND Date >= '{fromDate.ToString("yyyy-MM-dd")}' AND Date <= '{toDate.ToString("yyyy-MM-dd")}' GROUP BY RoomID HAVING COUNT(RoomID) >= {(fromDate - toDate).TotalDays})")?.ToList();

            // couldn't find the room so must not have availability
            if (rooms == null || !rooms.Any())
            {
                return false;
            }

            return true;
        }
    }
}
