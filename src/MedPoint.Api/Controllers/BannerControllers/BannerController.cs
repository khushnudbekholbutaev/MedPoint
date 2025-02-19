using MedPoint.Api.Models;
using MedPoint.Service.Dtos.Banners;
using MedPoint.Service.Dtos.Catalogs;
using MedPoint.Service.Interfaces.IBannerServices;
using MedPoint.Service.Services.CatalogServices;
using Microsoft.AspNetCore.Mvc;

namespace MedPoint.Api.Controllers.BannerControllers
{
    public class BannerController(IBannerService bannerService) : BaseController
    {
        private readonly IBannerService bannerService = bannerService;
        [HttpGet]

        public async Task<IActionResult> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var response = new Response()
            {
                StatusCode = 200,
                Message = "Success",
                Data = await bannerService.GetAllAsync(cancellationToken)
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
                Data = await bannerService.GetByIdAsync(id, cancellationToken)
            };

            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync(
            [FromForm] BannerForCreationDto dto, 
            CancellationToken cancellationToken = default)
        {
            var response = new Response()
            {
                StatusCode = 200,
                Message = "Success",
                Data = await bannerService.AddAsync(dto, cancellationToken)
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
                Data = await bannerService.RemoveAsync(id, cancellationToken)
            };

            return Ok(response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(
            [FromRoute(Name = "id")] long id,
            [FromForm] BannerForUpdateDto dto,
            CancellationToken cancellationToken = default)
        {
            var response = new Response()
            {
                StatusCode = 200,
                Message = "Success",
                Data = await bannerService.ModifyAsync(id, dto, cancellationToken)
            };

            return Ok(response);
        }
    }
}
