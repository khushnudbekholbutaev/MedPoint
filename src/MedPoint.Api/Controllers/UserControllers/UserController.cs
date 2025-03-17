using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using MedPoint.Api.Models;
using MedPoint.Domain.Entities.Users;
using MedPoint.Domain.Validations;
using MedPoint.Service.Dtos.Users;
using MedPoint.Service.Interfaces.IUserServices;
using MedPoint.Service.Services.UserServices;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Authorization;

namespace MedPoint.Api.Controllers.UserControllers;

public class UserController(IUserService userService, IValidator<UserForCreationDto> validator) : BaseController
{
    private readonly IUserService userService = userService;
    private readonly IValidator<UserForCreationDto> validator = validator;

    [HttpGet]

    public async Task<IActionResult> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var response = new Response()
        {
            StatusCode = 200,
            Message = "Success",
            Data = await userService.GetAllAsync(cancellationToken)
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
            Data = await userService.GetByIdAsync(id, cancellationToken)
        };

        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> PostAsync([FromBody] UserForCreationDto dto, CancellationToken cancellationToken = default)
    {
        var validatorResult = await validator.ValidateAsync(dto, cancellationToken);
        if (!validatorResult.IsValid)
        {
            return BadRequest(validatorResult.Errors);
        }

        var response = new Response()
        {
            StatusCode = 200,
            Message = "Success",
            Data = await userService.AddAsync(dto, cancellationToken)
        };

        return Ok(response);
    }

    [HttpDelete("{id}")]

    public async Task<IActionResult> PostAsync([FromRoute(Name = "id")] long id, CancellationToken cancellationToken = default)
    {
        var response = new Response()
        {
            StatusCode = 200,
            Message = "Success",
            Data = await userService.RemoveAsync(id, cancellationToken)
        };

        return Ok(response);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutAsync(
        [FromRoute(Name = "id")] long id,
        [FromBody] UserForUpdateDto dto,
        CancellationToken cancellationToken = default)
    {
        var response = new Response()
        {
            StatusCode = 200,
            Message = "Success",
            Data = await userService.ModifyAsync(id, dto, cancellationToken)
        };

        return Ok(response);
    }
}

