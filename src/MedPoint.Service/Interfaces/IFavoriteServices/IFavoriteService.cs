using MedPoint.Domain.Entities.Medications;
using MedPoint.Service.Dtos.Favorites;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedPoint.Service.Interfaces.IFavoriteServices
{
    public interface IFavoriteService
    {
        Task<IEnumerable<Medication>> GetMostLikedMedicationsAsync(CancellationToken cancellationToken);
        Task<bool> RemoveAsync(long id, CancellationToken cancellationToken = default);
        Task<FavoriteForResultDto> AddAsync(FavoriteForCreationDto dto, CancellationToken cancellationToken = default);
        Task<IEnumerable<FavoriteForResultDto>> GetAllAsync(CancellationToken cancellationToken = default);
    }
}
