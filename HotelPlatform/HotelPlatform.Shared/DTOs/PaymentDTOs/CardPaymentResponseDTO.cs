namespace HotelPlatform.Shared.DTOs.PaymentDTOs
{
    public class CardPaymentResponseDTO:BasePaymentResponseDTO
    {
        public Data data { get; set; }
        public class Data : BasePaymentData
        {
            public Payment_Data payment_data { get; set; }
        }
        public class Payment_Data
        {
            public string redirectTo { get; set; }
        }
    }
}
