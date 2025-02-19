using MedPoint.Service.Dtos.Medications;
using MedPoint.Service.Dtos.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedPoint.Service.Interfaces.IOrderServices
{
    public interface IOrderService
    {
        Task<bool> CancelOrderAsync(int orderId, CancellationToken cancellationToken = default);
        Task<OrderForResultDto> GetByIdAsync(long id, CancellationToken cancellationToken = default);
        Task<IEnumerable<OrderForResultDto>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<OrderForResultDto> AddAsync(OrderForCreationDto dto, CancellationToken cancellationToken = default);
    }
}
