namespace HotelPlatform.Shared.DTOs.PaymentDTOs
{
    public class EInvoiceResponseDTO
    {
        public string status { get; set; }
        public Data data { get; set; }
        public PayLoad PayLoad { get; set; }
    }
}
