using MedPoint.Domain.Entities.Categories;
using MedPoint.Service.Dtos.Categories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedPoint.Service.Dtos.Catalogs
{
    public class CatalogForResultDto
    {
        public int Id { get; set; } 
        public string CatalogName { get; set; }
        public ICollection<CategoryForResultDto> Categories { get; set; } 
    }
}
