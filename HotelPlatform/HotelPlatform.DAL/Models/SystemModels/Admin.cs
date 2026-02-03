using HotelPlatform.DAL.Models.BaseModel;

namespace HotelPlatform.DAL.Models.SystemModels
{
    public class Admin : BaseEntity
    {
        public string UserId { get; set; }
        public AppUser User { get; set; }
        public ICollection<Hotel> ManagedHotels { get; set; } = new HashSet<Hotel>();
    }

}
