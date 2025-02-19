using AutoMapper;
using MedPoint.Data.IRepositories;
using MedPoint.Domain.Entities.CartItems;
using MedPoint.Domain.Entities.Favorites;
using MedPoint.Domain.Entities.Medications;
using MedPoint.Domain.Entities.Users;
using MedPoint.Service.Dtos.CartItems;
using MedPoint.Service.Dtos.Favorites;
using MedPoint.Service.Dtos.Users;
using MedPoint.Service.Exceptions;
using MedPoint.Service.Interfaces.IFavoriteServices;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedPoint.Service.Services.FavoriteServices
{
    public class FavoriteService(
        IRepository<User> userRepository,
        IRepository<Medication> medicationRepository,
        IRepository<Favorite> favoriteRepository,
        IMapper mapper) : IFavoriteService
    {
        private readonly IRepository<User> userRepository = userRepository;
        private readonly IRepository<Medication> medicationRepository = medicationRepository;
        private readonly IRepository<Favorite> favoriteRepository = favoriteRepository;
        private readonly IMapper mapper = mapper;

        public async Task<FavoriteForResultDto> AddAsync(FavoriteForCreationDto dto, CancellationToken cancellationToken = default)
        {
            var users = await userRepository.SelectAll()
                .AsNoTracking()
                .FirstOrDefaultAsync(i => i.Id == dto.UserId, cancellationToken)
                ?? throw new MedPointException(404, "User with this ID is not found.");

            var medications = await medicationRepository.SelectAll()
                .AsNoTracking()
                .FirstOrDefaultAsync(i => i.Id == dto.MedicationId, cancellationToken)
                ?? throw new MedPointException(404, "Medication with this ID is not found.");

            var mapped = mapper.Map<Favorite>(dto);
            var result = await favoriteRepository.CreateAsync(mapped, cancellationToken);

            return mapper.Map<FavoriteForResultDto>(result);
        }

        public async Task<bool> RemoveAsync(long id, CancellationToken cancellationToken = default)
        {
            var cartItems = await favoriteRepository.SelectAll()
                .AsNoTracking()
                .FirstOrDefaultAsync(i => i.Id == id, cancellationToken)
                ?? throw new MedPointException(404, "Favorite with this ID is not found.");

            return await favoriteRepository.DeleteAsync(id, cancellationToken);
        }

        public async Task<IEnumerable<FavoriteForResultDto>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var user = await favoriteRepository.SelectAll()
            .AsNoTracking()
            .ToListAsync(cancellationToken);

            return mapper.Map<IEnumerable<FavoriteForResultDto>>(user);
        }

        public async Task<IEnumerable<Medication>> GetMostLikedMedicationsAsync(CancellationToken cancellationToken)
        {
            return await favoriteRepository.SelectAll()
                .GroupBy(f => f.MedicationId)
                .OrderByDescending(g => g.Count())
                .Select(g => g.First().Medication)
                .Take(10)
                .ToListAsync(cancellationToken);
        }
    }
}
