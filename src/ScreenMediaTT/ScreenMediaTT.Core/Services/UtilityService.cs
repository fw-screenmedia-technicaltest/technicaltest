using Microsoft.Extensions.Logging;
using ScreenMediaTT.Core.Interfaces;
using ScreenMediaTT.Data;
using ScreenMediaTT.Data.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ScreenMediaTT.Core.Services
{
    public class UtilityService : IUtilityService
    {
        private readonly ILogger<UtilityService> _logger;
        private readonly ScreenMediaTtContext _dbContext;

        public UtilityService(ScreenMediaTtContext dbContext, ILogger<UtilityService> logger)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        /// <summary>
        /// Used to reset the current data for bookings
        /// </summary>
        /// <returns></returns>
        public bool Reset()
        {
            try
            {
                _dbContext.Bookings.RemoveRange(_dbContext.Bookings);
                _dbContext.PersonalDetails.RemoveRange(_dbContext.PersonalDetails);
                _dbContext.RoomAvailability.RemoveRange(_dbContext.RoomAvailability);
                AddAvailability(_dbContext.Rooms);
                _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return false;
            }

            return true;
        }

        public async Task<bool> SeedAsync()
        {
            try
            {
                // clear current data
                _dbContext.Database.EnsureDeleted();
                _dbContext.Database.EnsureCreated();

                // seed hotels
                var hotel = new Hotel
                {
                    HotelID = 1,
                    Name = "Hotel California"
                };

                hotel = _dbContext.Hotels.Add(hotel).Entity;

                await _dbContext.SaveChangesAsync();


                // seed room types
                var roomTypeSingle = new RoomType { RoomTypeID = 1, Name = "Single", Capacity = 1 };
                var roomTypeDouble = new RoomType { RoomTypeID = 2, Name = "Double", Capacity = 2 };
                var roomTypeDeluxe = new RoomType { RoomTypeID = 3, Name = "Deluxe", Capacity = 2 };

                var roomTypes = new List<RoomType>
                {
                    roomTypeSingle,
                    roomTypeDouble,
                    roomTypeDeluxe
                };

                _dbContext.RoomTypes.AddRange(roomTypes);

                // seed rooms

                var rooms = new List<Room>
                {
                    // SINGLES
                    new Room{ RoomID = 1, Hotel = hotel, Number = "101", RoomTypeID = 1 },
                    new Room{ RoomID = 2, Hotel = hotel, Number = "102", RoomTypeID = 1 },
                    new Room{ RoomID = 3, Hotel = hotel, Number = "103", RoomTypeID = 1 },
                    new Room{ RoomID = 4, Hotel = hotel, Number = "104", RoomTypeID = 1 },
                    new Room{ RoomID = 5, Hotel = hotel, Number = "105", RoomTypeID = 1 },
                    new Room{ RoomID = 6, Hotel = hotel, Number = "106", RoomTypeID = 1 },
                    // DOUBLES
                    new Room{ RoomID = 7, Hotel = hotel, Number = "201", RoomTypeID = 2 },
                    new Room{ RoomID = 8, Hotel = hotel, Number = "202", RoomTypeID = 2 },
                    new Room{ RoomID = 9, Hotel = hotel, Number = "203", RoomTypeID = 2 },
                    new Room{ RoomID = 10, Hotel = hotel, Number = "204", RoomTypeID = 2 },
                    new Room{ RoomID = 11, Hotel = hotel, Number = "205", RoomTypeID = 2 },
                    new Room{ RoomID = 12, Hotel = hotel, Number = "206", RoomTypeID = 2 },
                    // DELUXE
                    new Room{ RoomID = 13, Hotel = hotel, Number = "301", RoomTypeID = 3 },
                    new Room{ RoomID = 14, Hotel = hotel, Number = "302", RoomTypeID = 3 },
                    new Room{ RoomID = 15, Hotel = hotel, Number = "303", RoomTypeID = 3 },
                    new Room{ RoomID = 16, Hotel = hotel, Number = "304", RoomTypeID = 3 },
                    new Room{ RoomID = 17, Hotel = hotel, Number = "305", RoomTypeID = 3 },
                    new Room{ RoomID = 18, Hotel = hotel, Number = "306", RoomTypeID = 3 },
                };

                _dbContext.Rooms.AddRange(rooms);

                // seed room availability

                AddAvailability(rooms);

                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return false;
            }

            return true;
        }

        private void AddAvailability(IEnumerable<Room> rooms)
        {
            var availability = new List<RoomAvailability>();
            var today = DateTime.Now.Date;

            foreach (var room in rooms)
            {
                for (int i = 0; i < 200; i++)
                {
                    var availabilityDate = today.AddDays(i);
                    availability.Add(new RoomAvailability { Date = availabilityDate, RoomID = room.RoomID });
                }
            }

            _dbContext.RoomAvailability.AddRange(availability);
        }

    }
}
