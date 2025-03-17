using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MedPoint.Data.IRepositories;
using MedPoint.Domain.Entities.Categories;
using MedPoint.Domain.Entities.Medications;
using MedPoint.Service.Dtos.Medications;
using MedPoint.Service.Exceptions;
using MedPoint.Service.Interfaces.IMedicationServices;
using MedPoint.Service.Helpers;
using Microsoft.EntityFrameworkCore.Infrastructure.Internal;
using System.Runtime.InteropServices;
using MedPoint.Service.Configurations;
using MedPoint.Service.Extensions;
using Microsoft.IdentityModel.Tokens;

namespace MedPoint.Service.Services.MedicationServices
{
    public class MedicationService(
            IRepository<Category> categoryRepository, 
            IMapper mapper, 
            IRepository<Medication> medicationRepository) : IMedicationService
    {
        private readonly IRepository<Category> categoryRepository = categoryRepository;
        private readonly IRepository<Medication> medicationRepository = medicationRepository;
        private readonly IMapper mapper = mapper;

        public async Task<IEnumerable<MedicationForResultDto>> GetByNameAsync(string name, PaginationParams @params, CancellationToken cancellationToken = default)
        {
            var medications = await medicationRepository.SelectAll()
                 .Where(m => EF.Functions.ILike(m.MedicationName, $"%{name}%"))
                 .ToPagedList(@params)
                 .ToListAsync(cancellationToken);

            return mapper.Map<IEnumerable<MedicationForResultDto>>(medications);
        }

        public async Task<MedicationForResultDto> AddAsync(MedicationForCreationDto dto, CancellationToken cancellationToken = default)
        {
            var categoryExists = await categoryRepository.SelectAll()
                .AsNoTracking()
                .AnyAsync(c => c.Id == dto.CategoryId, cancellationToken);

            if (categoryExists is false)
            {
                throw new MedPointException(404, "Category with this ID is not found.");
            }

            var medicationExists = await medicationRepository.SelectAll()
                .AsNoTracking()
                .AnyAsync(c => c.MedicationName == dto.MedicationName, cancellationToken);

            if (medicationExists)
            {
                throw new MedPointException(409, "Medication with this name already exists");
            }

            if (dto.Image == null || dto.Image.Length == 0)
            {
                throw new MedPointException(400, "Rasm yuklanishi shart!");
            }

            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(dto.Image.FileName);
            string uploadPath = Path.Combine(WebEnvironmentHost.WebRootPath, "images");

            if (!Directory.Exists(uploadPath))
            {
                Directory.CreateDirectory(uploadPath);
            }

            string filePath = Path.Combine(uploadPath, fileName);

            using (var streamFile = File.OpenWrite(filePath))
            {
                await dto.Image.CopyToAsync(streamFile, cancellationToken);
            };

            var mapped = mapper.Map<Medication>(dto);
            mapped.ImageUrl = $"/images/{fileName}";
            mapped.ExpiryDate = DateTime.UtcNow.AddDays(dto.ExpiryDuration);

            var result = await medicationRepository.CreateAsync(mapped, cancellationToken);

            return mapper.Map<MedicationForResultDto>(result);
        }

        public async Task<IEnumerable<MedicationForResultDto>> GetAllAsync(PaginationParams @params, CancellationToken cancellationToken = default)
        {
            var medications = await medicationRepository.SelectAll()
                .AsNoTracking()
                .ToPagedList(@params)
                .ToListAsync(cancellationToken);

            return mapper.Map<IEnumerable<MedicationForResultDto>>(medications);
        }

        public async Task<MedicationForResultDto> GetByIdAsync(long id, CancellationToken cancellationToken = default)
        {
            var medications = await medicationRepository.SelectAll()
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.Id == id, cancellationToken)
                ?? throw new MedPointException(404, "Medication with this ID is not found.");

            return mapper.Map<MedicationForResultDto>(medications);
        }

        public async Task<MedicationForResultDto> ModifyAsync(long id, MedicationForUpdateDto dto, CancellationToken cancellationToken = default)
        {
            var medications = await medicationRepository.SelectAll()
                .FirstOrDefaultAsync(m => m.Id == id, cancellationToken)
                ?? throw new MedPointException(404, "Medication with this ID is not found.");

            mapper.Map(dto, medications);
            medications.UpdatedAt = DateTimeOffset.UtcNow;

            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(dto.Image.FileName);
            string uploadPath = Path.Combine(WebEnvironmentHost.WebRootPath, "images");

            if (!Directory.Exists(uploadPath))
            {
                Directory.CreateDirectory(uploadPath);
            }

            string filePath = Path.Combine(uploadPath, fileName);

            using (var streamFile = File.OpenWrite(filePath))
            {
                await dto.Image.CopyToAsync(streamFile, cancellationToken);
            };

            var result = await medicationRepository.UpdateAsync(medications, cancellationToken);
            medications.ImageUrl = $"/images/{fileName}";

            return mapper.Map<MedicationForResultDto>(result);
        }

        public async Task<bool> RemoveAsync(long id, CancellationToken cancellationToken = default)
        {
            var medicationExists = await medicationRepository.SelectAll()
                .AsNoTracking()
                .AnyAsync(m => m.Id == id, cancellationToken);

            if (!medicationExists)
            {
                throw new MedPointException(404, "Medication with this ID is not found.");
            }

            return await medicationRepository.DeleteAsync(id, cancellationToken);
        }

        public async Task<IEnumerable<MedicationForResultDto>> GetByCategoryAsync(int categoryId, PaginationParams @params, CancellationToken cancellationToken = default)
        {
            var categoryExists = await categoryRepository.SelectAll()
                .AsNoTracking()
                .AnyAsync(c => c.Id == categoryId, cancellationToken);

            if (categoryExists is false)
            {
                throw new MedPointException(404, "Category with this ID is not found.");
            }

            var medications = await medicationRepository.SelectAll()
                .Where(c => c.CategoryId == categoryId)
                .ToPagedList(@params)
                .ToListAsync(cancellationToken);

            return mapper.Map<IEnumerable<MedicationForResultDto>>(medications);
        }
    }
}
