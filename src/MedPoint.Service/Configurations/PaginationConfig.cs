using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedPoint.Service.Configurations
{
    public class PaginationConfig(int totalCount, PaginationParams @params)
    {
        public int CurrentPage { get; set; } = @params.PageIndex;
        public int TotalPages { get; set; } = (int)Math.Ceiling(totalCount / (double)@params.PageIndex);
        public int TotalCount { get; set; } = totalCount;
        public bool HasPrevious => CurrentPage > 1;
        public bool HasNext => CurrentPage < TotalPages;
    }
}
