using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ScreenMediaTT.Core.Services;
using ScreenMediaTT.Data;
using ScreenMediaTT.Data.Models;
using ScreenMediaTT.Data.Repositories;
using System.Linq;

namespace ScreenMediaTT.Tests.Services
{
    [TestClass]
    public class HotelServiceTests
    {
        private IConfigurationRoot _configuration;
        private DbContextOptions<ScreenMediaTtContext> _options;

        public HotelServiceTests()
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
        public void HotelService_FindHotel_SuccessfullyReturnsAHotel()
        {
            using (var dbContext = new ScreenMediaTtContext(_options))
            {
                dbContext.Database.EnsureDeleted();
                dbContext.Database.EnsureCreated();

                var service = new HotelService(new HotelRepository(dbContext));

                // seed hotels
                var hotel = new Hotel
                {
                    HotelID = 1,
                    Name = "Hotel Test"
                };

                dbContext.Hotels.Add(hotel);

                dbContext.SaveChangesAsync();

                var hotelResults = service.FindHotel("tel");

                var result = hotelResults is null || !hotelResults.Any();

                Assert.IsFalse(result);
            }
        }

        [TestMethod]
        public void HotelService_FindHotel_UnsuccessfullyReturnsAHotel()
        {
            using (var dbContext = new ScreenMediaTtContext(_options))
            {
                dbContext.Database.EnsureDeleted();
                dbContext.Database.EnsureCreated();

                var service = new HotelService(new HotelRepository(dbContext));

                // seed hotels
                var hotel = new Hotel
                {
                    HotelID = 1,
                    Name = "Hotel Test"
                };

                dbContext.Hotels.Add(hotel);

                dbContext.SaveChangesAsync();

                var hotelResults = service.FindHotel("not here");

                var result = hotelResults is null || !hotelResults.Any();

                Assert.IsTrue(result);
            }

        }
    }
}
