using MedPoint.Domain.Commons;
using MedPoint.Domain.Entities.CartItems;
using MedPoint.Domain.Entities.Medications;
using MedPoint.Domain.Entities.OrderDetails;
using MedPoint.Domain.Entities.Payments;
using MedPoint.Domain.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedPoint.Domain.Entities.Orders
{
    public class Order : Auditable
    {
        public DateTimeOffset OrderDate { get; set; } 
        public bool IsCanceled { get; set; }

        public User Users { get; set; }
        public int UserId { get; set; }

        public ICollection<OrderDetail> Details { get; set; }
    }
}
