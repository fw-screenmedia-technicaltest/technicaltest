
using System.ComponentModel.DataAnnotations;

namespace ScreenMediaTT.Data.Models
{
    public class Hotel
    {
        [Key]
        public int HotelID { get; set; }
        public string Name { get; set; }
    }
}
