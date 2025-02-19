using MedPoint.Domain.Enums;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedPoint.Service.Dtos.Medications
{
    public class MedicationForCreationDto
    {
        public string MedicationName { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public int ExpiryDuration { get; set; }
        public decimal Price { get; set; }
        public string Producer {  get; set; }
        public IFormFile Image { get; set; }
        public int CategoryId { get; set; }
        public MedicationStatus Status { get; set; }
        public PrescriptionType Type { get; set; }
    }
}
