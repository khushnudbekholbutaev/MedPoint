using MedPoint.Service.Dtos.Orders;
using MedPoint.Service.Dtos.Payments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedPoint.Service.Interfaces.IPaymentServices
{
    public interface IPaymentService
    {
        Task<PaymentForResultDto> GetByIdAsync(long id, CancellationToken cancellationToken = default);
        Task<IEnumerable<PaymentForResultDto>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<PaymentForResultDto> AddAsync(PaymentForCreationDto dto, CancellationToken cancellationToken = default);
    }
}
