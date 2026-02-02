using HotelPlatform.Services.Interfaces;
using HotelPlatform.Shared.DTOs;
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

        public PaymentController(IPaymentService paymentService , ILogger<PaymentController> logger )
        {
            _paymentService = paymentService;
            _logger = logger;
        }
        [HttpPost]
        public async Task<IActionResult> Payment([FromBody]EInvoiceRequestDTO eInvoiceRequestDTO)
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
        [HttpGet]
        public async  Task<IActionResult> GetPaymentMethods()
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

    }
}
