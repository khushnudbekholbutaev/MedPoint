using MedPoint.Service.Dtos.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedPoint.Service.Interfaces.IUserServices;

public interface IUserService
{
    Task<bool> RemoveAsync(long id, CancellationToken cancellationToken = default);
    Task<UserForResultDto> GetByIdAsync(long id, CancellationToken cancellationToken = default);
    Task<UserForResultDto> ModifyAsync(long id,UserForUpdateDto dto, CancellationToken cancellationToken = default);
    Task<IEnumerable<UserForResultDto>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<UserForResultDto> AddAsync(UserForCreationDto dto, CancellationToken cancellationToken = default);
}

