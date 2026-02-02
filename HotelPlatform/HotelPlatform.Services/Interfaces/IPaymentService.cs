using HotelPlatform.Shared.DTOs;
using HotelPlatform.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelPlatform.Services.Interfaces
{
    public interface IPaymentService
    {
        Task<EInvoiceResponseDTO?> CreateEInvoiceAsync(EInvoiceRequestDTO invoice);
        Task<IList<PaymentMethod>?> GetPaymentMethodsAsync();
        Task<PaymentMethodType> MappingPaymentMethod(int paymentId, IList<PaymentMethod>? methodsResponseDTOs = null);
        Task<(PaymentMethodType PaymentMethod, object Data)?> GeneralPayAsync(EInvoiceRequestDTO invoice);


    }
}
