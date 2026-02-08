using Base.DAL.Models.BaseModels;
using Base.Shared.Enums;

namespace Base.DAL.Models.SystemModels
{
    public class Hotel:BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string Governate { get; set; }
        public HotelStatus HotelStatus { get; set; }
        public Admin Manager { get; set; }
        public ICollection<Unit> Units { get; set; } = new HashSet<Unit>();
        public ICollection<HotelPhoto> HotelPhotos { get; set; } = new HashSet<HotelPhoto>();
        public ICollection<SeasonalPricing> SeasonalPricings { get; set; } = new HashSet<SeasonalPricing>();
        public ICollection<Review> Reviews { get; set; } = new HashSet<Review>();
    }

}
