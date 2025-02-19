using Microsoft.AspNetCore.Mvc;
using MedPoint.Api.Models;
using MedPoint.Service.Dtos.OrderDetails;
using MedPoint.Service.Dtos.Orders;
using MedPoint.Service.Interfaces.IOrderServices;
using MedPoint.Service.Interfaces.OrderDetailServices;

namespace MedPoint.Api.Controllers.OrderDetailControllers
{
    public class OrderDetailController(IOrderDetailService orderDetailService) : BaseController
    {
        private readonly IOrderDetailService orderDetailService = orderDetailService;

        [HttpGet]

        public async Task<IActionResult> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var response = new Response()
            {
                StatusCode = 200,
                Message = "Success",
                Data = await orderDetailService.GetAllAsync(cancellationToken)
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
                Data = await orderDetailService.GetByIdAsync(id, cancellationToken)
            };

            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] OrderDetailForCreationDto dto, CancellationToken cancellationToken = default)
        {
            var response = new Response()
            {
                StatusCode = 201,
                Message = "Success",
                Data = await orderDetailService.AddAsync(dto)
            };

            return Ok(response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(
            [FromRoute(Name = "id")] long id,
            [FromBody] OrderDetailForUpdateDto dto,
            CancellationToken cancellationToken = default)
        {
            var response = new Response()
            {
                StatusCode = 200,
                Message = "Success",
                Data = await orderDetailService.ModifyAsync(id, dto, cancellationToken)
            };

            return Ok(response);
        }

        [HttpDelete("{id}")]

        public async Task<IActionResult> DeleteAsync([FromRoute(Name = "id")] long id, CancellationToken cancellationToken = default)
        {
            var response = new Response()
            {
                StatusCode = 200,
                Message = "Success",
                Data = await orderDetailService.RemoveAsync(id, cancellationToken)
            };

            return Ok(response);
        }
    }
}
