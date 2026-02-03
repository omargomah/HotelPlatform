using HotelPlatform.Services.Interfaces;
using HotelPlatform.Shared.DTOs.PaymentDTOs;
using HotelPlatform.Shared.Enums;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace HotelPlatform.Services.Implementation
{
    // this class is responsible for handling the payment process by integrating with the Fawaterak API.
    public class PaymentService : IPaymentService
    {
        private readonly FawaterakOptions _fawaterakOptions;
        private readonly IHttpClientFactory _httpClientFactory; 

        public PaymentService(IOptions<FawaterakOptions> fawaterakOptions, IHttpClientFactory httpClientFactory) 
        {
            _fawaterakOptions = fawaterakOptions.Value;
            _httpClientFactory = httpClientFactory;
        }
        /// <summary>
        /// this method is responsible for creating the e-invoice link by sending a POST request to the Fawaterak API with the invoice details 
        /// and returns the response containing the invoice link and other relevant information. 
        /// The method also handles the authentication by including the API key in the request headers.
        /// If the request is successful, it deserializes the response into an EInvoiceResponseDTO object and returns it; otherwise, it returns null.
        /// </summary>
        /// <param name="invoice"></param>
        /// <returns></returns>
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

        /// <summary>
        /// this method is responsible for retrieving the available payment methods from the Fawaterak API by sending a GET request.
        /// </summary>
        /// <returns></returns>
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
        /// <summary>
        /// this method is responsible for mapping the payment method based on the payment ID.
        /// It takes a payment ID and an optional list of payment methods as parameters.
        /// </summary>
        /// <param name="paymentId"></param>
        /// <param name="methodsResponseDTOs"></param>
        /// <returns></returns>
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
        /// <summary>
        /// this method is responsible for processing the payment by sending a POST request to the Fawaterak API with the invoice details and payment method.
        /// and returns the response containing the payment result and other relevant information.
        /// and there is a switch case to handle the response based on the payment method type and deserialize it into the appropriate DTO object 
        /// (CardPaymentResponseDTO, FawryPaymentResponseDTO, WalletPaymentResponseDTO) 
        /// and return it as a tuple with the payment method type. If the request is not successful, it returns null.
        /// </summary>
        /// <param name="invoice"></param>
        /// <returns></returns>

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

        #region WebHook Verification
        public bool VerifyWebhook(WebHookPaidDTO webHook)
        {
            var generatedHashKey =
                GenerateHashKeyForWebhookVerification(webHook.InvoiceId, webHook.InvoiceKey, webHook.PaymentMethod);
            return generatedHashKey == webHook.HashKey;
        }

        public bool VerifyCancelTransaction(WebHookCancelDTO webHookCancelDTO)
        {
            var generatedHashKey = GenerateHashKeyForCancelTransaction(webHookCancelDTO.ReferenceId, webHookCancelDTO.PaymentMethod);
            return generatedHashKey == webHookCancelDTO.HashKey;
        }

        public bool VerifyApiKeyTransaction(string apiKey)
        {
            return apiKey == _fawaterakOptions.ApiKey;
        }

        #endregion



        #region Generate HashKey
        public string GenerateHashKeyForIFrame(string domain)
        {
            var queryParam = $"Domain={domain}&ProviderKey={_fawaterakOptions.ProviderKey}";
            using var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(_fawaterakOptions.ApiKey));
            var hashBytes = hmac.ComputeHash(Encoding.UTF8.GetBytes(queryParam));
            return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
        }

        private string GenerateHashKeyForWebhookVerification(long invoiceId, string invoiceKey, string paymentMethod)
        {
            var queryParam = $"InvoiceId={invoiceId}&InvoiceKey={invoiceKey}&PaymentMethod={paymentMethod}";
            using var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(_fawaterakOptions.ApiKey));
            var hashBytes = hmac.ComputeHash(Encoding.UTF8.GetBytes(queryParam));
            return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
        }

        private string GenerateHashKeyForCancelTransaction(string referenceId, string paymentMethod)
        {
            var queryParam = $"referenceId={referenceId}&PaymentMethod={paymentMethod}";
            using var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(_fawaterakOptions.ApiKey));
            var hashBytes = hmac.ComputeHash(Encoding.UTF8.GetBytes(queryParam));
            return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
        }
        #endregion
    }
}
