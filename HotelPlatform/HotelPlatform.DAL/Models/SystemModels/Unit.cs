using HotelPlatform.DAL.Models.BaseModel;
using HotelPlatform.Shared.Enums;

namespace HotelPlatform.DAL.Models.SystemModels
{
    public class Unit:BaseEntity
    {
        public int RoomNumber { get; set; }
        public int FloorNumber { get; set; }
        public double BasePrice { get; set; }
        public UnitType Type { get; set; }
        public string Status { get; set; }
        public string Description { get; set; }
        public int MaxOccupancy { get; set; }
        public string HotelId { get; set; }
        public Hotel Hotel { get; set; }
        public ICollection<Booking> Bookings { get; set; } = new HashSet<Booking>();
        public ICollection<UnitPhoto> UnitPhotos { get; set; } = new HashSet<UnitPhoto>();
    }

}
