using HotelPlatform.Services.Interfaces;
using HotelPlatform.Shared.DTOs.PaymentDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HotelPlatform.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;
        private readonly ILogger<PaymentController> _logger;

        public PaymentController(IPaymentService paymentService, ILogger<PaymentController> logger)
        {
            _paymentService = paymentService;
            _logger = logger;
        }
        #region Pseudo Code steps to create the invoice and process the payment
        // 1. Validate the ShoppingCartId && Payment Method Id
        // 2. Retrieve the shopping cart items from the database or cache
        // 3. validate the items in the cart
        // 4. Calculate the total price of the items in the cart
        // 5. create an order record in the database with status "Pending"
        // 6. Create the invoice using FawaterakPaymentService
        // 7. Save the invoice id in the order record & order id in the invoice payload
        // 8. Return the order details along with the invoice link to the client
        #endregion

        #region The Endpoint to Create E-invoice Link for Payment
        [HttpPost]
        public async Task<IActionResult> Payment([FromBody] EInvoiceRequestDTO eInvoiceRequestDTO)
        {
            _logger.LogInformation("Payment request received at {Time} and start to pay", DateTime.UtcNow);
            var result = await _paymentService.CreateEInvoiceAsync(eInvoiceRequestDTO);
            if (result is not null)
            {
                _logger.LogInformation("Payment request processed successfully at {Time}", DateTime.UtcNow);
                return Ok(result);
            }
            _logger.LogError("Payment request failed at {Time}", DateTime.UtcNow);
            return BadRequest("Failed to Pay try again in another time");
        }
        #endregion

        #region Gateway Integration

        #region The Endpoint to Get All Payment Methods
        [HttpGet]
        public async Task<IActionResult> GetPaymentMethods()
        {
            _logger.LogInformation("GetPaymentMethods request received at {Time}", DateTime.UtcNow);
            var result = await _paymentService.GetPaymentMethodsAsync();
            if (result is not null)
            {
                _logger.LogInformation("GetPaymentMethods request processed successfully at {Time}", DateTime.UtcNow);
                return Ok(result);
            }
            _logger.LogError("GetPaymentMethods request failed at {Time}", DateTime.UtcNow);
            return BadRequest("Failed to get Payment Methods try again in another time");
        }
        #endregion

        #region The Endpoint to Process Payment using the selected Payment Method
        [HttpPost("GeneralPay")]
        public async Task<IActionResult> GeneralPay([FromBody] EInvoiceRequestDTO eInvoiceRequestDTO)
        {
            _logger.LogInformation("GeneralPay request received at {Time} and start to pay", DateTime.UtcNow);
            var result = await _paymentService.GeneralPayAsync(eInvoiceRequestDTO);
            if (result is not null)
            {
                _logger.LogInformation("GeneralPay request processed successfully at {Time}", DateTime.UtcNow);
                return Ok(result.Value.Data);
            }
            _logger.LogError("GeneralPay request failed at {Time}", DateTime.UtcNow);
            return BadRequest("Failed to Pay try again in another time");
        }
        #endregion

        #endregion

        #region Get Hash Key for IFrame Endpoint
        [HttpGet("GenerateHashKeyForIFrame")]
        public IActionResult GenerateHashKeyForIFrame([FromQuery] string domain)
        {
            _logger.LogInformation("GenerateHashKeyForIFrame request received at {Time}", DateTime.UtcNow);
            var result = _paymentService.GenerateHashKeyForIFrame(domain);
            if (!string.IsNullOrEmpty(result))
            {
                _logger.LogInformation("GenerateHashKeyForIFrame request processed successfully at {Time}", DateTime.UtcNow);
                return Ok(result);
            }
            _logger.LogError("GenerateHashKeyForIFrame request failed at {Time}", DateTime.UtcNow);
            return BadRequest("Failed to generate hash key try again in another time");
        }
        #endregion

        #region webhook verification endpoint
        [HttpPost("paid_json")]
        public async Task<IActionResult> WebHookPaid([FromBody] WebHookPaidDTO webHookPaidDTO)
        {
            var result = _paymentService.VerifyWebhook(webHookPaidDTO);
            if(result)
                return Ok("the Payment Done");
            _logger.LogWarning("Webhook verification failed for Invoice ID: {InvoiceId} at {Time}", webHookPaidDTO.InvoiceId, DateTime.UtcNow);
            return BadRequest();
        }
        [HttpPost("cancel_json")]
        public async Task<IActionResult> WebHookCancel([FromBody] WebHookCancelDTO  webHookPaidDTO)
        {
            var result = _paymentService.VerifyCancelTransaction(webHookPaidDTO);
            if (result)
                return Ok("the Payment Canceled");
            _logger.LogWarning("Webhook cancellation verification failed at {Time}",DateTime.UtcNow);
            return BadRequest();
        }

        #endregion
    }
}
