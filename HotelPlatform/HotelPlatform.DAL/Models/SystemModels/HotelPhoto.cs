using HotelPlatform.DAL.Models.BaseModel;

namespace HotelPlatform.DAL.Models.SystemModels
{
    public class HotelPhoto: BaseEntity
    {
        public string PhotoLink { get; set; }
        public string HotelId { get; set; }
        public Hotel Hotel { get; set; }
    }

}
