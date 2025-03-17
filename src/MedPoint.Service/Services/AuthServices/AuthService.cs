using MedPoint.Data.IRepositories;
using MedPoint.Domain.Entities.Users;
using MedPoint.Service.Dtos.Login;
using MedPoint.Service.Exceptions;
using MedPoint.Service.Extensions;
using MedPoint.Service.Interfaces.IAuthServices;
using Microsoft.AspNetCore.Http.Features.Authentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MedPoint.Service.Services.AuthServices
{
    public class AuthService(IConfiguration configuration, IRepository<User> userRepository) : IAuthService
    {   
        private readonly IConfiguration configuration = configuration;
        private readonly IRepository<User> userRepository = userRepository;
        public async Task<LoginForResultDto> AuthenticateAsync(LoginForCreationDto dto, CancellationToken cancellationToken = default)
        {
           var user = await userRepository.SelectAll()
                .FirstOrDefaultAsync(u => u.PhoneNumber == dto.Phone, cancellationToken);

            if (user is null || !PasswordHelper.Verify(dto.Password, user.Salt, user.Password))
                throw new MedPointException(401, "Phone or Password is incorrect");

            return new LoginForResultDto
            {
                Role = user.Status.ToString(),
                Token = GenerateToken(user),
            };
        }

        private string GenerateToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = configuration["JWT:Key"] ?? throw new ArgumentNullException("JWT:Key is missing in configuration");
            var tokenKey = Encoding.UTF8.GetBytes(key);

            if (!double.TryParse(configuration["JWT:Expire"], out double expireMinutes))
                expireMinutes = 60;

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Role, user.Status.ToString())
                }),
                Audience = configuration["JWT:Audience"] ?? throw new ArgumentNullException("JWT:Audience is missing"),
                Issuer = configuration["JWT:Issuer"] ?? throw new ArgumentNullException("JWT:Issuer is missing"),
                IssuedAt = DateTime.UtcNow,
                Expires = DateTime.UtcNow.AddMinutes(expireMinutes),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(tokenKey),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
