using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MedPoint.Data.IRepositories;
using MedPoint.Domain.Entities.Medications;
using MedPoint.Domain.Entities.OrderDetails;
using MedPoint.Domain.Entities.Orders;
using MedPoint.Domain.Entities.Payments;
using MedPoint.Domain.Entities.Users;
using MedPoint.Service.Dtos.OrderDetails;
using MedPoint.Service.Dtos.Orders;
using MedPoint.Service.Exceptions;
using MedPoint.Service.Interfaces.IOrderServices;
using MedPoint.Service.Interfaces.OrderDetailServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MedPoint.Domain.Entities.CartItems;

namespace MedPoint.Service.Services.OrderDetailServices
{
    public class OrderDetailService(
        IRepository<OrderDetail> orderDetailRepository, 
        IMapper mapper,
        IRepository<CartItem> cartItemRepository,
        IRepository<Medication> medicationRepository, 
        IRepository<Order> orderRepository) : IOrderDetailService
    {
        private readonly IRepository<OrderDetail> orderDetailRepository = orderDetailRepository;
        private readonly IRepository<Medication> medicationRepository = medicationRepository;
        private readonly IRepository<Order> orderRepository = orderRepository;
        private readonly IRepository<CartItem> cartItemRepository = cartItemRepository;
        private readonly IMapper mapper = mapper;

        public async Task<OrderDetailForResultDto> AddAsync(OrderDetailForCreationDto dto, CancellationToken cancellationToken = default)
        {
            var order = await orderRepository.SelectAll()
                .AsNoTracking()
                .FirstOrDefaultAsync(od => od.Id == dto.OrderId, cancellationToken)
                ?? throw new MedPointException(404, "Order with this ID is not found.");

            var med = await medicationRepository.SelectAll()
                .AsNoTracking()
                .FirstOrDefaultAsync(od => od.Id == dto.MedicationId, cancellationToken)
                ?? throw new MedPointException(404, "Medication with this ID is not found.");

            var cartItem = await cartItemRepository.SelectAll()
                .AsNoTracking()
                .FirstOrDefaultAsync(od => order.UserId == od.UserId, cancellationToken)
                ?? throw new MedPointException(404, "User with this ID is not found.");

            var mapped = mapper.Map<OrderDetail>(dto);
            mapped.Quantity = cartItem.Count;
            mapped.Price = med.Price * cartItem.Count;

            var result = await orderDetailRepository.CreateAsync(mapped, cancellationToken);

            return mapper.Map<OrderDetailForResultDto>(result);
        }

        public async Task<IEnumerable<OrderDetailForResultDto>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var orderDetails = await orderDetailRepository.SelectAll()
                .AsNoTracking()
                .ToListAsync(cancellationToken);

            return mapper.Map<IEnumerable<OrderDetailForResultDto>>(orderDetails);
        }

        public async Task<OrderDetailForResultDto> GetByIdAsync(long id, CancellationToken cancellationToken = default)
        {
            var orderDetails = await orderDetailRepository.SelectAll()
                .AsNoTracking()
                .FirstOrDefaultAsync(od => od.Id == id, cancellationToken)
                ?? throw new MedPointException(404, "OrderDetail with this ID is not found.");

            return mapper.Map<OrderDetailForResultDto>(orderDetails);
        }

        public async Task<OrderDetailForResultDto> ModifyAsync(long id, OrderDetailForUpdateDto dto, CancellationToken cancellationToken = default)
        {
            var orderDetails = await orderDetailRepository.SelectAll()
                .FirstOrDefaultAsync(od => od.Id == id, cancellationToken)
                ?? throw new MedPointException(404, "OrderDetail with this ID is not found.");

            mapper.Map(dto, orderDetails);
            orderDetails.UpdatedAt = DateTimeOffset.UtcNow;

            var result = await orderDetailRepository.UpdateAsync(orderDetails, cancellationToken);

            return mapper.Map<OrderDetailForResultDto>(result);
        }

        public async Task<bool> RemoveAsync(long id, CancellationToken cancellationToken = default)
        {
            var orderExists = await orderDetailRepository.SelectAll()
                .AsNoTracking()
                .AnyAsync(od => od.Id == id, cancellationToken);

            if (!orderExists)
            {
                throw new MedPointException(404, "OrderDetail with this ID is not found.");
            }

            return await orderDetailRepository.DeleteAsync(id, cancellationToken);
        }
    }
}
