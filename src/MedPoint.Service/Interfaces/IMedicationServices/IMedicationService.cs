using MedPoint.Domain.Entities.Medications;
using MedPoint.Service.Configurations;
using MedPoint.Service.Dtos.Medications;

namespace MedPoint.Service.Interfaces.IMedicationServices
{
    public interface IMedicationService
    {
        Task<IEnumerable<MedicationForResultDto>> GetByCategoryAsync(int categoryId, PaginationParams @params, CancellationToken cancellationToken = default);
        Task<IEnumerable<MedicationForResultDto>> GetByNameAsync(string name, PaginationParams @params, CancellationToken cancellationToken = default);
        Task<bool> RemoveAsync(long id, CancellationToken cancellationToken = default);
        Task<MedicationForResultDto> GetByIdAsync(long id, CancellationToken cancellationToken = default);
        Task<IEnumerable<MedicationForResultDto>> GetAllAsync(PaginationParams @params, CancellationToken cancellationToken = default);
        Task<MedicationForResultDto> AddAsync(MedicationForCreationDto dto, CancellationToken cancellationToken = default);
        Task<MedicationForResultDto> ModifyAsync(long id, MedicationForUpdateDto dto, CancellationToken cancellationToken = default);
    }
}
