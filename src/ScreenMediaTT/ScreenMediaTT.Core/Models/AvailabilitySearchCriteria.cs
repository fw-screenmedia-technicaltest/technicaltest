
using System;

namespace ScreenMediaTT.Core.Models
{
    public class AvailabilitySearchCriteria
    {
        public int NoOfPeople { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public int? HotelID { get; set; }
    }
}
