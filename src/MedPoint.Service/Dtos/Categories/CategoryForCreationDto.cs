using MedPoint.Service.Dtos.Catalogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedPoint.Service.Dtos.Categories
{
    public class CategoryForCreationDto
    {
        public string CategoryName { get; set; }
        public int CatalogId { get; set; } 
    }
}
