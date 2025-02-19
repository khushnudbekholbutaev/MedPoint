using MedPoint.Domain.Commons;
using MedPoint.Domain.Entities.Categories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedPoint.Domain.Entities.Catalogs
{
    public class Catalog : Auditable
    {
        public string CatalogName { get; set; }
        public ICollection<Category> Categories { get; set; }
    }
}
