using HotelPlatform.DAL.Models.BaseModel;

namespace HotelPlatform.DAL.Models.SystemModels
{
    public class Admin : AppUser
    {
        public ICollection<Hotel> ManagedHotels { get; set; } = new HashSet<Hotel>();
    }

}
