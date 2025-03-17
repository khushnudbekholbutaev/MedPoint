using MedPoint.Domain.Commons;
using MedPoint.Domain.Entities.Medications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedPoint.Domain.Entities.Categories
{
    public class Category : Auditable
    {
        public string CategoryName { get; set; }
        public int CatalogId { get; set; }
        public ICollection<Medication> Medications { get; set; }
    }
}
