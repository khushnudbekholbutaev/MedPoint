using MedPoint.Domain.Commons;
using MedPoint.Domain.Entities.Categories;
using MedPoint.Domain.Entities.Medications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedPoint.Domain.Entities.Banners
{
    public class Banner : Auditable
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }

        public int? CategoryId { get; set; }
        public int? MedicationId { get; set; }

        public Category Category { get; set; }
        public Medication Medication { get; set; }
    }
}
