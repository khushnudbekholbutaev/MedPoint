using MedPoint.Domain.Commons;
using MedPoint.Domain.Entities.OrderDetails;
using MedPoint.Domain.Entities.Orders;
using MedPoint.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedPoint.Domain.Entities.Payments
{
    public class Payment : Auditable
    {
        public decimal Amount { get; set; }
        public PaymentStatus Status { get; set; } 
        public DateTimeOffset PaymentDate { get; set; } 
        public string PaymentMethod { get; set; }
        
        public int OrderId { get; set; }
        public Order Order { get; set; }
    }
}
