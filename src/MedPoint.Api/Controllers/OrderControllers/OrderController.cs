using Microsoft.AspNetCore.Mvc;
using MedPoint.Api.Models;
using MedPoint.Service.Dtos.Medications;
using MedPoint.Service.Dtos.Orders;
using MedPoint.Service.Interfaces.IMedicationServices;
using MedPoint.Service.Interfaces.IOrderServices;

namespace MedPoint.Api.Controllers.OrderControllers
{
    public class OrderController(IOrderService orderService) : BaseController
    {
        private readonly IOrderService orderService = orderService;

        [HttpGet]

        public async Task<IActionResult> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var response = new Response()
            {
                StatusCode = 200,
                Message = "Success",
                Data = await orderService.GetAllAsync(cancellationToken)
            };

            return Ok(response);
        }

        [HttpGet("{id}")]

        public async Task<IActionResult> GetAsync(
            [FromRoute(Name = "id")] long id,
            CancellationToken cancellationToken = default)
        {
            var response = new Response()
            {
                StatusCode = 200,
                Message = "Success",
                Data = await orderService.GetByIdAsync(id, cancellationToken)
            };

            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] OrderForCreationDto dto, CancellationToken cancellationToken = default)
        {
            var response = new Response()
            {
                StatusCode = 201,
                Message = "Success",
                Data = await orderService.AddAsync(dto)
            };

            return Ok(response);
        }

        [HttpDelete("{id}")]

        public async Task<IActionResult> DeleteAsync(
            [FromRoute] int id,
            CancellationToken cancellationToken = default)
        {
            return Ok(await orderService.CancelOrderAsync(id, cancellationToken));
        }
    }
}
