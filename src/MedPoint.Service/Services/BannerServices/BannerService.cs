using AutoMapper;
using MedPoint.Data.IRepositories;
using MedPoint.Data.Repositories;
using MedPoint.Domain.Entities.Banners;
using MedPoint.Domain.Entities.Categories;
using MedPoint.Domain.Entities.Medications;
using MedPoint.Domain.Entities.Orders;
using MedPoint.Service.Dtos.Banners;
using MedPoint.Service.Dtos.Catalogs;
using MedPoint.Service.Exceptions;
using MedPoint.Service.Helpers;
using MedPoint.Service.Interfaces.IBannerServices;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedPoint.Service.Services.BannerServices
{
    public class BannerService(
        IRepository<Banner> bannerRepository,
        IRepository<Medication> medicationrepository,
        IRepository<Category> categoryRepsitory,
        IMapper mapper) : IBannerService
    {
        private readonly IRepository<Banner> bannerRepository = bannerRepository;
        private readonly IRepository<Medication> medicationRepository = medicationrepository;
        private readonly IRepository<Category> categoryRepository = categoryRepsitory;
        private readonly IMapper mapper = mapper;
        public async Task<BannerForResultDto> AddAsync(BannerForCreationDto dto, CancellationToken cancellationToken = default)
        {
            var medicationExists = await medicationRepository.SelectAll()
                .AsNoTracking()
                .AnyAsync(b => b.Id == dto.MedicationId, cancellationToken);

            if(medicationExists is false) 
            {
                throw new MedPointException(404, "Medication with this ID is not found.");
            }
            
            var categoryExist = await categoryRepository.SelectAll()
                .AsNoTracking()
                .AnyAsync(b => b.Id == dto.CategoryId, cancellationToken);

            if(categoryExist is false) 
            {
                throw new MedPointException(404, "Category with this ID is not found.");
            }

            var banners = await bannerRepository.SelectAll()
                .AsNoTracking()
                .AnyAsync(b => b.Title == dto.Title, cancellationToken);

            if(banners)
            {
                throw new MedPointException(409, "Banner with this title already exist.");
            }

            if (dto.Image == null || dto.Image.Length == 0)
            {
                throw new MedPointException(400, "Rasm yuklanishi shart!");
            }

            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(dto.Image.FileName);
            var uploadFile = Path.Combine(WebEnvironmentHost.WebRootPath, "images");

            if(!Directory.Exists(uploadFile))
            {
                Directory.CreateDirectory(uploadFile);
            }

            var filePath = Path.Combine(uploadFile, fileName);

            using (var streamFile = File.OpenWrite(filePath))
            {
                await dto.Image.CopyToAsync(streamFile, cancellationToken);
            };

            var mapped = mapper.Map<Banner>(dto);
            mapped.ImageUrl = $"/images/{fileName}";

            var result = await bannerRepository.CreateAsync(mapped, cancellationToken);
            return mapper.Map<BannerForResultDto>(result);
        }

        public async Task<IEnumerable<BannerForResultDto>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var banners = await bannerRepository.SelectAll()
                .AsNoTracking()
                .ToListAsync(cancellationToken);

            return mapper.Map<IEnumerable<BannerForResultDto>>(banners);
        }

        public async Task<BannerForResultDto> GetByIdAsync(long id, CancellationToken cancellationToken = default)
        {
            var banners = await bannerRepository.SelectAll()
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id, cancellationToken)
                ?? throw new MedPointException(404, "Banner with this ID is not found.");

            return mapper.Map<BannerForResultDto>(banners);
        }

        public async Task<BannerForResultDto> ModifyAsync(long id, BannerForUpdateDto dto, CancellationToken cancellationToken = default)
        {
            var banners = await bannerRepository.SelectAll()
                 .FirstOrDefaultAsync(x => x.Id == id, cancellationToken)
                 ?? throw new MedPointException(404, "Banner with this ID is not found.");

            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(dto.Image.FileName);
            var uploadFile = Path.Combine(WebEnvironmentHost.WebRootPath, "images");

            if(!Directory.Exists(uploadFile))
            {
                Directory.CreateDirectory(uploadFile);
            }

            var filePath = Path.Combine(uploadFile, fileName);

            using(var stream = File.OpenWrite(filePath)) 
            { 
                await dto.Image.CopyToAsync(stream, cancellationToken);
            }

            mapper.Map(dto, banners);
            banners.UpdatedAt = DateTime.UtcNow;
            banners.ImageUrl = $"/images/{fileName}";

            var result = await bannerRepository.UpdateAsync(banners, cancellationToken);

            return mapper.Map<BannerForResultDto>(result);
        }

        public async Task<bool> RemoveAsync(long id, CancellationToken cancellationToken = default)
        {
            var banners = await bannerRepository.SelectAll()
                .AsNoTracking()
                .AnyAsync(x => x.Id == id, cancellationToken);

            if (!banners)
            {
                throw new MedPointException(404, "Banner with this ID is not found.");
            }

            return await bannerRepository.DeleteAsync(id, cancellationToken);
        }
    }
}
