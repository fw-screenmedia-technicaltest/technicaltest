using System.ComponentModel.DataAnnotations;

namespace ScreenMediaTT.Data.Models
{
    public class PersonalDetails
    {
        [Key]
        public int PersonalDetailsID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }
    }
}
