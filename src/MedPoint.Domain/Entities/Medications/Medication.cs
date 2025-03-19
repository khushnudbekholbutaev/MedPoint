using MedPoint.Domain.Commons;
using MedPoint.Domain.Entities.Banners;
using MedPoint.Domain.Entities.OrderDetails;
using MedPoint.Domain.Enums;

namespace MedPoint.Domain.Entities.Medications
{
    public class Medication : Auditable
    {
        public string MedicationName { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public string Producer {  get; set; }
        public string ImageUrl {  get; set; }
        public int CategoryId { get; set; }
        public DateTime ExpiryDate { get; set; }
        public MedicationStatus Status { get; set; }
        public PrescriptionType Type { get; set; }
        public ICollection<OrderDetail> Details { get; set; }
        public ICollection<Banner> Banners { get; set; }
    }

}
