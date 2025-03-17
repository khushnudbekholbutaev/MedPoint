using Microsoft.AspNetCore.Mvc;
using MedPoint.Api.Models;
using Microsoft.AspNetCore.Http;
using MedPoint.Service.Dtos.Categories;
using MedPoint.Service.Dtos.Medications;
using MedPoint.Service.Interfaces.ICategoryServices;
using MedPoint.Service.Interfaces.IMedicationServices;
using MedPoint.Service.Services.CategoryServices;
using static System.Net.Mime.MediaTypeNames;
using MedPoint.Service.Services.MedicationServices;
using System.Xml.Linq;
using MedPoint.Service.Configurations;
using MedPoint.Domain.Entities.Medications;
using MedPoint.Data.IRepositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace MedPoint.Api.Controllers.MedicationControllers
{
    public class MedicationController(
        IMedicationService medicationService,
        IRepository<Medication> medications) : BaseController
    {
        private readonly IMedicationService medicationService = medicationService;
        private readonly IRepository<Medication> medications = medications;

        [HttpGet("categoryId")]
        public async Task<IActionResult> GetByCategoryAsync(
            [FromRoute]int categoryId,
            [FromQuery]PaginationParams @params, 
            CancellationToken cancellationToken = default)
        {
            var response = new Response
            {
                StatusCode = 200,
                Message = "Success",
                Data = await medicationService.GetByCategoryAsync(categoryId, @params, cancellationToken)
            };
            return Ok(response);
        }

        [HttpGet("name")]
        public async Task<IActionResult> SearchByName(
            [FromQuery] string name,
            [FromQuery]PaginationParams @params, 
            CancellationToken cancellationToken = default)
        {
            var response = new Response
            {
                StatusCode = 200,
                Message = "Success",
                Data = await medicationService.GetByNameAsync(name, @params, cancellationToken)
            };
            return Ok(response);
        }

        [HttpGet("paged")]

        public async Task<IActionResult> GetAllAsync(
            [FromQuery] PaginationParams @params,
            CancellationToken cancellationToken = default)
        {
            var meds = await medications.SelectAll().CountAsync(cancellationToken);

            var response = new Response()
            {
                StatusCode = 200,
                Message = Convert.ToString(meds),
                Data = await medicationService.GetAllAsync(@params, cancellationToken)
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
                Data = await medicationService.GetByIdAsync(id, cancellationToken)
            };

            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync(
            [FromForm] MedicationForCreationDto dto, 
            CancellationToken cancellationToken = default)
        {

            var response = new Response()
            {
                StatusCode = 201,
                Message = "Success",
                Data = await medicationService.AddAsync(dto, cancellationToken)
            };

            return Ok(response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(
            [FromRoute] long id,
            [FromForm] MedicationForUpdateDto dto,
            CancellationToken cancellationToken = default)
        {
            var response = new Response()
            {
                StatusCode = 200,
                Message = "Success",
                Data = await medicationService.ModifyAsync(id, dto, cancellationToken)
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
                Data = await medicationService.RemoveAsync(id, cancellationToken)
            };

            return Ok(response);
        }
    }
}
