using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ScreenMediaTT.Core.Interfaces;
using System.Threading.Tasks;

namespace ScreenMediaTT.Web.Controllers
{
    [ApiController]
    public class UtilityController : ControllerBase
    {

        private readonly ILogger<HotelController> _logger;
        private readonly IUtilityService _utilityService;

        public UtilityController(ILogger<HotelController> logger, IUtilityService utilityService)
        {
            _logger = logger;
            _utilityService = utilityService;
        }

        [HttpGet]
        [Route("Utility/seed")]
        public async Task<bool> Seed()
        {
            return await _utilityService.SeedAsync();
        }

        [HttpGet]
        [Route("Utility/reset")]
        public bool Reset()
        {
            return _utilityService.Reset();
        }
    }
}
