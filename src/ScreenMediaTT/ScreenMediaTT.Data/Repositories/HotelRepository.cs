using ScreenMediaTT.Data.Interfaces;
using ScreenMediaTT.Data.Models;

namespace ScreenMediaTT.Data.Repositories
{
    public class HotelRepository : Repository<Hotel>, IHotelRepository
    {
        public HotelRepository(ScreenMediaTtContext dbContext) : base(dbContext)
        {

        }
    }
}
