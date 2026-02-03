using HotelPlatform.DAL.Models.BaseModel;

namespace HotelPlatform.DAL.Models.SystemModels
{
    public class Review:BaseEntity
    {
        public string Id { get; set; }
        public int UnitRate { get; set; }
        public int HotelRate { get; set; }
        public string UnitComment { get; set; }
        public string HotelComment { get; set; }

        public string BookingId { get; set; }
        public string ClientId { get; set; }
        public string UnitId { get; set; }
        public string HotelId { get; set; }
        public Client Client { get; set; }
        public Booking Booking { get; set; }
        public Unit Unit { get; set; }
        public Hotel Hotel { get; set; }
    }

}
