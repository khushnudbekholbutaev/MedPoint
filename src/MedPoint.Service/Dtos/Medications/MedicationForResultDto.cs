using MedPoint.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedPoint.Service.Dtos.Medications
{
    public class MedicationForResultDto
    { 
        public int Id { get; set; }    
        public string MedicationName { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public string Producer {  get; set; }
        public string ImageUrl { get; set; }
        public DateTime ExpiryDate { get; set; } 
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
        public MedicationStatus Status { get; set; }
        public PrescriptionType Type { get; set; }
    }
}
