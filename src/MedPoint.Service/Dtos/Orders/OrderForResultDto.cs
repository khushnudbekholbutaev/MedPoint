using MedPoint.Service.Dtos.OrderDetails;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedPoint.Service.Dtos.Orders
{
    public class OrderForResultDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }

        public ICollection<OrderDetailForResultDto> Details { get; set; }
    }
}
