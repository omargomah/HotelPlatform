using HotelPlatform.DAL.Models.BaseModel;
namespace HotelPlatform.DAL.Models.SystemModels
{
    public class Client:AppUser
    {
        public string UserId { get; set; }
        public string SSN { get; set; }
        public DateTime DOB { get; set; }
        public string Gender { get; set; }
        public string ProfileImageLink { get; set; }
        public AppUser User { get; set; }
        public ICollection<Booking> Bookings { get; set; } = new HashSet<Booking>();
        public ICollection<Review> Reviews { get; set; } = new HashSet<Review>();
    }

}
