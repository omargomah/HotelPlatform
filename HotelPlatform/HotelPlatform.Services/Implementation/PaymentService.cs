using HotelPlatform.Services.Interfaces;
using HotelPlatform.Shared.DTOs;
using HotelPlatform.Shared.Enums;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace HotelPlatform.Services.Implementation
{
    public class PaymentService : IPaymentService
    {
        private readonly FawaterakOptions _fawaterakOptions;
        private readonly IHttpClientFactory _httpClientFactory;

        public PaymentService(IOptions<FawaterakOptions> fawaterakOptions, IHttpClientFactory httpClientFactory) 
        {
            _fawaterakOptions = fawaterakOptions.Value;
            _httpClientFactory = httpClientFactory;
        }
        public async Task<EInvoiceResponseDTO?> CreateEInvoiceAsync(EInvoiceRequestDTO invoice)
        {
            var client = _httpClientFactory.CreateClient();
            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, $"{_fawaterakOptions.BaseUrl}/createInvoiceLink");
            httpRequestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _fawaterakOptions.ApiKey);
            httpRequestMessage.Content = new StringContent(JsonSerializer.Serialize(invoice), Encoding.UTF8, "application/json");
            var response = await client.SendAsync(httpRequestMessage);
            if(response.IsSuccessStatusCode)
                return JsonSerializer.Deserialize<EInvoiceResponseDTO>(await response.Content.ReadAsStringAsync())!;
            return null!;
        }

        public async Task<IList<PaymentMethod>?> GetPaymentMethodsAsync()
        {
            var client = _httpClientFactory.CreateClient();
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, $"{_fawaterakOptions.BaseUrl}/getPaymentmethods");
            requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _fawaterakOptions.ApiKey);
            requestMessage.Content = new StringContent("", Encoding.UTF8, "application/json");
            var response = await client.SendAsync(requestMessage);
            if (response.IsSuccessStatusCode)
            {
                var getPaymentMethods = JsonSerializer.Deserialize<GetPaymentMethodsResponseDTO>(await response.Content.ReadAsStringAsync())!;
                foreach (var item in getPaymentMethods.data)
                    item.PaymentMethodType = await MappingPaymentMethod(item.PaymentId, getPaymentMethods.data);
                return getPaymentMethods.data;
            }
            return null;
        }
        public async Task<PaymentMethodType> MappingPaymentMethod(int paymentId,IList<PaymentMethod>? methodsResponseDTOs = null)
        {
            methodsResponseDTOs = methodsResponseDTOs.IsNullOrEmpty() ? await GetPaymentMethodsAsync(): methodsResponseDTOs;
            var MappingMethod = methodsResponseDTOs!.FirstOrDefault(mr => mr.PaymentId == paymentId);
            if(MappingMethod is null || MappingMethod.NameInEnglish.IsNullOrEmpty())
                return PaymentMethodType.Card;
            
            if(MappingMethod.NameInEnglish.Contains(PaymentMethodType.Card.ToString(),StringComparison.OrdinalIgnoreCase))
                return PaymentMethodType.Card;
            else if (MappingMethod.NameInEnglish.Contains(PaymentMethodType.Fawry.ToString(), StringComparison.OrdinalIgnoreCase))
                return PaymentMethodType.Fawry;
            else if(MappingMethod.NameInEnglish.Contains(PaymentMethodType.Wallet.ToString(), StringComparison.OrdinalIgnoreCase)
                    || MappingMethod.NameInEnglish.Contains(PaymentMethodType.Meeza.ToString(), StringComparison.OrdinalIgnoreCase))
                return PaymentMethodType.Wallet;

            return PaymentMethodType.Card;
        }

        public async Task<(PaymentMethodType PaymentMethod,object Data)?> GeneralPayAsync(EInvoiceRequestDTO invoice)
        {
            var client = _httpClientFactory.CreateClient();
            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, $"{_fawaterakOptions.BaseUrl}/invoiceInitPay");
            httpRequestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _fawaterakOptions.ApiKey);
            httpRequestMessage.Content = new StringContent(JsonSerializer.Serialize(invoice), Encoding.UTF8, "application/json");
            var response = await client.SendAsync(httpRequestMessage);
            if (response.IsSuccessStatusCode)
            {
                var paymentMethod = await MappingPaymentMethod((int)invoice.PaymentMethodId!);
                return paymentMethod switch
                {
                    PaymentMethodType.Card => new() { PaymentMethod = PaymentMethodType.Card, Data = JsonSerializer.Deserialize<CardPaymentResponseDTO>(await response.Content.ReadAsStringAsync())! },
                    PaymentMethodType.Fawry => new() { PaymentMethod = PaymentMethodType.Fawry, Data = JsonSerializer.Deserialize<FawryPaymentResponseDTO>(await response.Content.ReadAsStringAsync())! },
                    PaymentMethodType.Wallet => new() { PaymentMethod = PaymentMethodType.Wallet, Data = JsonSerializer.Deserialize<WalletPaymentResponseDTO>(await response.Content.ReadAsStringAsync())! },
                    _ => null,
                };
            }
            return null;

        }
    }
}
