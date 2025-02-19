using MedPoint.Service.Dtos.Login;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedPoint.Service.Interfaces.IAuthServices
{
    public interface IAuthService
    {
        Task<LoginForResultDto> AuthenticateAsync(LoginForCreationDto dto, CancellationToken cancellationToken = default);
    }
}
