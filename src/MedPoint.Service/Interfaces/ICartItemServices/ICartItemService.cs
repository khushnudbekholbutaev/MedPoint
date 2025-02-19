using MedPoint.Service.Dtos.CartItems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedPoint.Service.Interfaces.ICartItemServices
{
    public interface ICartItemService
    {
        Task<bool> RemoveAsync(long id, CancellationToken cancellationToken = default);
        Task<CartItemForResultDto?> UpdateCartAsync(CartItemForCreationDto dto, bool isIncreasing, CancellationToken cancellationToken = default);
        Task<CartItemForResultDto> GetByIdAsync(long id, CancellationToken cancellationToken = default);
        Task<CartItemForResultDto> AddAsync(CartItemForCreationDto dto, CancellationToken cancellationToken = default);
    }
}
