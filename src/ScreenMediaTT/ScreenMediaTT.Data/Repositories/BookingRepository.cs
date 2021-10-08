using ScreenMediaTT.Data.Interfaces;
using ScreenMediaTT.Data.Models;

namespace ScreenMediaTT.Data.Repositories
{
    public class BookingRepository : Repository<Booking>, IBookingRepository
    {
        public BookingRepository(ScreenMediaTtContext dbContext) : base(dbContext)
        {

        }
    }
}
