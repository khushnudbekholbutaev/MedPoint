using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MedPoint.Data.IRepositories;
using MedPoint.Domain.Entities.Catalogs;
using MedPoint.Domain.Entities.Categories;
using MedPoint.Service.Dtos.Categories;
using MedPoint.Service.Exceptions;
using MedPoint.Service.Interfaces.ICategoryServices;

namespace MedPoint.Service.Services.CategoryServices
{
    public class CategoryService : ICategoryService
    {
        private readonly IRepository<Category> categoryRepository;
        private readonly IRepository<Catalog> catalogRepository;
        private readonly IMapper mapper;
        public CategoryService(IRepository<Category> repository, IMapper mapper, IRepository<Catalog> catalogRepository)
            => (this.categoryRepository, this.mapper, this.catalogRepository) = (repository, mapper, catalogRepository);

        public async Task<CategoryForResultDto> AddAsync(CategoryForCreationDto dto, CancellationToken cancellationToken = default)
        {
            var catalogExists = await catalogRepository.SelectAll()
                .AsNoTracking()
                .AnyAsync(c => c.Id == dto.CatalogId, cancellationToken);

            if (catalogExists is false)
            {
                throw new MedPointException(404, "Catalog with this id is not found.");
            }

            var categoryExists = await categoryRepository.SelectAll()
                .AsNoTracking()
                .AnyAsync(c => c.CategoryName == dto.CategoryName, cancellationToken);

            if (categoryExists)
            {
                throw new MedPointException(409, "Category with this name already exists");
            }

            var mapped = mapper.Map<Category>(dto);
            var result = await categoryRepository.CreateAsync(mapped);

            return mapper.Map<CategoryForResultDto>(result);
        }

        public async Task<IEnumerable<CategoryForResultDto>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var category = await categoryRepository.SelectAll()
                .Include(c => c.Medications)
                .AsNoTracking()
                .ToListAsync(cancellationToken);

            return mapper.Map<IEnumerable<CategoryForResultDto>>(category);
        }

        public async Task<CategoryForResultDto> GetByIdAsync(long id, CancellationToken cancellationToken = default)
        {
            var category = await categoryRepository.SelectAll()
                .Include(c => c.Medications)
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id, cancellationToken)
                ?? throw new MedPointException(404, "Category with this ID is not found.");

            return mapper.Map<CategoryForResultDto>(category);
        }

        public async Task<CategoryForResultDto> ModifyAsync(long id, CategoryForUpdateDto dto, CancellationToken cancellationToken = default)
        {
            var category = await categoryRepository.SelectAll()
                .FirstOrDefaultAsync(x => x.Id == id, cancellationToken)
                ?? throw new MedPointException(404, "Category with this ID is not found.");

            mapper.Map(dto, category);
            category.UpdatedAt = DateTime.UtcNow;

            var result = await categoryRepository.UpdateAsync(category);

            return mapper.Map<CategoryForResultDto>(result);
        }

        public async Task<bool> RemoveAsync(long id, CancellationToken cancellationToken = default)
        {
            var categoryExists = await categoryRepository.SelectAll()
                .AsNoTracking()
                .AnyAsync(x => x.Id == id, cancellationToken);

            if (!categoryExists)
            {
                throw new MedPointException(404, "Category with this ID is not found.");
            }

            return await categoryRepository.DeleteAsync(id);
        }
    }
}
