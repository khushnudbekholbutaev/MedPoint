using MedPoint.Service.Dtos.OrderDetails;
using MedPoint.Service.Dtos.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedPoint.Service.Interfaces.OrderDetailServices
{
    public interface IOrderDetailService
    {
        Task<bool> RemoveAsync(long id, CancellationToken cancellationToken = default);
        Task<OrderDetailForResultDto> GetByIdAsync(long id, CancellationToken cancellationToken = default);
        Task<IEnumerable<OrderDetailForResultDto>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<OrderDetailForResultDto> AddAsync(OrderDetailForCreationDto dto, CancellationToken cancellationToken = default);
        Task<OrderDetailForResultDto> ModifyAsync(long id, OrderDetailForUpdateDto dto, CancellationToken cancellationToken = default);
    }

}
