using HotelPlatform.DAL.Models.BaseModel;
using HotelPlatform.Shared.Enums;

namespace HotelPlatform.DAL.Models.SystemModels
{
    public class Hotel:BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string Governate { get; set; }
        public HotelStatus HotelStatus { get; set; }
        public List<Unit> Units { get; set; }
        public ICollection<HotelPhoto> HotelPhotos { get; set; } = new HashSet<HotelPhoto>();
        public ICollection<SeasonalPricing> SeasonalPricings { get; set; } = new HashSet<SeasonalPricing>();
    }

}
