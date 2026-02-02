namespace HotelPlatform.Shared.DTOs
{
    public class FawryPaymentResponseDTO : BasePaymentResponseDTO
    {
        public string status { get; set; }
        public Data data { get; set; }
        public class Data : BasePaymentData
        {
            public Payment_Data payment_data { get; set; }
        }
        public class Payment_Data
        {
            public string fawryCode { get; set; }
            public string expireDate { get; set; }
        }
    }







}
