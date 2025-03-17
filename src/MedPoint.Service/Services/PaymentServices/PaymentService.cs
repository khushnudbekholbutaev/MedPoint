using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MedPoint.Data.IRepositories;
using MedPoint.Domain.Entities.Medications;
using MedPoint.Domain.Entities.Orders;
using MedPoint.Domain.Entities.Payments;
using MedPoint.Domain.Entities.Users;
using MedPoint.Service.Dtos.Payments;
using MedPoint.Service.Dtos.Users;
using MedPoint.Service.Exceptions;
using MedPoint.Service.Interfaces.IPaymentServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MedPoint.Domain.Entities.OrderDetails;

namespace MedPoint.Service.Services.PaymentServices
{
    public class PaymentService(
        IRepository<Payment> paymentRepository, 
        IRepository<Order> orderRepository, 
        IMapper mapper) : IPaymentService
    {
        private readonly IRepository<Payment> paymentRepository = paymentRepository;
        private readonly IRepository<Order> orderRepository = orderRepository;
        private readonly IMapper mapper = mapper;

        public async Task<PaymentForResultDto> AddAsync(PaymentForCreationDto dto, CancellationToken cancellationToken = default)
        {
            var mapped = mapper.Map<Payment>(dto);
            mapped.Amount = await orderRepository.SelectAll()
                                .Where(od => od.Id == dto.OrderId)
                                .SelectMany(od => od.Details)
                                .SumAsync(od => od.Price, cancellationToken);

            var result = await paymentRepository.CreateAsync(mapped, cancellationToken);

            return mapper.Map<PaymentForResultDto>(result);
        }

        public async Task<IEnumerable<PaymentForResultDto>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var payment = await paymentRepository.SelectAll()
             .AsNoTracking()
             .ToListAsync(cancellationToken);

            return mapper.Map<IEnumerable<PaymentForResultDto>>(payment);
        }

        public async Task<PaymentForResultDto> GetByIdAsync(long id, CancellationToken cancellationToken = default)
        {
            var payment = await paymentRepository.SelectAll()
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.Id == id, cancellationToken)
            ?? throw new MedPointException(404, "Payment with this ID is not found.");

            return mapper.Map<PaymentForResultDto>(payment);
        }
    }
}
