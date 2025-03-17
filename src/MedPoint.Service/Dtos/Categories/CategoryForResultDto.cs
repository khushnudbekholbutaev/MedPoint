using MedPoint.Service.Dtos.Medications;

namespace MedPoint.Service.Dtos.Categories
{
    public class CategoryForResultDto
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }
        public long CatalogId { get; set; }

        public ICollection<MedicationForResultDto> Medications { get; set; }
    }
}
