using HotelPlatform.Shared.Enums;
using System.Text.Json.Serialization;

namespace HotelPlatform.Shared.DTOs.PaymentDTOs
{
    public class PaymentMethod
    {
        [JsonPropertyName("paymentMethod")]
        public PaymentMethodType? PaymentMethodType { get; set; }
        [JsonPropertyName("paymentId")]
        public int PaymentId { get; set; }
        [JsonPropertyName("name_en")]
        public string NameInEnglish { get; set; }
        [JsonPropertyName("name_ar")]
        public string NameInArabic { get; set; }
        [JsonPropertyName("redirect")]
        public string RedirectLink { get; set; }
        [JsonPropertyName("logo")]
        public string Logo { get; set; }
    }


}
