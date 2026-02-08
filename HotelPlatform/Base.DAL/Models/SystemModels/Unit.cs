using Base.DAL.Models.BaseModels;
using Base.Shared.Enums;

namespace Base.DAL.Models.SystemModels
{
    public class Unit:BaseEntity
    {
        public int RoomNumber { get; set; }
        public int FloorNumber { get; set; }
        public double BasePrice { get; set; }
        public UnitType UnitType { get; set; }
        public UnitStatus UnitStatus { get; set; }
        public string Description { get; set; }
        public string HotelId { get; set; }
        public Hotel Hotel { get; set; }
        public ICollection<Booking> Bookings { get; set; } = new HashSet<Booking>();
        public ICollection<Review> Reviews { get; set; } = new HashSet<Review>();
        public ICollection<UnitPhoto> UnitPhotos { get; set; } = new HashSet<UnitPhoto>();
    }

}
