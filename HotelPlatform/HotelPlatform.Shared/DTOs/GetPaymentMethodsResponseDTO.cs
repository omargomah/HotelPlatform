namespace HotelPlatform.Shared.DTOs
{
    public class GetPaymentMethodsResponseDTO
    {
        public string status { get; set; }
        public PaymentMethod[] data { get; set; }
    }


}
