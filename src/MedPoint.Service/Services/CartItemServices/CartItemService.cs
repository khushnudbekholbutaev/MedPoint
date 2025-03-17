using AutoMapper;
using MedPoint.Data.IRepositories;
using MedPoint.Domain.Entities.CartItems;
using MedPoint.Domain.Entities.Medications;
using MedPoint.Domain.Entities.Users;
using MedPoint.Service.Dtos.CartItems;
using MedPoint.Service.Exceptions;
using MedPoint.Service.Interfaces.ICartItemServices;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedPoint.Service.Services.CartItemServices
{
    public class CartItemService(
        IRepository<User> userRepository,
        IRepository<Medication> medicationRepository,
        IRepository<CartItem> itemsRepository,
        IMapper mapper) : ICartItemService
    {
        private readonly IRepository<User> userRepository = userRepository;
        private readonly IRepository<CartItem> itemsRepository = itemsRepository;
        private readonly IRepository<Medication> medicationRepository = medicationRepository;
        private readonly IMapper mapper = mapper;
        public async Task<CartItemForResultDto> AddAsync(CartItemForCreationDto dto, CancellationToken cancellationToken = default)
        {
            var users = await userRepository.SelectAll()
                .AsNoTracking()
                .FirstOrDefaultAsync(i => i.Id == dto.UserId, cancellationToken)
                 ?? throw new MedPointException(404, "User with this ID is not found.");

            var medications = await medicationRepository.SelectAll()
                .AsNoTracking()
                .FirstOrDefaultAsync(i => i.Id == dto.MedicationId, cancellationToken)
                ?? throw new MedPointException(404, "Medication with this ID is not found.");

            var usermed = await itemsRepository.SelectAll()
               .AsNoTracking()
               .AnyAsync(i => i.UserId == dto.UserId && i.MedicationId == dto.MedicationId, cancellationToken);

            if (usermed is true)
            {
                throw new MedPointException(409, "Cart already exists");
            }

            var mapped = mapper.Map<CartItem>(dto);
            mapped.Count = 1;

            var result = await itemsRepository.CreateAsync(mapped, cancellationToken);

            return mapper.Map<CartItemForResultDto>(result);
        }

        public async Task<CartItemForResultDto> GetByIdAsync(long id, CancellationToken cancellationToken = default)
        {
            var items = await itemsRepository.SelectAll()
                .FirstOrDefaultAsync(i => i.Id == id, cancellationToken)
                ?? throw new MedPointException(404, "Cart with this ID is not found.");

            return mapper.Map<CartItemForResultDto>(items);
        }

        public async Task<bool> RemoveAsync(long id, CancellationToken cancellationToken = default)
        {
            var cartItems = await itemsRepository.SelectAll()
                .AsNoTracking()
                .FirstOrDefaultAsync(i => i.Id == id, cancellationToken)
                ?? throw new MedPointException(404, "Cart with this ID is not found.");

            return await itemsRepository.DeleteAsync(id, cancellationToken);
        }

        public async Task<CartItemForResultDto?> UpdateCartAsync(CartItemForCreationDto dto, bool isIncreasing, CancellationToken cancellationToken = default)
        {
            var cartItem = await itemsRepository.SelectAll()
                .FirstOrDefaultAsync(i => i.UserId == dto.UserId && i.MedicationId == dto.MedicationId, cancellationToken)
                ?? throw new MedPointException(404, "Cart with this ID is not found.");

            if (isIncreasing)
            {
                cartItem.Count++;
            }
            else
            {
                if (cartItem.Count > 1)
                {
                    cartItem.Count--;
                }
                else
                {
                    await itemsRepository.DeleteAsync(cartItem.Id, cancellationToken);
                    return null;
                }
            }

            var updatedItem = await itemsRepository.UpdateAsync(cartItem, cancellationToken);
            return mapper.Map<CartItemForResultDto>(updatedItem);
        }
    }
}
