using Microsoft.AspNetCore.Mvc;
using MedPoint.Api.Models;
using MedPoint.Service.Dtos.Catalogs;
using MedPoint.Service.Dtos.Users;
using MedPoint.Service.Interfaces.ICatalogServices;
using MedPoint.Service.Interfaces.IUserServices;

namespace MedPoint.Api.Controllers.CatalogControllers
{
    public class CatalogController(ICatalogService catalogService) : BaseController
    {
        private readonly ICatalogService catalogService = catalogService;

        [HttpGet]

        public async Task<IActionResult> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var response = new Response()
            {
                StatusCode = 200,
                Message = "Success",
                Data = await catalogService.GetAllAsync(cancellationToken)
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
                Data = await catalogService.GetByIdAsync(id, cancellationToken)
            };

            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] CatalogForCreationDto dto, CancellationToken cancellationToken = default)
        {
            var response = new Response()
            {
                StatusCode = 200,
                Message = "Success",
                Data = await catalogService.AddAsync(dto, cancellationToken)
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
                Data = await catalogService.RemoveAsync(id, cancellationToken)
            };

            return Ok(response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(
            [FromRoute(Name = "id")] long id,
            [FromBody] CatalogForUpdateDto dto,
            CancellationToken cancellationToken = default)
        {
            var response = new Response()
            {
                StatusCode = 200,
                Message = "Success",
                Data = await catalogService.ModifyAsync(id, dto, cancellationToken)
            };

            return Ok(response);
        }
    }
}
