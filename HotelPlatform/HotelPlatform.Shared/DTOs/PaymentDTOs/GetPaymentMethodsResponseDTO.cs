namespace HotelPlatform.Shared.DTOs.PaymentDTOs
{
    public class GetPaymentMethodsResponseDTO
    {
        public string status { get; set; }
        public PaymentMethod[] data { get; set; }
    }


}
