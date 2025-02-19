using Microsoft.AspNetCore.Mvc;
using MedPoint.Api.Models;
using MedPoint.Service.Dtos.Categories;
using MedPoint.Service.Interfaces.ICategoryServices;

namespace MedPoint.Api.Controllers.CategoryControllers
{
    public class CategoryController (ICategoryService categoryService) : BaseController
    {
        private readonly ICategoryService _categoryService = categoryService;

        [HttpGet]

        public async Task<IActionResult> GetAllAsync(CancellationToken cancellationToken = default) 
        {
            var response = new Response()
            {
                StatusCode = 200,
                Message = "Success",
                Data = await categoryService.GetAllAsync(cancellationToken)
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
                Data = await categoryService.GetByIdAsync(id)
            };

            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody]CategoryForCreationDto dto, CancellationToken cancellationToken = default)
        {
            var response = new Response()
            {
                StatusCode = 201,
                Message = "Success",
                Data = await categoryService.AddAsync(dto)
            };

            return Ok(response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(
            [FromRoute(Name = "id")] long id,
            [FromBody] CategoryForUpdateDto dto, 
            CancellationToken cancellationToken = default)
        {
            var response = new Response()
            {
                StatusCode = 200,
                Message = "Success",
                Data = await categoryService.ModifyAsync(id, dto, cancellationToken)
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
                Data = await categoryService.RemoveAsync(id, cancellationToken)
            };

            return Ok(response);
        }

    }
}
