using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MedPoint.Data.IRepositories;
using MedPoint.Domain.Entities.Categories;
using MedPoint.Domain.Entities.Medications;
using MedPoint.Domain.Entities.Orders;
using MedPoint.Domain.Entities.Users;
using MedPoint.Service.Dtos.Medications;
using MedPoint.Service.Dtos.Orders;
using MedPoint.Service.Exceptions;
using MedPoint.Service.Interfaces.IOrderServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MedPoint.Data.DbContexts;

namespace MedPoint.Service.Services.OrderServices
{
    public class OrderService (
        IRepository<User> userRepository, 
        IMapper mapper, 
        AppDbContext context,
        IRepository<Medication> medicationRepository, 
        IRepository<Order> orderRepository) : IOrderService
    {
        private readonly IRepository<User> userRepository = userRepository;
        private readonly IRepository<Medication> medicationRepository = medicationRepository;
        private readonly IRepository<Order> orderRepository = orderRepository;
        private readonly IMapper mapper = mapper;
        private readonly AppDbContext context = context;
        public async Task<OrderForResultDto> AddAsync(OrderForCreationDto dto, CancellationToken cancellationToken = default)
        {
            var user = await userRepository.SelectAll()
                .AsNoTracking()
                .AnyAsync(o => o.Id == dto.UserId, cancellationToken);

            if (user is false)
            {
                throw new MedPointException(404, "User with this ID is not found.");
            }

            var mapped = mapper.Map<Order>(dto);
            var result = await orderRepository.CreateAsync(mapped, cancellationToken);

            return mapper.Map<OrderForResultDto>(result);
        }

        public async Task<IEnumerable<OrderForResultDto>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var orders = await orderRepository.SelectAll()
                .Include(o => o.Details)
                .AsNoTracking()
                .ToListAsync(cancellationToken);

            return mapper.Map<IEnumerable<OrderForResultDto>>(orders);
        }

        public async Task<OrderForResultDto> GetByIdAsync(long id, CancellationToken cancellationToken = default)
        {
            var orders = await orderRepository.SelectAll()
                .Include(o => o.Details)
                .AsNoTracking()
                .FirstOrDefaultAsync(o => o.Id == id, cancellationToken)
                ?? throw new MedPointException(404, "Order with this ID is not found.");

            return mapper.Map<OrderForResultDto>(orders);
        }

        public async Task<bool> CancelOrderAsync(int Id, CancellationToken cancellationToken = default)
        {
            var orders = await orderRepository.SelectAll()
                .AsNoTracking()
                .FirstOrDefaultAsync(o => o.Id == Id, cancellationToken)
                ?? throw new MedPointException(404, "Order with this ID is not found.");

                orders.IsCanceled = true;
                await context.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
