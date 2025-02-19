using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedPoint.Service.Configurations
{
    public class PaginationParams
    {
        private int maxPageSize = 25;
        private int pageSize;
        public int PageSize 
        {
            set => pageSize = value > maxPageSize ? maxPageSize : value;
            get => pageSize == 0 ? 10 : pageSize;
        }
        public int PageIndex { get; set; } = 1;
    }
}
