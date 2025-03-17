using MedPoint.Domain.Entities.CartItems;
using MedPoint.Domain.Entities.Medications;
using MedPoint.Domain.Entities.Users;
using MedPoint.Service.Dtos.CartItems;
using MedPoint.Service.Interfaces.ICartItemServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MedPoint.Api.Controllers.CartItemControllers
{
    public class CartItemController(ICartItemService itemService) : BaseController
    {
        private readonly ICartItemService itemService = itemService;

        [HttpPost]

        public async Task<IActionResult> PostAsync(CartItemForCreationDto dto, CancellationToken cancellationToken = default)
        {
            return Ok(await itemService.AddAsync(dto, cancellationToken));
        }

        [HttpGet("{id}")]

        public async Task<IActionResult> GetAsync(long id, CancellationToken cancellationToken = default)
        {
            return Ok(await itemService.GetByIdAsync(id, cancellationToken));
        }

        [HttpDelete]

        public async Task<IActionResult> DeleteAsync(long id, CancellationToken cancellationToken = default)
        {
            return Ok(await itemService.RemoveAsync(id, cancellationToken));
        }

        [HttpPut]
        public async Task<IActionResult> PutAsync(CartItemForCreationDto dto, bool isIncreasing, CancellationToken cancellationToken = default)
        {
            return Ok(await itemService.UpdateCartAsync(dto, isIncreasing, cancellationToken));
        }
    }
}
