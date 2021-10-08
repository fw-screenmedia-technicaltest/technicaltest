using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ScreenMediaTT.Core.Interfaces;
using ScreenMediaTT.Core.Models;
using ScreenMediaTT.Data.Models;
using System.Collections.Generic;

namespace ScreenMediaTT.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AvailabilityController : ControllerBase
    {
        private readonly ILogger<HotelController> _logger;
        private readonly IAvailabilityService _availabilityService;

        public AvailabilityController(ILogger<HotelController> logger, IAvailabilityService availabilityService)
        {
            _logger = logger;
            _availabilityService = availabilityService;
        }

        [HttpGet]
        public IEnumerable<Room> Get([FromQuery] AvailabilitySearchCriteria searchCriteria)
        {
            return _availabilityService.FindAvailableRooms(searchCriteria);
        }
    }
}
