using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ScreenMediaTT.Core.Interfaces;
using ScreenMediaTT.Data.Models;
using System.Collections.Generic;

namespace ScreenMediaTT.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HotelController : ControllerBase
    {

        private readonly ILogger<HotelController> _logger;
        private readonly IHotelService _hotelService;

        public HotelController(ILogger<HotelController> logger, IHotelService hotelService)
        {
            _logger = logger;
            _hotelService = hotelService;
        }

        [HttpGet]
        public IEnumerable<Hotel> Get(string hotelName)
        {
            return _hotelService.FindHotel(hotelName);
        }
    }
}
