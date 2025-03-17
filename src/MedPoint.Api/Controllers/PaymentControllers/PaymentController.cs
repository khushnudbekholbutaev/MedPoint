using Microsoft.AspNetCore.Mvc;
using MedPoint.Api.Models;
using MedPoint.Service.Dtos.Payments;
using MedPoint.Service.Dtos.Users;
using MedPoint.Service.Interfaces.IPaymentServices;
using MedPoint.Service.Interfaces.IUserServices;

namespace MedPoint.Api.Controllers.PaymentControllers
{
    public class PaymentController(IPaymentService paymentService) : BaseController
    {
        private readonly IPaymentService paymentService = paymentService;

        [HttpGet]

        public async Task<IActionResult> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var response = new Response()
            {
                StatusCode = 200,
                Message = "Success",
                Data = await paymentService.GetAllAsync(cancellationToken)
            };

            return Ok(response);
        }

        [HttpGet("{id}")]

        public async Task<IActionResult> GetAsync(long id, CancellationToken cancellationToken = default)
        {
            var response = new Response()
            {
                StatusCode = 200,
                Message = "Success",
                Data = await paymentService.GetByIdAsync(id, cancellationToken)
            };

            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] PaymentForCreationDto dto, CancellationToken cancellationToken = default)
        {
            var response = new Response()
            {
                StatusCode = 200,
                Message = "Success",
                Data = await paymentService.AddAsync(dto, cancellationToken)
            };

            return Ok(response);
        }
    }
}
