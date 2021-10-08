using ScreenMediaTT.Core.Interfaces;
using ScreenMediaTT.Core.Models;
using ScreenMediaTT.Data.Interfaces;
using ScreenMediaTT.Data.Models;
using System;
using System.Linq;

namespace ScreenMediaTT.Core.Services
{
    public class BookingService : IBookingService
    {
        private readonly IBookingRepository _bookingRepo;
        private readonly IRoomAvailabilityRepository _roomAvailabilityRepository;
        private readonly IRoomRepository _roomRepository;

        public BookingService(IBookingRepository bookingRepo, IRoomAvailabilityRepository roomAvailabilityRepository, IRoomRepository roomRepository)
        {
            _bookingRepo = bookingRepo;
            _roomAvailabilityRepository = roomAvailabilityRepository;
            _roomRepository = roomRepository;
        }

        /// <summary>
        /// Stores a booking if room(s) has availability. 
        /// </summary>
        /// <param name="booking"></param>
        /// <returns></returns>
        public CreateBookingResult CreateBooking(Booking booking)
        {
            // validate rooms
            foreach(var roomBooking in booking.RoomBookings)
            {
                var room = _roomRepository.GetAll().FirstOrDefault(x => x.RoomID == roomBooking.RoomID);

                if (room.RoomType.Capacity < roomBooking.People)
                {
                    return new CreateBookingResult { Success = false };
                }
            }

            // validate availability
            foreach(var roomBooking in booking.RoomBookings)
            {
                if (!_roomAvailabilityRepository.RoomHasAvailability(roomBooking.RoomID, booking.FromDate, booking.ToDate))
                {
                    Console.WriteLine("Has No availability");
                    return new CreateBookingResult { Success = false };
                }

                if (!BookRoomAvailability(roomBooking.RoomID, booking.FromDate, booking.ToDate))
                {
                    Console.WriteLine("Failed to book availability");
                    return new CreateBookingResult { Success = false };
                }
            }

            var bookingResult = _bookingRepo.AddAsync(booking).Result;

            // TODO extract this out into it a more suitable reference no.
            var bookingRef = $"REF-{bookingResult.BookingID}";

            return new CreateBookingResult { Success = true, BookingReference = bookingRef }; ;
        }

        /// <summary>
        /// Removes room availabiltiy over a given date range
        /// </summary>
        /// <param name="roomID"></param>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <returns></returns>
        private bool BookRoomAvailability(int roomID, DateTime fromDate, DateTime toDate)
        {
            var roomAvailability = _roomAvailabilityRepository.GetAll().Where(x => x.RoomID == roomID && x.Date >= fromDate && x.Date <= toDate);

            if(roomAvailability == null || !roomAvailability.Any())
            {
                return false;
            }

            _roomAvailabilityRepository.RemoveRange(roomAvailability);

            return true;
        }


        /// <summary>
        /// Finds a previously stored booking.
        /// </summary>
        /// <param name="bookingReference"></param>
        /// <returns></returns>
        public Booking FindBooking(string bookingReference)
        {
            Console.WriteLine(bookingReference);
            // TODO validation move out
            if (string.IsNullOrEmpty(bookingReference) || bookingReference.Length <= 4)
            {
                return null;
            }

            int.TryParse(bookingReference.Substring(4, bookingReference.Length - 4), out int bookingID);

            Console.WriteLine(bookingID);
            // Couldn't parse the reference number
            if (bookingID == default(int))
            {
                return null;
            }

            return _bookingRepo.GetAll().FirstOrDefault(x => x.BookingID == bookingID);
        }
    }
}
