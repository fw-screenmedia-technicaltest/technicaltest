using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ScreenMediaTT.Core.Services;
using ScreenMediaTT.Data;
using ScreenMediaTT.Data.Models;
using ScreenMediaTT.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ScreenMediaTT.Tests.Services
{
    [TestClass]
    public class BookingServiceTests
    {
        private IConfigurationRoot _configuration;
        private DbContextOptions<ScreenMediaTtContext> _options;

        public BookingServiceTests()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath("D:\\Personal\\technicaltest\\src\\ScreenMediaTT\\ScreenMediaTT.Tests\\")
                .AddJsonFile("appsettings.json");

            _configuration = builder.Build();
            _options = new DbContextOptionsBuilder<ScreenMediaTtContext>()
                .UseSqlite(_configuration.GetConnectionString("DefaultConnection"))
                .Options;
        }

        [TestMethod]
        public void BookingService_CreateBooking_SuccessfullyBooksARoom()
        {
            using (var dbContext = new ScreenMediaTtContext(_options))
            {
                dbContext.Database.EnsureDeleted();
                dbContext.Database.EnsureCreated();

                // seed hotel
                var hotel = new Hotel
                {
                    HotelID = 1,
                    Name = "Hotel Test"
                };

                dbContext.Hotels.Add(hotel);

                dbContext.SaveChangesAsync();

                // seed room types
                var roomTypeSingle = new RoomType { RoomTypeID = 1, Name = "Single", Capacity = 1 };

                var roomTypes = new List<RoomType>
                {
                    roomTypeSingle,
                };

                dbContext.RoomTypes.AddRange(roomTypes);

                // seed rooms

                var rooms = new List<Room>
                {
                    // SINGLES
                    new Room{ RoomID = 1, Hotel = hotel, Number = "101", RoomTypeID = 1 },
                };

                dbContext.Rooms.AddRange(rooms);

                // seed room availability

                var today = DateTime.Now.Date;
                var tomorrow = DateTime.Now.Date.AddDays(1);

                var roomAvailability = new List<RoomAvailability> {
                    new RoomAvailability { Date = today, RoomID = 1 },
                    new RoomAvailability { Date = tomorrow, RoomID = 1 },
                };

                dbContext.RoomAvailability.AddRange(roomAvailability);

                dbContext.SaveChangesAsync();

                var service = new BookingService(new BookingRepository(dbContext), new RoomAvailabilityRepository(dbContext), new RoomRepository(dbContext));

                var booking = new Booking
                {
                    FromDate = today,
                    ToDate = tomorrow,
                    NumberOfGuests = 1,
                    PersonalDetails = new PersonalDetails { EmailAddress = "test@test.com", FirstName = "Test", LastName = "Test", PhoneNumber = "07000000000" },
                    RoomBookings = new List<RoomBooking>
                    {
                        new RoomBooking { People = 1, RoomID = 1}
                    }
                };

                var bookingResult = service.CreateBooking(booking);

                Assert.IsTrue(bookingResult.Success);
            }
        }

        [TestMethod]
        public void BookingService_CreateBooking_CannotBookARoomWithMorePeopleThanItsCapactity()
        {
            using (var dbContext = new ScreenMediaTtContext(_options))
            {
                dbContext.Database.EnsureDeleted();
                dbContext.Database.EnsureCreated();

                // seed hotel
                var hotel = new Hotel
                {
                    HotelID = 1,
                    Name = "Hotel Test"
                };

                dbContext.Hotels.Add(hotel);

                dbContext.SaveChangesAsync();

                // seed room types
                var roomTypeSingle = new RoomType { RoomTypeID = 1, Name = "Single", Capacity = 1 };

                var roomTypes = new List<RoomType>
                {
                    roomTypeSingle,
                };

                dbContext.RoomTypes.AddRange(roomTypes);

                // seed rooms

                var rooms = new List<Room>
                {
                    // SINGLES
                    new Room{ RoomID = 1, Hotel = hotel, Number = "101", RoomTypeID = 1 },
                };

                dbContext.Rooms.AddRange(rooms);

                // seed room availability

                var today = DateTime.Now.Date;
                var tomorrow = DateTime.Now.Date.AddDays(1);

                var roomAvailability = new List<RoomAvailability> {
                    new RoomAvailability { Date = today, RoomID = 1 },
                    new RoomAvailability { Date = tomorrow, RoomID = 1 },
                };

                dbContext.RoomAvailability.AddRange(roomAvailability);

                dbContext.SaveChangesAsync();

                var service = new BookingService(new BookingRepository(dbContext), new RoomAvailabilityRepository(dbContext), new RoomRepository(dbContext));

                var booking = new Booking
                {
                    FromDate = today,
                    ToDate = tomorrow,
                    NumberOfGuests = 1,
                    PersonalDetails = new PersonalDetails { EmailAddress = "test@test.com", FirstName = "Test", LastName = "Test", PhoneNumber = "07000000000" },
                    RoomBookings = new List<RoomBooking>
                    {
                        new RoomBooking { People = 2, RoomID = 1}
                    }
                };

                var bookingResult = service.CreateBooking(booking);

                Assert.IsFalse(bookingResult.Success);
            }
        }

        [TestMethod]
        public void BookingService_CreateBooking_CannotBookARoomThatsAlreadyBeenBooked()
        {
            using (var dbContext = new ScreenMediaTtContext(_options))
            {
                dbContext.Database.EnsureDeleted();
                dbContext.Database.EnsureCreated();

                // seed hotel
                var hotel = new Hotel
                {
                    HotelID = 1,
                    Name = "Hotel Test"
                };

                dbContext.Hotels.Add(hotel);

                dbContext.SaveChangesAsync();

                // seed room types
                var roomTypeSingle = new RoomType { RoomTypeID = 1, Name = "Single", Capacity = 1 };

                var roomTypes = new List<RoomType>
                {
                    roomTypeSingle,
                };

                dbContext.RoomTypes.AddRange(roomTypes);

                // seed rooms

                var rooms = new List<Room>
                {
                    // SINGLES
                    new Room{ RoomID = 1, Hotel = hotel, Number = "101", RoomTypeID = 1 },
                };

                dbContext.Rooms.AddRange(rooms);

                // seed room availability

                var today = DateTime.Now.Date;
                var tomorrow = DateTime.Now.Date.AddDays(1);

                var roomAvailability = new List<RoomAvailability> {
                    new RoomAvailability { Date = today, RoomID = 1 },
                    new RoomAvailability { Date = tomorrow, RoomID = 1 },
                };

                dbContext.RoomAvailability.AddRange(roomAvailability);

                dbContext.SaveChangesAsync();

                var service = new BookingService(new BookingRepository(dbContext), new RoomAvailabilityRepository(dbContext), new RoomRepository(dbContext));

                var booking = new Booking
                {
                    FromDate = today,
                    ToDate = tomorrow,
                    NumberOfGuests = 1,
                    PersonalDetails = new PersonalDetails { EmailAddress = "test@test.com", FirstName = "Test", LastName = "Test", PhoneNumber = "07000000000" },
                    RoomBookings = new List<RoomBooking>
                    {
                        new RoomBooking { People = 1, RoomID = 1}
                    }
                };

                var firstBookingResult = service.CreateBooking(booking);

                if (!firstBookingResult.Success)
                {
                    Assert.Fail();
                }

                var secondBookingResult = service.CreateBooking(booking);

                Assert.IsFalse(secondBookingResult.Success);
            }
        }

        [TestMethod]
        public void BookingService_FindBookingDetails_SuccessfullyReturnsBookingDetails()
        {
            using (var dbContext = new ScreenMediaTtContext(_options))
            {
                dbContext.Database.EnsureDeleted();
                dbContext.Database.EnsureCreated();

                // seed hotel
                var hotel = new Hotel
                {
                    HotelID = 1,
                    Name = "Hotel Test"
                };

                dbContext.Hotels.Add(hotel);

                dbContext.SaveChangesAsync();

                // seed room types
                var roomTypeSingle = new RoomType { RoomTypeID = 1, Name = "Single", Capacity = 1 };

                var roomTypes = new List<RoomType>
                {
                    roomTypeSingle,
                };

                dbContext.RoomTypes.AddRange(roomTypes);

                // seed rooms

                var rooms = new List<Room>
                {
                    // SINGLES
                    new Room{ RoomID = 1, Hotel = hotel, Number = "101", RoomTypeID = 1 },
                };

                dbContext.Rooms.AddRange(rooms);

                // seed room availability

                var today = DateTime.Now.Date;
                var tomorrow = DateTime.Now.Date.AddDays(1);

                var roomAvailability = new List<RoomAvailability> {
                    new RoomAvailability { Date = today, RoomID = 1 },
                    new RoomAvailability { Date = tomorrow, RoomID = 1 },
                };

                dbContext.RoomAvailability.AddRange(roomAvailability);

                dbContext.SaveChangesAsync();

                var service = new BookingService(new BookingRepository(dbContext), new RoomAvailabilityRepository(dbContext), new RoomRepository(dbContext));

                var booking = new Booking
                {
                    FromDate = today,
                    ToDate = tomorrow,
                    NumberOfGuests = 1,
                    PersonalDetails = new PersonalDetails { EmailAddress = "test@test.com", FirstName = "Test", LastName = "Test", PhoneNumber = "07000000000" },
                    RoomBookings = new List<RoomBooking>
                    {
                        new RoomBooking { People = 1, RoomID = 1}
                    }
                };

                var bookingResult = service.CreateBooking(booking);

                if (!bookingResult.Success)
                {
                    Assert.Fail();
                }

                var bookingDetails = service.FindBooking(bookingResult.BookingReference);

                Assert.IsNotNull(bookingDetails);
            }
        }
    }
}
