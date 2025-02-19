using MedPoint.Service.Dtos.Catalogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedPoint.Service.Interfaces.ICatalogServices
{
    public interface ICatalogService
    {
        Task<bool> RemoveAsync(long id, CancellationToken cancellationToken = default);
        Task<IEnumerable<CatalogForResultDto>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<CatalogForResultDto> GetByIdAsync(long id, CancellationToken cancellationToken = default);
        Task<CatalogForResultDto> AddAsync(CatalogForCreationDto dto, CancellationToken cancellationToken = default);
        Task<CatalogForResultDto> ModifyAsync(long id, CatalogForUpdateDto dto, CancellationToken cancellationToken = default);

    }
}
