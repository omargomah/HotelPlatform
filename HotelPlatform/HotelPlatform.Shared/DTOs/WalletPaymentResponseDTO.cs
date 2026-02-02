namespace HotelPlatform.Shared.DTOs
{
    public class WalletPaymentResponseDTO : BasePaymentResponseDTO
    {
        public string status { get; set; }
        public Data data { get; set; }
        public class Data:BasePaymentData
        {
            public Payment_Data payment_data { get; set; }
        }
        public class Payment_Data
        {
            public int meezaReference { get; set; }
            public string meezaQrCode { get; set; }
        }
    }







}
