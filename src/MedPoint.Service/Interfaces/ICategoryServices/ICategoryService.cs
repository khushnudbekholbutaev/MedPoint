using MedPoint.Service.Dtos.Categories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedPoint.Service.Interfaces.ICategoryServices
{
    public interface ICategoryService
    {
        Task<bool> RemoveAsync(long id, CancellationToken cancellationToken = default);
        Task<CategoryForResultDto> GetByIdAsync(long id, CancellationToken cancellationToken = default);
        Task<IEnumerable<CategoryForResultDto>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<CategoryForResultDto> AddAsync(CategoryForCreationDto dto, CancellationToken cancellationToken = default);
        Task<CategoryForResultDto> ModifyAsync(long id, CategoryForUpdateDto dto, CancellationToken cancellationToken = default);

    }
}
