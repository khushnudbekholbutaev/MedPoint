using MedPoint.Service.Dtos.Banners;
using MedPoint.Service.Dtos.Medications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedPoint.Service.Interfaces.IBannerServices
{
    public interface IBannerService
    {
        Task<bool> RemoveAsync(long id, CancellationToken cancellationToken = default);
        Task<BannerForResultDto> GetByIdAsync(long id, CancellationToken cancellationToken = default);
        Task<IEnumerable<BannerForResultDto>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<BannerForResultDto> AddAsync(BannerForCreationDto dto, CancellationToken cancellationToken = default);
        Task<BannerForResultDto> ModifyAsync(long id, BannerForUpdateDto dto, CancellationToken cancellationToken = default);
    }
}
