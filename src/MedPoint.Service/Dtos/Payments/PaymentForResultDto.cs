using MedPoint.Domain.Enums;
using MedPoint.Service.Dtos.OrderDetails;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedPoint.Service.Dtos.Payments
{
    public class PaymentForResultDto
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public DateTimeOffset PaymentDate { get; set; } = DateTimeOffset.UtcNow;
        public string PaymentMethod { get; set; }
        public int OrderId { get; set; }
    }
}
