using HotelPlatform.DAL.Models.BaseModel;

namespace HotelPlatform.DAL.Models.SystemModels
{
    public class UnitPhoto : BaseEntity
    {
        public string UnitId { get; set; }
        public string PhotoUrl { get; set; }
        public Unit Unit { get; set; }
    }

}
