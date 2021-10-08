using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ScreenMediaTT.Core.Interfaces;
using ScreenMediaTT.Core.Models;
using ScreenMediaTT.Data.Models;

namespace ScreenMediaTT.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BookingController : ControllerBase
    {
        private readonly ILogger<HotelController> _logger;
        private readonly IBookingService _bookingService;

        public BookingController(ILogger<HotelController> logger, IBookingService bookingService)
        {
            _logger = logger;
            _bookingService = bookingService;
        }

        [HttpGet]
        public Booking Get(string referenceNo)
        {
            return _bookingService.FindBooking(referenceNo);
        }

        [HttpPost]
        public CreateBookingResult Post(Booking booking)
        {
            return _bookingService.CreateBooking(booking);
        }
    }
}
