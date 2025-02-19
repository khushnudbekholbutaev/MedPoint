using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MedPoint.Data.IRepositories;
using MedPoint.Domain.Entities.Orders;
using MedPoint.Domain.Entities.Users;
using MedPoint.Service.Dtos.Users;
using MedPoint.Service.Exceptions;
using MedPoint.Service.Interfaces.IUserServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MedPoint.Service.Extensions;

namespace MedPoint.Service.Services.UserServices;

public class UserService(IRepository<User> repository, IMapper mapper) : IUserService
{
    private readonly IRepository<User> userRepository = repository;
    private readonly IMapper mapper = mapper;
    public async Task<bool> RemoveAsync(long id, CancellationToken cancellationToken = default)
    {
        var userExists = await userRepository.SelectAll()
            .AnyAsync(x => x.Id == id, cancellationToken);

        if (!userExists)
        {
            throw new MedPointException(404, "User with this ID is not found.");
        }

        return await userRepository.DeleteAsync(id, cancellationToken);
    }

    public async Task<UserForResultDto> AddAsync(UserForCreationDto dto, CancellationToken cancellationToken = default)
    {
        bool user = await userRepository.SelectAll()
            .AsNoTracking()
            .AnyAsync(x => x.Email == dto.Email, cancellationToken);

        if (user)
        {
            throw new MedPointException(409, "User with this email already exists.");
        }

        var mapped = mapper.Map<User>(dto);
        var HashedPassword = PasswordHelper.Hash(dto.Password);
        mapped.Password = HashedPassword.Hash;
        mapped.Salt = HashedPassword.Salt;

        var result = await userRepository.CreateAsync(mapped, cancellationToken);
        return mapper.Map<UserForResultDto>(result);
    }

    public async Task<IEnumerable<UserForResultDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var user = await userRepository.SelectAll()
            .Include(u => u.Orders)
            .AsNoTracking()
            .ToListAsync(cancellationToken);

        return mapper.Map<IEnumerable<UserForResultDto>>(user);    
    }

    public async Task<UserForResultDto> GetByIdAsync(long id, CancellationToken cancellationToken = default)
    {
        var user = await userRepository.SelectAll()
            .Include(u => u.Orders)
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken)
            ?? throw new MedPointException(404, "User with this ID is not found.");

        return mapper.Map<UserForResultDto>(user);
    }

    public async Task<UserForResultDto> ModifyAsync(long id, UserForUpdateDto dto, CancellationToken cancellationToken = default)
    {
        var user = await userRepository.SelectAll()
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken)
            ?? throw new MedPointException(404, "User with this ID is not found.");

        mapper.Map(dto, user);
        user.UpdatedAt = DateTimeOffset.UtcNow;

        var result = await userRepository.UpdateAsync(user, cancellationToken);

        return mapper.Map<UserForResultDto>(result);
    }
}

