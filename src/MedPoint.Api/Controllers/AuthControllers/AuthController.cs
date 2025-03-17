using MedPoint.Service.Dtos.Login;
using MedPoint.Service.Interfaces.IAuthServices;
using Microsoft.AspNetCore.Mvc;

namespace MedPoint.Api.Controllers.AuthControllers
{
    public class AuthController(IAuthService authService) : BaseController
    {
        private readonly IAuthService authService = authService;

        [HttpPost("Authenticate")]
        public async Task<IActionResult> PostAsync(LoginForCreationDto dto, CancellationToken cancellationToken = default)
        {
            var token = await authService.AuthenticateAsync(dto, cancellationToken);
            return Ok(token);
        }
    }
}
