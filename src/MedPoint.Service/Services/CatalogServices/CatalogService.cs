using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MedPoint.Data.IRepositories;
using MedPoint.Domain.Entities.Catalogs;
using MedPoint.Service.Dtos.Catalogs;
using MedPoint.Service.Exceptions;
using MedPoint.Service.Interfaces.ICatalogServices;

namespace MedPoint.Service.Services.CatalogServices
{
    public class CatalogService : ICatalogService
    {
        private readonly IRepository<Catalog> repository;
        private readonly IMapper mapper;
        public CatalogService(IRepository<Catalog> repository, IMapper mapper)
            => (this.repository, this.mapper) = (repository, mapper);

        public async Task<CatalogForResultDto> AddAsync(CatalogForCreationDto dto, CancellationToken cancellationToken = default)
        {
            var catalogExists = await repository.SelectAll()
                .AsNoTracking()
                .AnyAsync(x => x.CatalogName == dto.CatalogName, cancellationToken);

            if (catalogExists)
            {
                throw new MedPointException(409, "Catalog with this name already exists.");
            }

            var mapped = mapper.Map<Catalog>(dto);
            var result = await repository.CreateAsync(mapped);

            return mapper.Map<CatalogForResultDto>(result);
        }

        public async Task<IEnumerable<CatalogForResultDto>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var catalogs = await repository.SelectAll()
                .Include(c => c.Categories)
                .AsNoTracking()
                .ToListAsync(cancellationToken);

            return mapper.Map<IEnumerable<CatalogForResultDto>>(catalogs);
        }

        public async Task<CatalogForResultDto> GetByIdAsync(long id, CancellationToken cancellationToken = default)
        {
            var catalog = await repository.SelectAll()
                .Include(c => c.Categories)
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id, cancellationToken)
                ?? throw new MedPointException(404, "Catalog with this ID is not found.");

            return mapper.Map<CatalogForResultDto>(catalog);
        }

        public async Task<CatalogForResultDto> ModifyAsync(long id, CatalogForUpdateDto dto, CancellationToken cancellationToken = default)
        {
            var catalog = await repository.SelectAll()
                .FirstOrDefaultAsync(x => x.Id == id, cancellationToken)
                ?? throw new MedPointException(404, "Catalog with this ID is not found.");

            mapper.Map(dto, catalog);
            catalog.UpdatedAt = DateTime.UtcNow;

            var result = await repository.UpdateAsync(catalog, cancellationToken);

            return mapper.Map<CatalogForResultDto>(result);
        }

        public async Task<bool> RemoveAsync(long id, CancellationToken cancellationToken = default)
        {
            var catalogExists = await repository.SelectAll()
                .Include(c => c.Categories)
                .AsNoTracking()
                .AnyAsync(x => x.Id == id, cancellationToken);

            if (!catalogExists)
            {
                throw new MedPointException(404, "Catalog with this ID is not found.");
            }

            return await repository.DeleteAsync(id, cancellationToken);
        }
    }
}
